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
    public class ActionController : ApiController
    {
        // GET api/values
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse<string>(HttpStatusCode.OK, @"Usage: ~/Action/{GUID} | Output: GUID logID");
        }

        // GET api/values/5
        public HttpResponseMessage Get(string id)
        {
            if (!String.IsNullOrWhiteSpace(id))
            {
                string output;
                bool result = FlowAPIObject.ExecuteActionSyncWithParams(id,null,out output);
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

        // GET api/values/5
        public HttpResponseMessage Get(string id, string input)
        {
            if (!String.IsNullOrWhiteSpace(id))
            {
                string output;
                bool result = FlowAPIObject.ExecuteActionSyncWithParams(id, input, out output);
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


        // POST api/values
        public HttpResponseMessage Post(string id, [FromBody]string value)
        {

            if (!String.IsNullOrWhiteSpace(id))
            {
                string output;
                bool result = FlowAPIObject.ExecuteActionSyncWithParams(id, value, out output);
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