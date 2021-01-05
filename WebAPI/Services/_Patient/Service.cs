using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Transactions;
using WebAPI.App_Code;
using WebAPI.Models;
using Dapper;
using WebAPI.Models.DTO._Patient;
using System.Threading.Tasks;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text;
using drawing = System.Drawing;

namespace WebAPI.Services._Patient
{
    public class Service: IDisposable
    {
        readonly EmeciEntities Context = new EmeciEntities();

        public void Dispose()
        {
            Context.Dispose();
        }

        public Service()
        {
            ((IObjectContextAdapter)Context).ObjectContext.CommandTimeout = 120;
        }

        public async Task<Tuple<int,IEnumerable<Patients>>> Get(int doctorId, int? page, int? itemsPerPage, string columnName, string textToSearch, string orderby)
        {
            string emeci = GetEmeci(doctorId);
            int _page = (int)((page - 1) * itemsPerPage);

            var result = await Context.Database.Connection.QueryMultipleAsync("spPatientList", new {
                emeci,
                columnName,
                @textToSearch = textToSearch.Replace(' ', '%'), 
                @page = _page,
                itemsPerPage,
                orderby
            }, null, null, System.Data.CommandType.StoredProcedure);

            int totalRows = result.Read<int>().First();

            var _result = result.Read<Patients>().ToList();
            foreach (var r in _result) r.AgeInMonths = AgeInMonths(r.BirthDate);

            return new Tuple<int,IEnumerable<Patients>>(totalRows, _result);
        }

        public Patient GetByPatientId(int patientId)
        {
            var q = Context.vPatients
                .Where(x => x.patientId == patientId)
                .Select(x => new Patient
                {
                    Id = x.patientId,
                    Name = x.name.Trim(),
                    LastName = x.lastName.Trim(),
                    EMECI = x.emeci,
                    Sex= x.sex,
                    LastConsultation = x.lastConsultation,
                    BirthDate = x.birthDate,
                    GroupRH = x.groupRH,
                    Allergies = x.allergies,
                    Reserved = x.reserved,
                    RelevantPathologies = x.relevantPathologies
                }).FirstOrDefault();

            if (q != null)
            {
                q.AgeInMonths = AgeInMonths(q.BirthDate);
                var alg = Context.Patologias
                    .Where(x => x.idpaciente == patientId && x.Categoria == 5)
                    .ToList();
                if (alg.Count > 0 && string.IsNullOrEmpty(q.Allergies))
                {
                    q.Allergies = "";
                    foreach (var a in alg)
                    {
                        q.Allergies += a.Alergeno + "\r\n";
                    }
                }

                var coordinates = Context.DatosTarjeta.Where(x => x.noTarjeta == q.EMECI).ToList();
                var random = coordinates.ElementAt(new Random().Next(1, coordinates.Count()));
                q.RandomCoordinate = random.Coordenada;
                q.RandomCoordinateValue = random.Dato;
            }

            return q;
        }

        public string GetLastEmeci(int doctorId)
        {
            string emeci = GetPatientsByDoctorId(doctorId)
                .OrderByDescending(x => int.Parse(x.emeci.Split('-')[2]))
                .FirstOrDefault()?.emeci;

            if (!string.IsNullOrEmpty(emeci))
            {
                string[] split = emeci.Split('-');
                int last = int.Parse(split[2]);
                last++;
                return $"{split[0]}-{split[1]}-{last:000#}";
            }

            return "";
        }

        public NewPatientRes AddNewPatient(int doctorId, NewPatientReq req)
        {
            var result = new NewPatientRes();
            string emeci = GetLastEmeci(doctorId);
            MemoryStream draw = null;

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    var r = new Registro
                    {
                        Nombre = req.Name,
                        Apellido = req.LastName,
                        Telefono = req.Phone,
                        Tipo = "P",
                        Status = "V",
                        FechaRegistro = DateTime.Now.Date,
                        FechaExpiracion = DateTime.Now.AddMonths(1).Date,
                        Emails = req.Emails,
                        clave = req.Password,
                        Emeci = emeci
                    };

                    Context.Registro.Add(r);
                    Context.SaveChanges();

                    var p = new Paciente
                    {
                        IdRegistro = r.idRegistro,
                        Sexo = req.Sex,
                        FechaNacimiento = DateTime.Parse(req.BirthDate),
                        NombreMadre = req.MothersName,
                        NombrePadre = req.FathersName,
                        AlergiaMedicina = req.Allergy
                    };

                    Context.Paciente.Add(p);
                    Context.SaveChanges();

                    draw = DrawDataInCard(emeci);

                    scope.Complete();
                    result.PatientId = p.idPaciente;
                }
            }
            catch (Exception ex)
            {
                Log.Write($"WebAPI.Services._Patient - AddNewPatient => ${ex.Message}");
            }

            if (result.PatientId.HasValue && draw != null)
            {
                var emailService = new EmailService(req.Emails);
                Task.Run(async () =>
                {
                    await emailService.SendPatientRegister(req, draw);
                });
            }

