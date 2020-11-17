using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace WebAPI.Models.DTO._Consults
{
    public class GeneralConsult : PatientConsult
    {
        [JsonIgnore]
        public DateTime? ConsultationDate { get; set; }

        public float? Weight { get; set; }
        public float? Size { get; set; }
        public float? Mass { get; set; }
        public float? Temperature { get; set; }
        public int? BloodPressure_A { get; set; }
        public int? BloodPressure_B { get; set; }
        public float? HeadCircuference { get; set; }
        public int? HeartRate { get; set; }
        public int? BreathingFrecuency { get; set; }
        public string ReasonForConsultation { get; set; }
        public string PhysicalExploration { get; set; }
        public string PreventiveMeasures { get; set; }
        public string Observations { get; set; }
        public List<string> Diagnostics { get; set; }
        public List<string> Treatments { get; set; }
        public List<string> CabinetStudies { get; set; }
        public List<string> LaboratoryStudies { get; set; }
        [JsonIgnore]
        public string _Prognostic { get; set; }
        public List<string> Prognostic { get; set; }
    }
}