﻿using System;
using System.Collections.Generic;
using System.Linq;
using WebAPI.Models;
using WebAPI.Models.DTO._Consults;
using TreatmentService = WebAPI.Services._Treatment.Service;
using DiagnosticService = WebAPI.Services._Diagnostic.Service;
using StudiesService = WebAPI.Services._Studies.Service;
using WebAPI.App_Code;
using System.Transactions;

namespace WebAPI.Services._Consults
{
    public class Service : IDisposable
    {
        readonly EmeciEntities Context = new EmeciEntities();

        public void Dispose()
        {
            Context.Dispose();
        }

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


        #region gynecology
        public GynecologyConsult GetGyneConsult(int consultId)
        {
            var q = (from c in Context.Consultas
                     join g in Context.ConsultaGinecologa
                     on c.idconsulta equals g.idconsulta into joined
                     from j in joined.DefaultIfEmpty()
                     where j != null && c.idconsulta == consultId
                     select new GynecologyConsult
                     {
                         BasicConsult = new BasicConsult
                         {
                             Weight = c.Peso,
                             Size = c.Altura,
                             Mass = 0,
                             Temperature = c.Temperatura,
                             BloodPressure_A = c.TensionArterial,
                             BloodPressure_B = c.TensionArterialB,
                             ReasonForConsultation = c.motivo
                         },
                         PatientConsult = new PatientConsult
                         {
                             PatientId = c.idpaciente
                         },
                         LastMenstruationDate = j.FechaUltimaMestruacion,
                         Gestas = (int)j.Gestas,
                         Paragestas = (int)j.ParaGestas,
                         Cesareans = (int)j.Cesareas,
                         Abortions = (int)j.abortos,
                         NewlyBorn = (int)j.RecienNacidosVivos,
                         Stillbirth = (int)j.mortinatos,
                         AgeOfOnsetOfActiveSexualLife = (int)j.EdadInicioVidaSexual,
                         Menacma = j.menacma,
                         Checkbox = new Options
                         {
                             Oligomenorrea = j.oligonorrea.Value,
                             Proiomenorrea = j.Proiomenorrea.Value,
                             Hipermenorrea = j.Hipermenorrea.Value,
                             Dismenorrea = j.Dismenorrea.Value,
                             Dispareunia = j.Dispareunia.Value,
                             Leucorrea = j.Leucorrea.Value,
                             Lactorrea = j.Lactorrea.Value,
                             Amenorrea = j.Amenorrea.Value,
                             Metrorragia = j.Metrorragia.Value,
                             Others = j.Otros.Value
                         },
                         SpecifyOthers = j.OtrosEspecifique,
                         Partner = new Partner
                         {
                             HasAPartner = j.TienePareja.Value,
                             Name = j.nombrePareja,
                             Sex = j.SexoPareja,
                             BirthDate = j.FechaNacimientoPareja,
                             GroupRH = j.GrupoRHPareja,
                             Age = j.edadPareja,
                             MaritalStatus = j.EstadoCivilPareja,
                             Occupation = j.OcupacionPareja,
                             Phone = j.TelefonoPareja
                         }
                     }).FirstOrDefault();
            if (q != null)
            {
                if (q.BasicConsult.Size % 1 == 0)
                    q.BasicConsult.Size /= 100;
                if (q.BasicConsult.Weight > 0 && q.BasicConsult.Size > 0)
                {
                    double mass = (double)(q.BasicConsult.Weight / (q.BasicConsult.Size * q.BasicConsult.Size));
                    q.BasicConsult.Mass = (float)Math.Round(mass);
                }

                var p = Context.Paciente.FirstOrDefault(x => x.idPaciente == q.PatientConsult.PatientId);
                if (p != null)
                    q.MenarcaAge = (int)p.EdadMenarca;
            }

            return q;
        }

        public bool SaveGynecologyConsult(int doctorId, GynecologyConsult req)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    var now = DateTime.Now;

