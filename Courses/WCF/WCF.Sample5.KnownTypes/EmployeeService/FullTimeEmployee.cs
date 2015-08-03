using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EmployeeService
{
    [DataContract(Namespace = "http://cymessageboards.com/2015/10/06/FullTimeEmployee")]
    public class FullTimeEmployee : Employee
    {
        [DataMember]
        public int AnnualSalary { get; set; }
    }
}
