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
    
    public partial class ConsultaGinecologa
    {
        public int idconsultaginecologa { get; set; }
        public int idconsulta { get; set; }
        public Nullable<System.DateTime> FechaUltimaMestruacion { get; set; }
        public Nullable<byte> Gestas { get; set; }
        public Nullable<byte> ParaGestas { get; set; }
        public Nullable<byte> Cesareas { get; set; }
        public Nullable<byte> abortos { get; set; }
        public Nullable<byte> RecienNacidosVivos { get; set; }
        public Nullable<byte> mortinatos { get; set; }
        public Nullable<byte> EdadInicioVidaSexual { get; set; }
        public string menacma { get; set; }
        public Nullable<bool> oligonorrea { get; set; }
        public Nullable<bool> Proiomenorrea { get; set; }
        public Nullable<bool> Hipermenorrea { get; set; }
        public Nullable<bool> Dismenorrea { get; set; }
        public Nullable<bool> Dispareunia { get; set; }
        public Nullable<bool> Leucorrea { get; set; }
        public Nullable<bool> Lactorrea { get; set; }
        public Nullable<bool> Amenorrea { get; set; }
        public Nullable<bool> Metrorragia { get; set; }
        public Nullable<bool> Otros { get; set; }
        public string OtrosEspecifique { get; set; }
        public Nullable<bool> TienePareja { get; set; }
        public string SexoPareja { get; set; }
        public string EstadoCivilPareja { get; set; }
        public string GrupoRHPareja { get; set; }
        public Nullable<System.DateTime> FechaNacimientoPareja { get; set; }
        public string OcupacionPareja { get; set; }
        public string TelefonoPareja { get; set; }
        public string nombrePareja { get; set; }
        public string edadPareja { get; set; }
    }
}
