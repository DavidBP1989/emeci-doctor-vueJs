using System;
using System.Collections.Generic;
using System.Linq;
using WebAPI.Models;
using WebAPI.Models.DTO;
using WebAPI.Models.DTO._Doctor;
using DoctorService = WebAPI.Services._Doctor.Service;

namespace WebAPI.Services._Base
{
    public class Service : IDisposable
    {
        readonly EmeciEntities Context = new EmeciEntities();

        public void Dispose()
        {
            Context.Dispose();
        }

        /// <summary>
        /// get the doctor's password
        /// to log back in
        /// </summary>
        public ForgotPasswordRes ForgotPassword(string emeci)
        {
            var q = Context.Registro
                .FirstOrDefault(x => x.Emeci == emeci && x.Status == "V");
            return new ForgotPasswordRes
            {
                Email = q?.Emails,
                Password = q?.clave,
                DoctorName = q != null ? ($"{q.Nombre} {q.Apellido}") : ""
            };
        }

        public bool DoctorRegister(Register req) => new DoctorService().Register(req);

        public List<States> GetStates()
        {
            return Context.Estados
                .Where(x => x.IdPais == "MX")
                .Select(x => new States
                {
                    Id = x.idEstado,
                    Name = x.Nombre
                }).ToList();
        }

        public List<Cities> GetCities(string stateId)
        {
            return Context.Ciudades
                .Where(x => x.idEstado == stateId)
                .Select(x => new Cities
                {
                    Id = x.idciudad,
                    Name = x.Nombre
                }).ToList();
        }
    }
}