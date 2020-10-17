using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Transactions;
using WebAPI.App_Code;
using WebAPI.Models;
using WebAPI.Models.DTO._Treatment;

namespace WebAPI.Services._Treatment
{
    public class Service : IDisposable
    {
        private readonly EmeciEntities Context = new EmeciEntities();

        public void Dispose()
        {
            Context.Dispose();
        }


        /// <summary>
        /// get the catalog of treatments that the
        /// doctor uses to add the consultation
        /// </summary>
        public IEnumerable<Treatments> Get(int doctorId)
        {
            return Context.catrecetas
                .Where(x => x.idmedico == doctorId)
                .OrderBy(n => n.nombre)
                .ToList()
                .Select(x => new Treatments
                {
                    Id = x.idcatreceta,
                    GroupName = x.nombre,
                    List = x.lineas.Split('|').ToList()
                });
        }


        /// <summary>
        /// save new treatment
        /// </summary>
        public TreatmentRes Save(int doctorId, TreatmentReq req)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    var cat = new catrecetas
                    {
                        idmedico = doctorId,
                        nombre = req.GroupName,
                        lineas = string.Join("|", req.List)
                    };

                    Context.catrecetas.Add(cat);
                    Context.SaveChanges();
                    scope.Complete();
                    return new TreatmentRes
                    {
                        Id = cat.idcatreceta
                    };
                }
            }
            catch (Exception ex)
            {
                Log.Write($"WebAPI.Services.Diagnostic - Save=> {ex.Message}");
            }
            return new TreatmentRes();
        }


        /// <summary>
        /// get the treatments added to the 
        /// patient's consultation
        /// </summary>
        public List<string> GetByConsult(DateTime ConsultationDate, int pacientId)
        {
            var q = Context.Recetas
                 .Where(x =>
                    x.idpaciente == pacientId &&
                    DbFunctions.TruncateTime(x.Fecha) == DbFunctions.TruncateTime(ConsultationDate)
                    )
                 .FirstOrDefault()?.Lineas;
            return HelperService.LineFormat(q);
        }


        /// <summary>
        /// remove treatments from the doctor's 
        /// treatment catalog
        /// </summary>
        public bool Delete(int treatmentId)
        {
            var register = new catrecetas { idcatreceta = treatmentId };

            Context.catrecetas.Attach(register);
            Context.catrecetas.Remove(register);
            try
            {
                Context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.Write($"WebAPI.Services._Treatment - Delete => {ex.Message}");
            }
            return false;
        }
    }
}