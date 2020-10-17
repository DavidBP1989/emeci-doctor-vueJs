using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebAPI.Models;
using WebAPI.Models.DTO._Studies;

namespace WebAPI.Services._Studies
{
    public class Service : IDisposable
    {
        private readonly EmeciEntities Context = new EmeciEntities();
        public void Dispose()
        {
            Context.Dispose();
        }

        /// <summary>
        /// get laboratory and cabinet studies
        /// </summary>
        /// <param name="type">0 = laboratory, 1 = cabinet</param>
        public IEnumerable<CategoryStudies> GetStudies(int type)
        {
            var catStudies = Context.CatCategoEstudios.ToList();
            var studies = Context.CatEstudios.ToList();

            return catStudies
                .Where(x => x.Tipo == type)
                .Select(x => new CategoryStudies
                {
                    Id = x.idcategoriaestudio,
                    Name = x.descripcion,
                    StudiesList = studies
                    .Where(s => s.idcategoriaestudio == x.idcategoriaestudio)
                    .Select(s => new CategoryStudies.Studies
                    {
                        Id = s.idestudio,
                        Name = s.descripcion
                    }).ToList()
                }).ToList();
        }

        public List<string> GetLaboratoryStudies(DateTime ConsultationDate, int pacientId)
        {
            var result = new List<string>();

            var q = Context.EstudiosLab
                 .Where(x =>
                    x.idpaciente == pacientId &&
                    DbFunctions.TruncateTime(x.Fecha) == DbFunctions.TruncateTime(ConsultationDate)
                    )
                 .FirstOrDefault()?.Lineas;
            return HelperService.LineFormat(q);
        }

        public List<string> GetCabinetStudies(DateTime ConsultationDate, int pacientId)
        {
            var result = new List<string>();

            var q = Context.EstudiosGab
                 .Where(x =>
                    x.idpaciente == pacientId &&
                    DbFunctions.TruncateTime(x.Fecha) == DbFunctions.TruncateTime(ConsultationDate)
                    )
                 .FirstOrDefault()?.Lineas;
            return HelperService.LineFormat(q);
        }
    }
}