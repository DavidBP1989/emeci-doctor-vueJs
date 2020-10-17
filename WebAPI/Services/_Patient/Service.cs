using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Transactions;
using WebAPI.App_Code;
using WebAPI.Models;
using WebAPI.Models.DTO._Patient;

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

            
        /// <summary>
        /// get pacient list by doctor
        /// </summary>
        public IEnumerable<Patients> Get(int doctorId)
        {
            var q = GetPatientsByDoctorId(doctorId)
                .Select(p => new Patients
                {
                    Id = p.patientId,
                    Name = p.name.Trim() + " " + p.lastName.Trim(),
                    //LastName = p.lastName.Trim(),
                    EMECI = p.emeci,
                    Sex = p.sex,
                    LastConsultation = p.lastConsultation,
                    AgeInMonths = AgeInMonths(p.birthDate)
                });

            return q;
        }


        /// <summary>
        /// get patient by id
        /// </summary>
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


        /// <summary>
        /// get the next number of emeci
        /// to add as a new patient
        /// </summary>
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


        /// <summary>
        /// add new patient
        /// </summary>
        public NewPatientRes AddNewPatient(int doctorId, NewPatientReq req)
        {
            try
            {
                string emeci = GetLastEmeci(doctorId);

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

                    scope.Complete();
                    return new NewPatientRes
                    {
                        PatientId = p.idPaciente
                    };
                }
            }
            catch (Exception ex)
            {
                Log.Write($"WebAPI.Services._Patient - AddNewPatient => ${ex.Message}");
            }
            return new NewPatientRes();
        }


        /// <summary>
        /// get patient who had already registered
        /// with another doctor
        /// </summary>
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

        private int AgeInMonths(DateTime? birthDate)
        {
            if (!birthDate.HasValue)
                return -1;
            return ((DateTime.Now.Year - birthDate.Value.Year) * 12) + (DateTime.Now.Month - birthDate.Value.Month);
        }
    }
}