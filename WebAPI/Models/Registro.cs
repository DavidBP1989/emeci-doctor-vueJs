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
    
    public partial class Registro
    {
        public int idRegistro { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Domicilio { get; set; }
        public string Colonia { get; set; }
        public Nullable<int> idCiudad { get; set; }
        public string idEstado { get; set; }
        public string IdPais { get; set; }
        public string Telefono { get; set; }
        public string TelefonoCel { get; set; }
        public string Tipo { get; set; }
        public string Status { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> FechaRegistro { get; set; }
        public Nullable<System.DateTime> FechaExpiracion { get; set; }
        public string Emails { get; set; }
        public Nullable<System.DateTime> FechaRenovacion { get; set; }
        public string OtraCiudad { get; set; }
        public string NoExt { get; set; }
        public string CodigoPostal { get; set; }
        public string clave { get; set; }
        public string Emeci { get; set; }
        public string apellido2 { get; set; }
        public string CURP { get; set; }
    }
}
