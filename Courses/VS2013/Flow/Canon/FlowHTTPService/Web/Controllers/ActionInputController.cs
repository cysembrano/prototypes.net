using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web.FlowCOM;
using Web.Logging;

namespace Web.Controllers
{
    public class ActionInputController : ApiController
    {
        // GET api/values
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse<string>(HttpStatusCode.OK, @"Usage: ~/Action/{GUID} | Output: GUID logID");
        }


        // POST api/values
        public HttpResponseMessage Post(string id, [FromBody]string value)
        {
            if (!String.IsNullOrWhiteSpace(id))
            {
                string output, logFid;
                bool result = FlowAPIObject.ExecuteSOAPRequestIn(id, value, out output, out logFid);
                if (!result)
                {
                    Log4NetManager.Instance.Error(this.GetType(), String.Format("Flow Action {0} was not called.", id));
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Flow Action was not called.");
                }

                return Request.CreateResponse<string>(HttpStatusCode.OK, output);

            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Action not found");
            }

        }

    }
}