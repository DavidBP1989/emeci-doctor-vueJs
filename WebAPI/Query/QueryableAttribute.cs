﻿using System.Collections;
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
            if (actionExecutedContext.Request.Properties.ContainsKey("x-total-count"))
            {
                int.TryParse(actionExecutedContext.Request.Properties["x-total-count"].ToString(), out int value);

                actionExecutedContext.Response.Headers.Add("Access-Control-Expose-Headers", "X-Total-Count");
                actionExecutedContext.Response.Headers.Add("X-Total-Count", value.ToString());
            }
            
            base.OnActionExecuted(actionExecutedContext);
        }
    }
}