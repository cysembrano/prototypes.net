using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convergys.Assist.Repository.BusinessModels;
using Convergys.Assist.Repository.Enums;

namespace Convergys.Assist.Repository
{
    public interface IEmployeeRepository
    {
        /// <summary>
        /// Views employee given an employee number.
        /// Role of Support Professional or Higher.
        /// </summary>
        Employee ViewEmployeeByEmpNumber(EmployeeView securityCard, int EmpNumber);

        Team[] ViewTeams(EmployeeView securityCard);

        EmployeeTeamView[] ViewEmployeesByTeam(EmployeeView securityCard, int? TeamId);

        EmployeeTeamView[] ViewEmployeesByTeamWithRole(EmployeeView securityCard, int? TeamId);

        /// <param name="empId">empId To change</param>
        EmployeeView ChangePassword(EmployeeView securityCard, decimal empId, string OldPwd, string NewPwd, out bool Success);
        /// <param name="empId">empId To change</param>
        EmployeeView ChangeTimezone(EmployeeView securityCard, decimal empId, string selectedTimezone, bool dst, out bool Success);
        /// <param name="empId">empId To change</param>
        EmployeeView ChangeRole(EmployeeView securityCard, decimal empId, Enum_Roles role, out bool Success);

    }
}
