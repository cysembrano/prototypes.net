using FlowFB.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlowFB.Web.Models
{
    public class CodesModel : BaseModel
    {
        public IEnumerable<FACode> FACodes { get; set; }
    }

    public class MyRow
    {
        public string ID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}