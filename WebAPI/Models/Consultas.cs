//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Consultas
    {
        public Nullable<int> idpaciente { get; set; }
        public int idconsulta { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public Nullable<float> Peso { get; set; }
        public Nullable<float> Altura { get; set; }
        public Nullable<float> Temperatura { get; set; }
        public Nullable<float> Cabeza { get; set; }
        public Nullable<float> perimetroCefalico { get; set; }
        public Nullable<int> TensionArterial { get; set; }
        public Nullable<int> TensionArterialB { get; set; }
        public Nullable<int> FrecuenciaCardiaca { get; set; }
        public Nullable<int> FrecuenciaRespiratoria { get; set; }
        public Nullable<int> idmedico { get; set; }
        public string motivo { get; set; }
        public string SignosSintomas1 { get; set; }
        public string SignosSintomas2 { get; set; }
        public string SignosSintomas3 { get; set; }
        public string MedidasPreventivas { get; set; }
        public Nullable<System.DateTime> ProximaCita { get; set; }
        public string observaciones { get; set; }
        public string Pronostico { get; set; }
    }
}
