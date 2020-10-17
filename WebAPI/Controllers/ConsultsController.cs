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
        //GET /api/consults/general/123
        public GeneralConsult Get(int consultId)
        {
            return service.GetGeneralConsult(consultId);
        }


        [Route("general/{doctorId:Int}")]
        [HttpPost]
        //POST /api/consults/general/1400
        public IHttpActionResult Post(int doctorId, [FromBody]GeneralConsult req)
        {
            if (service.SaveGeneralConsult(doctorId, req))
                return Ok();
            else return NotFound();
        }


        [Route("general/dates/{pacientId:Int}")]
        [HttpGet]
        //GET /api/consults/general/dates/123
        public IEnumerable<ConsultationDates> ConsultationsDates(int pacientId)
        {
            return service.GetConsultationDates(pacientId);
        }
    }
}
