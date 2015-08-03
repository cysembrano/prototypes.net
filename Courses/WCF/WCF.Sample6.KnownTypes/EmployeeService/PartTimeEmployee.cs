using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EmployeeService
{
    [DataContract(Namespace = "http://cymessageboards.com/2015/10/06/PartTimeEmployee")]
    public class PartTimeEmployee : Employee
    {
        [DataMember]
        public int HourlyPay { get; set; }
        [DataMember]
        public int HoursWorked { get; set; }
    }
}
