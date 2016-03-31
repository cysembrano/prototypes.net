using System;
using Convergys.Assist;
using Convergys.Assist.MockBusinessLogic;
using Convergys.Assist.Repository.BusinessModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Convergys.Assist.Test
{
    [TestClass]
    public class EmployeeMockTest
    {
        [TestMethod]
        public void MockGetEmployeeByNumber_ReturnEmp()
        {
            MockEmployeeRepository repo = new MockEmployeeRepository();
            var emp = repo.ViewEmployeeByEmpNumber(new EmployeeView(){ RoleId = 5 }, 697);

            Assert.AreEqual<int>(emp.EmpNumber.GetValueOrDefault(), 697);
        }
    }
}
