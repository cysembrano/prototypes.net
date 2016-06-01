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
        public string Get(int id)
        {
            return data[id];
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
            data.Add(value);
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            data.RemoveAt(id);
        }
    }
}