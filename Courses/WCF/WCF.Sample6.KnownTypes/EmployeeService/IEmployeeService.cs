using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace EmployeeService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEmployeeService" in both code and config file together.
    [ServiceContract]
    public interface IEmployeeService
    {
        //[ServiceKnownType(typeof(FullTimeEmployee))]
        //[ServiceKnownType(typeof(PartTimeEmployee))]
        [OperationContract]
        Employee GetEmployee(int Id);


        //This will error since it will be serializing a child of the employee.
        //Need to add ServiceKnownType here too
        [OperationContract]
        void SaveEmployee(Employee employee);
    }
}
