﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EmeciEntities : DbContext
    {
        public EmeciEntities()
            : base("name=EmeciEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Medico> Medico { get; set; }
        public virtual DbSet<Registro> Registro { get; set; }
        public virtual DbSet<catdiagnostico> catdiagnostico { get; set; }
        public virtual DbSet<catrecetas> catrecetas { get; set; }
        public virtual DbSet<Consultas> Consultas { get; set; }
        public virtual DbSet<Diagnosticos> Diagnosticos { get; set; }
        public virtual DbSet<Recetas> Recetas { get; set; }
        public virtual DbSet<DatosTarjeta> DatosTarjeta { get; set; }
        public virtual DbSet<Patologias> Patologias { get; set; }
        public virtual DbSet<vPatients> vPatients { get; set; }
        public virtual DbSet<CatCategoEstudios> CatCategoEstudios { get; set; }
        public virtual DbSet<CatEstudios> CatEstudios { get; set; }
        public virtual DbSet<EstudiosGab> EstudiosGab { get; set; }
        public virtual DbSet<EstudiosLab> EstudiosLab { get; set; }
        public virtual DbSet<Paciente> Paciente { get; set; }
        public virtual DbSet<Ciudades> Ciudades { get; set; }
        public virtual DbSet<Estados> Estados { get; set; }
        public virtual DbSet<ConsultaGinecologa> ConsultaGinecologa { get; set; }
        public virtual DbSet<ConsultaObstetrica> ConsultaObstetrica { get; set; }
    }
}
