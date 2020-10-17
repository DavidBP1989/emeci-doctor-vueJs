using System.Collections.Generic;
using System.Web.Http;
using WebAPI.Models.DTO._Studies;
using WebAPI.Services._Studies;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/laboratory")]
    [Authorize(Roles = "User")]
    public class StudiesLaboratoryController : ApiController
    {
        private readonly Service service = new Service();

        [Route("")]
        [HttpGet]
        //GET /api/laboratory
        public IEnumerable<CategoryStudies> Get()
        {
            return service.GetStudies(0);
        }
    }
}
