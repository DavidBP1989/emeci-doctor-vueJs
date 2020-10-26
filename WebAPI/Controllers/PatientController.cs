using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData;
using WebAPI.Models.DTO._Patient;
using WebAPI.Services._Patient;
using queryable = WebAPI.Query.QueryableAttribute;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/patient")]
    [Authorize(Roles = "User")]
    public class PatientController : ApiController
    {
        private readonly Service service = new Service();

        [Route("{doctorId:Int}")]
        [queryable]
        [HttpGet]
        public async Task<IEnumerable<Patients>> Get(
            int doctorId, int? page, int? itemsPerPage, string columnName = null,
            string textToSearch = null, string orderby = null)
        {
            var result = await service.Get(
                doctorId, page ?? 1,
                itemsPerPage ?? 15,
                columnName ?? "Nombre",
                textToSearch ?? "",
                orderby ?? "name asc"
            );

            Request.Properties["x-total-count"] = result.Item1;
            return result.Item2;
        }


        [Route("byId/{patientId:Int}")]
        [HttpGet]
        //GET /api/patient/byId/123
        public Patient GetById(int patientId)
        {
            return service.GetByPatientId(patientId);
        }
        

        [Route("{doctorId:Int}/last")]
        [HttpGet]
        //GET /api/patient/1400/last
        public string GetLastEmeci(int doctorId)
        {
            return service.GetLastEmeci(doctorId);
        }


        [Route("{doctorId:Int}")]
        [HttpPost]
        //POST /api/patient/1400
        public NewPatientRes Post(int doctorId, [FromBody]NewPatientReq req)
        {
            return service.AddNewPatient(doctorId, req);
        }


        [Route("newpatient")]
        [HttpGet]
        //GET /api/patient/newpatient
        public NewPatientRes NewExistingPatient([FromUri]NewExistingPatientReq req)
        {
            return service.FindExistingPatient(req);
        }
    }
}
