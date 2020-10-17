using System.Collections;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Filters;
using WebAPI.Models.DTO._Patient;

namespace WebAPI.Query
{
    public class QueryableAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            actionExecutedContext.Response.TryGetContentValue(out IEnumerable model);
            if (model != null)
            {
                IQueryable modelQuery = model.AsQueryable();

                var count = (modelQuery as IQueryable<Patients>).Count();
                actionExecutedContext.Response.Headers.Add("Access-Control-Expose-Headers", "X-Total-Count");
                actionExecutedContext.Response.Headers.Add("X-Total-Count", count.ToString());
            }

            base.OnActionExecuted(actionExecutedContext);
        }
    }
}