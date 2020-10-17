using System.Web.Http;
using WebAPI.Models.DTO._Doctor;
using WebAPI.Services._Doctor;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/doctor/{doctorId:Int}")]
    [Authorize(Roles = "User")]
    public class DoctorController : ApiController
    {
        private readonly Service service = new Service();

        [Route("")]
        [HttpGet]
        //GET /api/doctor/1400
        public Doctor Get(int doctorId)
        {
            return service.Get(doctorId);
        }

        [Route("registerInfo")]
        [HttpGet]
        //GET /api/doctor/registerInfo/1400
        public RegisterInfo GetRegisterInfo(int doctorId)
        {
            return service.GetRegisterInfo(doctorId);
        }

        [Route("changePwd")]
        [HttpPut]
        //POST /api/doctor/1400/changePwd
        public ChangePwdRes ChangePassword(int doctorId, [FromBody]ChangePwdReq changePwdReq)
        {
            return service.ChangePassword(doctorId, changePwdReq);
        }

        [Route("")]
        [HttpPut]
        //POST /api/doctor/1400
        public IHttpActionResult Put(int doctorId, [FromBody]RegisterInfo req)
        {
            if (service.UpdateRegisterInfo(doctorId, req))
                return Ok();
            return NotFound();
        }
    }
}