            return result;
        }

        public NewPatientRes FindExistingPatient(NewExistingPatientReq req)
        {
            var pquery = Context.Registro
                .Join(Context.Paciente,
                    reg => reg.idRegistro,
                    pac => pac.IdRegistro,
                    (reg, pac) => new { r = reg, p = pac })
                .Where(x => x.r.Emeci == req.Emeci && x.r.Tipo == "P")
                .FirstOrDefault();

            if (pquery != null)
            {
                var dtquery = Context.DatosTarjeta
                    .Where(x => 
                        x.Coordenada == req.Coordinate &&
                        x.Dato == req.Value &&
                        x.noTarjeta == req.Emeci
                    )
                    .FirstOrDefault();
                if (pquery != null)
                    return new NewPatientRes
                    {
                        PatientId = pquery.p.idPaciente
                    };
            }

            return new NewPatientRes();
        }

        private List<vPatients> GetPatientsByDoctorId(int doctorId)
        {
            var emeci = Context.Registro
                .Join(Context.Medico,
                    reg => reg.idRegistro,
                    med => med.IdRegistro,
                    (reg, med) => new { r = reg, m = med })
                .Where(x => x.m.Idmedico == doctorId)
                .FirstOrDefault()?.r.Emeci;

            return Context.vPatients
                .Where(x => x.emeci.StartsWith(emeci))
                .ToList();
        }

        private string GetEmeci(int doctorId)
        {
            return Context.Registro
                .Join(Context.Medico,
                    reg => reg.idRegistro,
                    med => med.IdRegistro,
                    (reg, med) => new { r = reg, m = med })
                .Where(x => x.m.Idmedico == doctorId)
                .FirstOrDefault()?.r.Emeci;
        }

        private int AgeInMonths(DateTime? birthDate)
        {
            if (!birthDate.HasValue)
                return -1;
            return ((DateTime.Now.Year - birthDate.Value.Year) * 12) + (DateTime.Now.Month - birthDate.Value.Month);
        }

        private MemoryStream DrawDataInCard(string emeci)
        {
            var memory = new MemoryStream();
            var doc = new Document();
            var pdf = PdfWriter.GetInstance(doc, memory);

            doc.Open();

            var bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
            var fBold = new Font(bfTimes, 8, Font.NORMAL, BaseColor.BLACK);

            
            var pdfTable = new PdfPTable(2)
            {
                TotalWidth = 500f,
                LockedWidth = true,
                HorizontalAlignment = 1
            };

            var widths = new float[] { 2.8f, 2.1f };
            pdfTable.SetWidths(widths);
            pdfTable.DefaultCell.Border = Rectangle.NO_BORDER;

            var file = $"{AppDomain.CurrentDomain.BaseDirectory}imgAccess.jpg";
            var image = Image.GetInstance(ConvertImageToBytes(file, emeci));
            image.ScaleAbsolute(258f, 153f);

            var cell = new PdfPCell(image)
            {
                HorizontalAlignment = Element.ALIGN_MIDDLE,
                Border = Rectangle.NO_BORDER
            };
            pdfTable.AddCell(cell);

            var position = new PdfPTable(11)
            {
                HorizontalAlignment = 1,
                TotalWidth = 265f,
                LockedWidth = true
            };

            var cellPosition = new PdfPCell(new Phrase("Posiciones de Acceso Seguro"))
            {
                BackgroundColor = new BaseColor(162, 212, 255),
                HorizontalAlignment = 1,
                Colspan = 11,
                Border = Rectangle.NO_BORDER
            };
            position.AddCell(cellPosition);

            string[] arrABC = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
            for (var i = 0; i <= 10; i++)
            {
                position.DefaultCell.BackgroundColor = new BaseColor(255, 255, 255);
                position.DefaultCell.Padding = 2f;
                position.DefaultCell.Border = Rectangle.NO_BORDER;

                if (i == 0)
                    position.AddCell("");
                else position.AddCell(new Phrase(arrABC[i - 1], fBold));
            }

            bool color = true;
            DatosTarjeta dt;

            for (int i = 1; i <= 10; i++)
            {
                if (color)
                    position.DefaultCell.BackgroundColor = new BaseColor(162, 212, 255);
                else position.DefaultCell.BackgroundColor = new BaseColor(255, 255, 255);

                color = !color;

                position.AddCell(new Phrase(i.ToString(), fBold));

                string letter;
                for (var j = 0; j <= 9; j++)
                {
                    letter = arrABC[j];
                    dt = new DatosTarjeta
                    {
                        noTarjeta = emeci,
                        Dato = new Random().Next(0, 999).ToString("00#"),
                        Coordenada = $"{letter}{i}"
                    };

                    position.AddCell(new Phrase(dt.Dato, fBold));
                    Context.DatosTarjeta.Add(dt);
                }
            }

            pdfTable.AddCell(position);
            doc.Add(pdfTable);

            pdf.CloseStream = false;
            doc.Close();
            memory.Position = 0;
            Context.SaveChanges();

            return memory;
        }

        private byte[] ConvertImageToBytes(string file, string msj)
        {
            var bitImg = new drawing.Bitmap(file);
            var gImg = drawing.Graphics.FromImage(bitImg);

            gImg.SmoothingMode = drawing.Drawing2D.SmoothingMode.AntiAlias;
            gImg.DrawString(
                msj,
                new drawing.Font("Arial", 20, drawing.FontStyle.Bold),
                drawing.SystemBrushes.Window, new drawing.Point(50, 175)
            );

            var ms = new MemoryStream();
            bitImg.Save(ms, drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }
    }
}