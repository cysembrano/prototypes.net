using System;
using Convergys.Assist.BusinessLogic.SQLRepository;
using Convergys.Assist.Repository.BusinessModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Convergys.Assist.Test
{
    [TestClass]
    public class EmployeeSQLTest
    {
        [TestMethod]
        public void GetEmployeeByNumber_ReturnEmployee()
        {
            EmployeeRepository repo = new EmployeeRepository();
            var emp = repo.ViewEmployeeByEmpNumber(new EmployeeView() { RoleId = 5 }, 697);

            Assert.AreEqual<string>(emp.FirstName, "Sherman Lackey");
        }

        [TestMethod]
        public void LogonEmployee_ReturnEmployeeView()
        {
            PublicRepository repo = new PublicRepository();
            var emp = repo.LoginEmployee("Rhonda.Koop@Stream.com", "5mQmSnQf");

            Assert.AreEqual<string>(emp.ManagerName, "Matthew Skuodas");
            Assert.AreEqual<string>(emp.Team, "CHL Redbox Voice - Chilliwack");
            Assert.AreEqual<string>(emp.FirstName, "Rhonda Koop");
        }

        [TestMethod]
        public void ChangePassword_ReturnTrue()
        {
            PublicRepository repo = new PublicRepository();
            EmployeeRepository emprepo = new EmployeeRepository();
            var emp = repo.LoginEmployee("Rhonda.Koop@Stream.com", "pass1234");
            bool success;
            EmployeeView result = emprepo.ChangePassword(securityCard: emp, empId: emp.EmpId, 
                OldPwd: "pass1234", NewPwd: "pass1233", Success: out success);
            EmployeeView found = repo.LoginEmployee("Rhonda.Koop@Stream.com", "pass1233");

            Assert.AreEqual<string>(emp.ManagerName, found.ManagerName);
            Assert.AreEqual<string>(emp.Team, found.Team);
            Assert.AreEqual<string>(emp.FirstName, emp.FirstName);
            Assert.AreEqual<bool>(true, success);

            Assert.AreEqual<string>(found.ManagerName, result.ManagerName);
            Assert.AreEqual<string>(found.Team, result.Team);
            Assert.AreEqual<string>(found.NewPwd, result.NewPwd);


            //Revert Password;
            emprepo.ChangePassword(emp, emp.EmpId, "pass1233", "pass1234", out success);
            
        }
        [TestMethod]
        public void ChangeTimezone_ReturnTrue()
        {
            PublicRepository repo = new PublicRepository();
            EmployeeRepository emprepo = new EmployeeRepository();

            var emp = repo.LoginEmployee("Rhonda.Koop@Stream.com", "pass1234");
            bool success;
            EmployeeView result = emprepo.ChangeTimezone(emp, emp.EmpId, "06.00", true, out success);

            Assert.AreEqual<decimal>(result.HrDifference.Value, 6.00M + 1);
            Assert.AreEqual<bool>(result.TimeZoneDST.Value, true);
        }
    }
}
