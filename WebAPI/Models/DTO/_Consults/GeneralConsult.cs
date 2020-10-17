using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace WebAPI.Models.DTO._Consults
{
    public class GeneralConsult : PatientConsult
    {
        [JsonIgnore]
        public DateTime? ConsultationDate { get; set; }

        
        /// <summary>
        /// peso kg
        /// </summary>
        public float? Weight { get; set; }
        /// <summary>
        /// talla m
        /// </summary>
        public float? Size { get; set; }
        /// <summary>
        /// masa
        /// </summary>
        public float? Mass { get; set; }
        /// <summary>
        /// temperatura c
        /// </summary>
        public float? Temperature { get; set; }
        /// <summary>
        /// tension arterial a
        /// </summary>
        public int? BloodPressure_A { get; set; }
        /// <summary>
        /// tension arterial b
        /// </summary>
        public int? BloodPressure_B { get; set; }
        /// <summary>
        /// perimetro cefalico
        /// </summary>
        public float? HeadCircuference { get; set; }
        /// <summary>
        /// frecuencia cardiaca
        /// </summary>
        public int? HeartRate { get; set; }
        /// <summary>
        /// frecuencia respiratoria
        /// </summary>
        public int? BreathingFrecuency { get; set; }
        /// <summary>
        /// motivo de la consulta
        /// </summary>
        public string ReasonForConsultation { get; set; }
        /// <summary>
        /// exploracion fisica
        /// </summary>
        public string PhysicalExploration { get; set; }
        /// <summary>
        /// medidas preventivas
        /// </summary>
        public string PreventiveMeasures { get; set; }
        /// <summary>
        /// observaciones
        /// </summary>
        public string Observations { get; set; }
        /// <summary>
        /// diagnosticos
        /// </summary>
        public List<string> Diagnostics { get; set; }
        /// <summary>
        /// tratamientos
        /// </summary>
        public List<string> Treatments { get; set; }
        /// <summary>
        /// Estudios de gabinete
        /// </summary>
        public List<string> CabinetStudies { get; set; }
        /// <summary>
        /// Estudios de laboratorio
        /// </summary>
        public List<string> LaboratoryStudies { get; set; }
        [JsonIgnore]
        public string _Prognostic { get; set; }
        /// <summary>
        /// pronosticos
        /// </summary>
        public List<string> Prognostic { get; set; }
    }
}