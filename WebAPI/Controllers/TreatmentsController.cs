using System.Collections.Generic;
using System.Web.Http;
using WebAPI.Models.DTO._Treatment;
using WebAPI.Services._Treatment;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/treatments")]
    [Authorize(Roles = "User")]
    public class TreatmentsController : ApiController
    {
        private readonly Service service = new Service();


        [Route("{doctorId:Int}")]
        [HttpGet]
        //GET /api/treatments/1400
        public IEnumerable<Treatments> Get(int doctorId)
        {
            return service.Get(doctorId);
        }


        [Route("{doctorId:Int}")]
        [HttpPost]
        //POST /api/treatments/1400
        public TreatmentRes Post(int doctorId, [FromBody] TreatmentReq req)
        {
            return service.Save(doctorId, req);
        }


        [Route("{id:Int}")]
        [HttpDelete]
        //DELETE /api/treatments/123
        public IHttpActionResult Delete(int id)
        {
            if (service.Delete(id))
                return Ok();
            return NotFound();
        }
    }
}
