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
        [OperationContract]
        Employee GetEmployee(int Id); //Adding/removing parameter/s will not affect existing clients.

        [OperationContract]
        void SaveEmployee(Employee employee);

        //Adding operation/s will not affect clients
        //Removing operations might affect clients
    }
}
