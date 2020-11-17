using System.Collections.Generic;
using System.Web.Http;
using WebAPI.Models.DTO._Consults;
using WebAPI.Services._Consults;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/consults")]
    [Authorize(Roles = "User")]
    public class ConsultsController : ApiController
    {
        readonly Service service = new Service();


        [Route("general/{consultId:Int}")]
        [HttpGet]
        public GeneralConsult Get(int consultId) => service.GetGeneralConsult(consultId);

        [Route("gynecology/{consultId:Int}")]
        [HttpGet]
        public GynecologyConsult GetGyne(int consultId) => service.GetGyneConsult(consultId);

        [Route("general/{doctorId:Int}")]
        [HttpPost]
        public IHttpActionResult Post(int doctorId, [FromBody]GeneralConsult req)
        {
            if (service.SaveGeneralConsult(doctorId, req))
                return Ok();
            else return NotFound();
        }

        [Route("gynecology/{doctorId:Int}")]
        [HttpPost]
        public IHttpActionResult Post(int doctorId, [FromBody]GynecologyConsult req)
        {
            if (service.SaveGynecologyConsult(doctorId, req))
                return Ok();
            else return NotFound();
        }

        [Route("general/dates/{pacientId:Int}")]
        [HttpGet]
        public IEnumerable<ConsultationDates> ConsultationsDates(int pacientId)
        {
            return service.GetConsultationDates(pacientId);
        }

        [Route("gynecology/dates/{pacientId:Int}")]
        [HttpGet]
        public IEnumerable<ConsultationDates> GyneConsultationsDates(int pacientId)
        {
            return service.GetDatesGynecologyConsults(pacientId);
        }
    }
}
