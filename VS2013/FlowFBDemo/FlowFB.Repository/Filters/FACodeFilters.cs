using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowFB.Repository.Filters
{
    public abstract class FACodeFilter
    {
        public string Project { get; set; }

        public FACodeFilter(string project)
        {
            Project = project;
        }
    }
    public class FAGLCodeFilter : FACodeFilter
    {
        public FAGLCodeFilter() : base(project: "GLCodes") { }
    }
    public class FATaxCodeFilter : FACodeFilter
    {
        public FATaxCodeFilter() : base(project: "TaxCodes") { }
    }
    public class FACostCenterCodeFilter : FACodeFilter
    {
        public FACostCenterCodeFilter() : base(project: "CostCenter") { }
    }
}
