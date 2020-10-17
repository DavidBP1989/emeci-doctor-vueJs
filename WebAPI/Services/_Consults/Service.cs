using System;
using System.Collections.Generic;
using System.Linq;
using WebAPI.Models;
using WebAPI.Models.DTO._Consults;
using TreatmentService = WebAPI.Services._Treatment.Service;
using DiagnosticService = WebAPI.Services._Diagnostic.Service;
using StudiesService = WebAPI.Services._Studies.Service;
using WebAPI.App_Code;
using System.Transactions;
using System.Security.Cryptography.X509Certificates;

namespace WebAPI.Services._Consults
{
    public class Service : IDisposable
    {
        readonly EmeciEntities Context = new EmeciEntities();

        public void Dispose()
        {
            Context.Dispose();
        }

        /// <summary>
        /// get general consult by consult id
        /// </summary>
        public GeneralConsult GetGeneralConsult(int consultId)
        {
            var q = Context.Consultas
                .Where(x => x.idconsulta == consultId)
                .Select(x => new GeneralConsult
                {
                    ConsultationDate = x.Fecha,
                    PatientId = x.idpaciente,
                    Weight = x.Peso,
                    Size = x.Altura,
                    Mass = 0,
                    Temperature = x.Temperatura,
                    BloodPressure_A = x.TensionArterial,
                    BloodPressure_B = x.TensionArterialB,
                    HeadCircuference = x.perimetroCefalico,
                    HeartRate = x.FrecuenciaCardiaca,
                    BreathingFrecuency = x.FrecuenciaRespiratoria,
                    ReasonForConsultation = x.motivo,
                    PhysicalExploration = x.SignosSintomas1,
                    PreventiveMeasures = x.MedidasPreventivas,
                    Observations = x.observaciones,
                    _Prognostic = x.Pronostico
                }).FirstOrDefault();
            if (q != null)
            {
                if (q.Size % 1 == 0)
                    q.Size /= 100;
                if (q.Weight > 0 && q.Size > 0)
                    q.Mass = q.Weight / (q.Size * q.Size);

                q.Diagnostics =
                    new DiagnosticService().GetByConsult(
                        q.ConsultationDate.GetValueOrDefault(),
                        q.PatientId.GetValueOrDefault()
                    );
                q.Treatments =
                    new TreatmentService().GetByConsult(
                        q.ConsultationDate.GetValueOrDefault(),
                        q.PatientId.GetValueOrDefault()
                    );
                q.CabinetStudies = new StudiesService().GetCabinetStudies(
                        q.ConsultationDate.GetValueOrDefault(),
                        q.PatientId.GetValueOrDefault()
                    );
                q.LaboratoryStudies = new StudiesService().GetLaboratoryStudies(
                        q.ConsultationDate.GetValueOrDefault(),
                        q.PatientId.GetValueOrDefault()
                    );
                q.Prognostic = q._Prognostic != null ?
                    q._Prognostic.Split('|', (char)StringSplitOptions.RemoveEmptyEntries).ToList()
                    : new List<string>();
            }
            return q;
        }


        /// <summary>
        /// save general consult
        /// </summary>
        public bool SaveGeneralConsult(int doctorId, GeneralConsult req)
        {
            try
            {
                using(TransactionScope scope = new TransactionScope())
                {
                    var now = DateTime.Now;

                    //patient
                    Paciente p = Context.Paciente.FirstOrDefault(x => x.idPaciente == req.PatientId);
                    p.AlergiaMedicina = req.Allergies;
                    p.AlergiaOtros = req.Reserved;
                    p.Patologia = req.RelevantPathologies;
                    Context.SaveChanges();

                    //consult
                    var consult = new Consultas
                    {
                        idmedico = doctorId,
                        idpaciente = req.PatientId,
                        Peso = req.Weight,
                        Altura = req.Size,
                        Temperatura = req.Temperature,
                        TensionArterial = req.BloodPressure_A,
                        TensionArterialB = req.BloodPressure_B,
                        perimetroCefalico = req.HeadCircuference,
                        FrecuenciaCardiaca = req.HeartRate,
                        FrecuenciaRespiratoria = req.BreathingFrecuency,
                        motivo = req.ReasonForConsultation,
                        SignosSintomas1 = req.PhysicalExploration,
                        MedidasPreventivas = req.PreventiveMeasures,
                        observaciones = req.Observations,
                        Fecha = now,
                        Pronostico = req.Prognostic != null ? string.Join("|", req.Prognostic) : ""
                    };

                    Context.Consultas.Add(consult);
                    Context.SaveChanges();

                    //treatments
                    var treatments = new Recetas
                    {
                        idconsulta = consult.idconsulta,
                        idmedico = doctorId,
                        idpaciente = req.PatientId,
                        Fecha = now,
                        Lineas = SetLines(req.Treatments)
                    };

                    Context.Recetas.Add(treatments);
                    Context.SaveChanges();

                    //diagnostics
                    var diagnostics = new Diagnosticos
                    {
                        idconsulta = consult.idconsulta,
                        idmedico = doctorId,
                        idpaciente = req.PatientId.GetValueOrDefault(),
                        Fecha = now,
                        Lineas = SetLines(req.Diagnostics)
                    };

                    Context.Diagnosticos.Add(diagnostics);
                    Context.SaveChanges();

                    //laboratory studies
                    var laboratory = new EstudiosLab
                    {
                        idconsulta = consult.idconsulta,
                        idmedico = doctorId,
                        idpaciente = req.PatientId,
                        Fecha = now,
                        Lineas = SetLines(req.LaboratoryStudies)
                    };

                    Context.EstudiosLab.Add(laboratory);
                    Context.SaveChanges();

                    //cabinet studies
                    var cabinet = new EstudiosGab
                    {
                        idconsulta = consult.idconsulta,
                        idmedico = doctorId,
                        idpaciente = req.PatientId,
                        Fecha = now,
                        Lineas = SetLines(req.CabinetStudies)
                    };

                    Context.EstudiosGab.Add(cabinet);
                    Context.SaveChanges();

                    scope.Complete();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.Write($"WebAPI.Services._Consults - SaveGeneralConsult => ${ex.Message}");
            }
            return false;
        }

       
        /// <summary>
        /// get list of previous dates
        /// </summary>
        public IEnumerable<ConsultationDates> GetConsultationDates(int pacientId)
        {
            return Context.Consultas
                .Where(x => x.idpaciente == pacientId)
                .OrderByDescending(n => n.Fecha)
                .ToList()
                .Select(x => new ConsultationDates
                {
                    Id = x.idconsulta,
                    ConsultationDate = x.Fecha
                });
        }


        private string SetLines(List<string> lines)
        {
            int count = 0;
            string l = "";
            if (lines != null)
            {
                foreach (var sr in lines)
                {
                    if (sr.Trim() != "")
                    {
                        l += $"{count++:0#}{sr}\r\n";
                    }
                }
            }
            return l;
        }
    }
}