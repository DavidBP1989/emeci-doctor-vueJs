using System.Collections.Generic;
using System.Web.Http;
using WebAPI.Models.DTO._Studies;
using WebAPI.Services._Studies;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/cabinet")]
    [Authorize(Roles = "User")]
    public class StudiesCabinetController : ApiController
    {
        private readonly Service service = new Service();

        [Route("")]
        [HttpGet]
        //GET /api/laboratory
        public IEnumerable<CategoryStudies> Get()
        {
            return service.GetStudies(1);
        }
    }
}
