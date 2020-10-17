using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using WebAPI.Models.DTO._Doctor;
using WebAPI.Services;
using WebAPI.Services._Base;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/wAuthority")]
    //[EnableCors(origins: "https://localhost:44303", headers: "*", methods: "*")]
    public class WithoutAuthorityController : ApiController
    {
        private readonly Service service = new Service();


        [HttpGet]
        [Route("forgotpwd/{emeci}")]
        //GET /api/wAuthority/forgotpwd/03240-0001
        public IHttpActionResult ForgotPassword(string emeci)
        {
            var result = service.ForgotPassword(emeci);
            if (!string.IsNullOrEmpty(result.Password) && !string.IsNullOrEmpty(result.Email))
            {
                var emailService = new EmailService(result.Email);
                Task.Run(async () => { 
                    await emailService.SendForgotPwd(result);
                });
                return Ok();
            }
            return NotFound();
        }

        [Route("doctorregister")]
        [HttpPost]
        //POST /api/wAuthority/doctorregister
        public IHttpActionResult Register( [FromBody] Register registerReq)
        {
            if (service.DoctorRegister(registerReq))
            {
                var emailService = new EmailService(registerReq.Email);
                Task.Run(async () =>
                {
                    await emailService.SendDoctorRegister(registerReq);
                });
                return Ok();
            }
            return NotFound();
        }


        [Route("states")]
        [HttpGet]
        //GET /api/wAuthority/states
        public IEnumerable<States> GetStates()
        {
            return service.GetStates();
        }


        [Route("cities/{stateId}")]
        [HttpGet]
        //GET /api/wAuthority/cities/BS
        public IEnumerable<Cities> GetCities(string stateId)
        {
            return service.GetCities(stateId);
        }
    }
}
