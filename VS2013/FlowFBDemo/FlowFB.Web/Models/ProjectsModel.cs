using FlowFB.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlowFB.Web.Models
{
    public class ProjectsModel : BaseModel
    {
        public IEnumerable<FBProject> Projects { get; set; }
    }
}