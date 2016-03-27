using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convergys.Assist.Repository;
using Convergys.Assist.Repository.BusinessModels;

namespace Convergys.Assist.MockBusinessLogic
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        public Employee ViewEmployeeByEmpNumber(EmployeeView securityCard, int EmpNumber)
        {
            return new Employee()
            {
                EmpId = 697,
                EmpNumber = EmpNumber,
                FirstName = "Cyrus Sembrano",
                LastName = String.Empty,
                ManagerId = 099900M,
                MiddleName = "V",
                NickName = "Cy",
                SiteId = 55M,
                TeamId = 123
            };


        }

        public EmployeeView LogonEmployee(string emailAddress, string clearPassword)
        {
            return new EmployeeView() 
            { 
                AppEntitlement = "Callback",
                EmailAddress = "cyrus.sembrano@convergys.com",
                EmpId = 95744M,
                EmpNumber = 95744,
                FirstName = "Cyrus Sembrano"
                
            };
        }

        public Team[] ViewTeams(EmployeeView securityCard)
        {
            throw new NotImplementedException();
        }

        public EmployeeTeamView[] ViewEmployeesByTeam(EmployeeView securityCard, int? TeamId)
        {
            throw new NotImplementedException();
        }

        public EmployeeView ChangePassword(EmployeeView securityCard, decimal empId, string OldPwd, string NewPwd, out bool Success)
        {
            throw new NotImplementedException();
        }

        public EmployeeView ChangeTimezone(EmployeeView securityCard, decimal empId, string selectedTimezone, bool dst, out bool Success)
        {
            throw new NotImplementedException();
        }

        public EmployeeView ChangeRole(EmployeeView securityCard, decimal empId, Repository.Enums.Enum_Roles role, out bool Success)
        {
            throw new NotImplementedException();
        }


        public EmployeeTeamView[] ViewEmployeesByTeamWithRole(EmployeeView securityCard, int? TeamId)
        {
            throw new NotImplementedException();
        }
    }
}