                    Paciente p = Context.Paciente.FirstOrDefault(x => x.idPaciente == req.PatientConsult.PatientId);
                    p.AlergiaMedicina = req.PatientConsult.Allergies;
                    p.AlergiaOtros = req.PatientConsult.Reserved;
                    p.Patologia = req.PatientConsult.RelevantPathologies;
                    p.EdadMenarca = req.MenarcaAge;
                    Context.SaveChanges();

                    var consult = new Consultas
                    {
                        idmedico = doctorId,
                        idpaciente = req.PatientConsult.PatientId,
                        Peso = req.BasicConsult.Weight,
                        Altura = req.BasicConsult.Size,
                        Temperatura = req.BasicConsult.Temperature,
                        TensionArterial = req.BasicConsult.BloodPressure_A,
                        TensionArterialB = req.BasicConsult.BloodPressure_B,
                        motivo = req.BasicConsult.ReasonForConsultation,
                        Fecha = now
                    };

                    Context.Consultas.Add(consult);
                    Context.SaveChanges();

                    var gConsult = new ConsultaGinecologa
                    {
                        idconsulta = consult.idconsulta,
                        FechaUltimaMestruacion = req.LastMenstruationDate,
                        Gestas = (byte?)req.Gestas,
                        ParaGestas = (byte?)req.Paragestas,
                        Cesareas = (byte?)req.Cesareans,
                        abortos = (byte?)req.Abortions,
                        RecienNacidosVivos = (byte?)req.NewlyBorn,
                        mortinatos = (byte?)req.Stillbirth,
                        EdadInicioVidaSexual = (byte?)req.AgeOfOnsetOfActiveSexualLife,
                        menacma = req.Menacma,
                        oligonorrea = req.Checkbox.Oligomenorrea,
                        Proiomenorrea = req.Checkbox.Proiomenorrea,
                        Hipermenorrea = req.Checkbox.Hipermenorrea,
                        Dismenorrea = req.Checkbox.Dismenorrea,
                        Dispareunia = req.Checkbox.Dispareunia,
                        Leucorrea = req.Checkbox.Leucorrea,
                        Lactorrea = req.Checkbox.Lactorrea,
                        Amenorrea = req.Checkbox.Amenorrea,
                        Metrorragia = req.Checkbox.Metrorragia,
                        Otros = req.Checkbox.Others,
                        OtrosEspecifique = req.SpecifyOthers,
                        TienePareja = req.Partner.HasAPartner
                    };
                    
                    if (req.Partner.HasAPartner)
                    {
                        gConsult.SexoPareja = req.Partner.Sex;
                        gConsult.EstadoCivilPareja = req.Partner.MaritalStatus;
                        gConsult.GrupoRHPareja = req.Partner.GroupRH;
                        gConsult.FechaNacimientoPareja = req.Partner.BirthDate;
                        gConsult.OcupacionPareja = req.Partner.Occupation;
                        gConsult.TelefonoPareja = req.Partner.Phone;
                        gConsult.nombrePareja = req.Partner.Name;
                        gConsult.edadPareja = req.Partner.Age;
                    }

                    Context.ConsultaGinecologa.Add(gConsult);
                    Context.SaveChanges();

