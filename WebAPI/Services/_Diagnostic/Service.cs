using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Transactions;
using WebAPI.App_Code;
using WebAPI.Models;
using WebAPI.Models.DTO._Diagnostic;

namespace WebAPI.Services._Diagnostic
{
    public class Service : IDisposable
    {
        private readonly EmeciEntities Context = new EmeciEntities();

        public void Dispose()
        {
            Context.Dispose();
        }

        /// <summary>
        /// get the catalog of diagnoses that the
        /// doctor uses to add the consultation
        /// </summary>
        public IEnumerable<Diagnostics> Get(int doctorId)
        {
            return Context.catdiagnostico
                .Where(x => x.idmedico == doctorId)
                .OrderBy(n => n.nombre)
                .ToList()
                .Select(x => new Diagnostics
                {
                    Id = x.idcatdiagnostico,
                    GroupName = x.nombre,
                    List = x.lineas.Split('|').ToList()
                });
        }


        /// <summary>
        /// get the diagnoses added to the 
        /// patient's consultation
        /// </summary>
        public List<string> GetByConsult(DateTime ConsultationDate, int pacientId)
        {
            var result = new List<string>();

            var q = Context.Diagnosticos
                 .Where(x =>
                    x.idpaciente == pacientId &&
                    DbFunctions.TruncateTime(x.Fecha) == DbFunctions.TruncateTime(ConsultationDate)
                    )
                 .FirstOrDefault()?.Lineas;
            return HelperService.LineFormat(q);
        }


        /// <summary>
        /// save new diagnosis
        /// </summary>
        public DiagnosticsRes Save(int doctorId, DiagnosticsReq req)
        {
            try
            {
                using(TransactionScope scope = new TransactionScope())
                {
                    var cat = new catdiagnostico
                    {
                        idmedico = doctorId,
                        nombre = req.GroupName,
                        lineas = string.Join("|", req.List)
                    };

                    Context.catdiagnostico.Add(cat);
                    Context.SaveChanges();
                    scope.Complete();
                    return new DiagnosticsRes
                    {
                        Id = cat.idcatdiagnostico
                    };
                }
            }
            catch (Exception ex)
            {
                Log.Write($"WebAPI.Services.Diagnostic - Save=> {ex.Message}");
            }
            return new DiagnosticsRes();
        }


        /// <summary>
        /// remove diagnoses from the doctor's 
        /// diagnostic catalog
        /// </summary>
        public bool Delete(int diagnosticId)
        {
            var register = new catdiagnostico { idcatdiagnostico = diagnosticId };

            Context.catdiagnostico.Attach(register);
            Context.catdiagnostico.Remove(register);
            try
            {
                Context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.Write($"WebAPI.Services.Diagnostic - Delete => {ex.Message}");
            }
            return false;
        } 
    }
}