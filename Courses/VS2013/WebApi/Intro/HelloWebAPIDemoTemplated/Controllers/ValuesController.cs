using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HelloWebAPIDemoTemplated.Controllers
{
    public class ValuesController : ApiController
    {
        static List<string> data = initList();

        private static List<string> initList()
        {
            var ret = new List<string>();
            ret.Add("val1");
            ret.Add("val2");
            return ret;
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            return data;
        }

        // GET api/values/5
        public HttpResponseMessage Get(int id)
        {
            if (data.Count > id)
                return Request.CreateResponse<string>(HttpStatusCode.OK, data[id]);
            else
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Item not found");
        }

        // POST api/values
        public HttpResponseMessage Post([FromBody]string value)
        {
            data.Add(value);
            var msg = Request.CreateResponse(HttpStatusCode.Created);
            var id = data.Count - 1;
            msg.Headers.Location = new Uri(Request.RequestUri + "/" + id.ToString());
            return msg;
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            data.RemoveAt(id);
        }
    }
}