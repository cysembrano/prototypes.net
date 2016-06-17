using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowFB.Repository.Models
{
    public enum CodeStatus
    {
        New = 1,
        Edited = 2,
        Synced = 3,
    }

    public abstract class FACode
    {
        public int Id { get; set; } 
        public String Code { get; set; }
        public String Description { get; set; }
        public int ProjectID { get; set; }
        public CodeStatus Status { get; set; }
    }
    public class FAGLCode : FACode  { }
    public class FATaxCode : FACode { }
    public class FACostCenterCode : FACode { }
}
