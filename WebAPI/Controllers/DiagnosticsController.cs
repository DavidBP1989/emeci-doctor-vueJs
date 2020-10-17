using System.Collections.Generic;
using System.Web.Http;
using WebAPI.Models.DTO._Diagnostic;
using WebAPI.Services._Diagnostic;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/diagnostics")]
    [Authorize(Roles = "User")]
    public class DiagnosticsController : ApiController
    {
        private readonly Service service = new Service();


        [Route("{doctorId:Int}")]
        [HttpGet]
        //GET /api/diagnostics/1400
        public IEnumerable<Diagnostics> Get(int doctorId)
        {
            return service.Get(doctorId);
        }


        [Route("{doctorId:Int}")]
        [HttpPost]
        //POST /api/diagnostics/1400
        public DiagnosticsRes Post(int doctorId, [FromBody]DiagnosticsReq req)
        {
            return service.Save(doctorId, req);
        }


        [Route("{id:Int}")]
        [HttpDelete]
        //DELETE /api/diagnostics/123
        public IHttpActionResult Delete(int id)
        {
            if (service.Delete(id))
                return Ok();
            return NotFound();
        }
    }
}
