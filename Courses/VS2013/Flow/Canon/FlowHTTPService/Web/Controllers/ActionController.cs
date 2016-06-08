using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Xml.Linq;
using System.Xml.XPath;
using Web.FlowCOM;
using Web.Logging;

namespace Web.Controllers
{
    public class ActionController : ApiController
    {
        // GET api/values
        public HttpResponseMessage Get(string id, string value)
        {
            return ProcessAction("GET", id, value);
        }

        // POST api/values
        public HttpResponseMessage Post(string id, [FromBody]string value)
        {
            return ProcessAction("POST", id, value);
        }

        private HttpResponseMessage ProcessAction(string verb, string id, string inputvalue)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("");
            sb.AppendLine(String.Format(@"{0} VERB BEGINS: {1}\{2} {3}", verb, "Action", id, DateTime.Now));
            if (!String.IsNullOrWhiteSpace(id))
            {


                string output, logFid;
                bool result = FlowAPIObject.ExecuteSOAPRequestIn(id, inputvalue, out output, out logFid);
                sb.AppendLine(String.Format(@"{0} VERB INPUT: {1}",verb, inputvalue));

                string modoutput = GarnishResponseValue(output);
                sb.AppendLine(String.Format(@"{0} VERB OUTPUT: {1}",verb, modoutput));


                if (!result)
                {
                    Log4NetManager.Instance.Error(this.GetType(), String.Format("Flow Action {1} was not called.", id));
                    sb.AppendLine(String.Format(@"{0} VERB ENDS [ERROR-FlowActionCall]: {1}\{2} {3}",verb, "Action", id, DateTime.Now));
                    Log4NetManager.Instance.Error(this.GetType(), sb);
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Flow Action was not called.");

                }
                sb.AppendLine(String.Format(@"{0} VERB ENDS [SUCCESS]: {1}\{2} {3}", verb, "Action", id, DateTime.Now));
                Log4NetManager.Instance.Info(this.GetType(), sb);
                return Request.CreateResponse<string>(HttpStatusCode.OK, modoutput);



            }
            else
            {
                sb.AppendLine(String.Format(@"{0} VERB ENDS [ERROR-NotFound]: {1}\{2} {3}", "Action", id, DateTime.Now));
                Log4NetManager.Instance.Error(this.GetType(), sb);
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Action not found");

            }

        }

        /// <summary>
        /// Removes the 
        /// </summary>
        private string GarnishResponseValue(string xmlResponse)
        {
            bool doGarnish = false;
            bool tryParseResult = Boolean.TryParse(ConfigurationManager.AppSettings["GarnishXmlEnabled"], out doGarnish);
            if(tryParseResult && doGarnish)
            {
                var document = XDocument.Parse(xmlResponse);
                return document.XPathSelectElement(ConfigurationManager.AppSettings["GarnishXmlXPath"]).Value;
            }
            else
            {
                return xmlResponse;
            }
            
        }

    }
}