                    scope.Complete();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.Write($"WebAPI.Services._Consults - SaveGynecologyConsult => ${ex.Message}");
            }
            return false;
        }

        public IEnumerable<ConsultationDates> GetDatesGynecologyConsults(int patientId)
        {
            return (from c in Context.Consultas
                    join g in Context.ConsultaGinecologa
                    on c.idconsulta equals g.idconsulta into joined
                    from j in joined.DefaultIfEmpty()
                    where j != null && c.idpaciente == patientId
                    orderby c.Fecha descending
                    select new ConsultationDates
                    {
                        Id = j.idconsulta,
                        ConsultationDate = c.Fecha
                    }).ToList();
        }
        #endregion


        #region obstetric
        public ObstetricConsult GetObstetricConsult(int consultId)
        {
            var q = (from c in Context.Consultas
                     join g in Context.ConsultaObstetrica
                     on c.idconsulta equals g.idconsulta into joined
                     from j in joined.DefaultIfEmpty()
                     where j != null && c.idconsulta == consultId
                     select new ObstetricConsult
                     {
                         BasicConsult = new BasicConsult
                         {
                             Weight = c.Peso,
                             Size = c.Altura,
                             Mass = 0,
                             Temperature = c.Temperatura,
                             BloodPressure_A = c.TensionArterial,
                             BloodPressure_B = c.TensionArterialB,
                             ReasonForConsultation = c.motivo
                         },
                         PatientConsult = new PatientConsult
                         {
                             PatientId = c.idpaciente
                         },
                         PregnancyNumber = j.noembarazo.Value,
                         SexuallyActive = j.activaSexualmente.Value,
                         LastParturitionDate = j.FechaUltmoParto,
                         FirstDayOfLastMenstruation = j.PrimerDiaUltimaMestruacuion,
                         PreviousToxemias = j.ToxemiasPrevias.Value == 1,
                         SpecifyToxemias = j.EspecifiqueToxemias,
                         Parturition = j.Partos.Value,
                         DystociaType = j.TipoDistocia.Value,
                         SpecifyDystociaType = j.EspecifiqueTipoDistocia,
                         DystociaReason = j.MotivoDistocia.Value,
                         SpecifyDystociaReason = j.EspecifiqueMotivoDistocia,
                         PreviousCesarean = j.CesareasPrevia.Value,
                         Forceps = j.UsoDeForceps.Value,
                         Stillbirths = j.Motinatos.Value,
                         NewbornAlive = j.RMVivos.Value,
                         EctopicPregnancies = j.EmbarazoEtopicos == 1,
                         SpecifyEctopicPregnancies = j.EmbrazoEtopicoExplique,
                         PreviousPregnacyComplications = j.EmbrazosComplicadosPrevios == 1,
                         SpecifyPreviousPregnacyComplications = j.EmbarazosComplicadosExplique,
                         PerinatalComplications = j.NoComplicacionesPertinales == 1,
                         SpecifyPerinatalComplications = j.ComplicacionesPerinatalesExplique,
                         AbnormalPregnancies = j.NoEmbrazosAnormales == 1,
                         SpecifyAbnormalPregnancies = j.EmbarazosAnormalesExplique,
                         Observations = j.Observaciones,
                         PregnancyControl = new PregnancyControl
                         {
                             FU = j.FU.Value,
                             FCF = j.FCF.Value,
                             CC = j.CC.Value,
                             CA = j.CA.Value,
                             LF = j.LF.Value,
                             DBP = j.DSP.Value,
                             Position = j.Posicion,
                             Presentation = j.Presentacion,
                             Situtation = j.siuacuion,
                             Attitude = j.Actitud,
                             FetalMovements = j.MovimientosFetales,
                             ApproximateProductWeight = j.PesoAproxProducto.Value,
                             TA = j.TA.Value,
                             FCM = j.FCM.Value,
                             Edema = j.Edema,
                             MadeUf = j.SeHizoUf.Value,
                             Ultrasound = j.ultrasonido,
                             PhysicalExploration = j.exploracionFisica
                         }
                     }).FirstOrDefault();
            if (q != null)
            {
                if (q.BasicConsult.Size % 1 == 0)
                    q.BasicConsult.Size /= 100;
                if (q.BasicConsult.Weight > 0 && q.BasicConsult.Size > 0)
                {
                    double mass = (double)(q.BasicConsult.Weight / (q.BasicConsult.Size * q.BasicConsult.Size));
                    q.BasicConsult.Mass = (float)Math.Round(mass);
                }
            }

            return q;
        }

        public bool SaveObstetricConsult(int doctorId, ObstetricConsult req)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    var now = DateTime.Now;

                    Paciente p = Context.Paciente.FirstOrDefault(x => x.idPaciente == req.PatientConsult.PatientId);
                    p.AlergiaMedicina = req.PatientConsult.Allergies;
                    p.AlergiaOtros = req.PatientConsult.Reserved;
                    p.Patologia = req.PatientConsult.RelevantPathologies;
                    Context.SaveChanges();

                    var consult = new Consultas
                    {
                        idmedico = doctorId,
                        idpaciente = req.PatientConsult.PatientId,
                        Peso = req.BasicConsult.Weight,
                        Altura = req.BasicConsult.Size,
                        Temperatura = req.BasicConsult.Temperature,
                        TensionArterial = req.BasicConsult.BloodPressure_A,
                        TensionArterialB = req.BasicConsult.BloodPressure_B,
                        motivo = req.BasicConsult.ReasonForConsultation,
                        Fecha = now
                    };

                    Context.Consultas.Add(consult);
                    Context.SaveChanges();

                    var consultObst = new ConsultaObstetrica
                    {
                        idconsulta = consult.idconsulta,
                        noembarazo = req.PregnancyNumber,
                        activaSexualmente = req.SexuallyActive,
                        abortos = req.Abortions,
                        FechaUltmoParto = req.LastParturitionDate,
                        PrimerDiaUltimaMestruacuion = req.FirstDayOfLastMenstruation,
                        ToxemiasPrevias = (byte?)(req.PreviousToxemias ? 1 : 0),
                        EspecifiqueToxemias = req.PreviousToxemias ? req.SpecifyToxemias : null,
                        Partos = req.Parturition,
                        CesareasPrevia = req.PreviousCesarean,
                        UsoDeForceps = req.Forceps,
                        Motinatos = req.Stillbirths,
                        RMVivos = req.NewbornAlive,
                        EmbarazoEtopicos = (byte?)(req.EctopicPregnancies ? 1 : 0),
                        EmbrazoEtopicoExplique = req.EctopicPregnancies ? req.SpecifyEctopicPregnancies : null,
                        EmbrazosComplicadosPrevios = (byte?)(req.PreviousPregnacyComplications ? 1 : 0),
                        EmbarazosComplicadosExplique = req.PreviousPregnacyComplications ? req.SpecifyPreviousPregnacyComplications : null,
                        NoComplicacionesPertinales = (byte?)(req.PerinatalComplications ? 1 : 0),
                        ComplicacionesPerinatalesExplique = req.PerinatalComplications ? req.SpecifyPerinatalComplications : null,
                        NoEmbrazosAnormales = (byte?)(req.AbnormalPregnancies ? 1 : 0),
                        EmbarazosAnormalesExplique = req.AbnormalPregnancies ? req.SpecifyAbnormalPregnancies : null,
                        Observaciones = req.Observations,
                        FU = req.PregnancyControl.FU,
                        FCF = req.PregnancyControl.FCF,
                        CC = req.PregnancyControl.CC,
                        CA = req.PregnancyControl.CA,
                        LF = req.PregnancyControl.LF,
                        DSP = req.PregnancyControl.DBP,
                        Posicion = req.PregnancyControl.Position,
                        Presentacion = req.PregnancyControl.Presentation,
                        siuacuion = req.PregnancyControl.Situtation,
                        Actitud = req.PregnancyControl.Attitude,
                        MovimientosFetales = req.PregnancyControl.FetalMovements,
                        PesoAproxProducto = req.PregnancyControl.ApproximateProductWeight,
                        TA = req.PregnancyControl.TA,
                        FCM = req.PregnancyControl.FCM,
                        Edema = req.PregnancyControl.Edema,
                        SeHizoUf = req.PregnancyControl.MadeUf,
                        ultrasonido = req.PregnancyControl.Ultrasound,
                        exploracionFisica = req.PregnancyControl.PhysicalExploration
                    };

                    if (req.Parturition == 1)
                    {
                        consultObst.TipoDistocia = (byte?)req.DystociaType;
                        consultObst.EspecifiqueTipoDistocia = req.DystociaType == 3 ? req.SpecifyDystociaType : null;
                        consultObst.MotivoDistocia = (byte?)req.DystociaReason;
                        consultObst.EspecifiqueMotivoDistocia = req.DystociaReason == 4 ? req.SpecifyDystociaReason : null;
                    }

                    Context.ConsultaObstetrica.Add(consultObst);
                    Context.SaveChanges();

                    scope.Complete();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.Write($"WebAPI.Services._Consults - SaveObstetricConsult => ${ex.Message}");
            }
            return false;
        }
        #endregion

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