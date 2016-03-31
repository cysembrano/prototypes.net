using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convergys.Assist.Data;
using Convergys.Assist.Repository.BusinessModels;

namespace Convergys.Assist.BusinessLogic.Extensions
{
    public static partial class CAEmployeeExtension
    {
        public static EmployeeView ToEmployeeView(this CAEmployeeView caEmployeeView)
        {
            if (caEmployeeView != null)
            {
                var emp = new EmployeeView()
                {
                    AppEntitlement = caEmployeeView.AppEntitlement,
                    EmailAddress = caEmployeeView.EmailAddress,
                    HrDifference = caEmployeeView.HrDifference,
                    EmpId = caEmployeeView.EmpId,
                    EmpNumber = caEmployeeView.EmpNumber,
                    FirstName = caEmployeeView.FirstName,
                    JobTitle = caEmployeeView.JobTitle,
                    ManagerId = caEmployeeView.ManagerId,
                    ManagerName = caEmployeeView.ManagerName,
                    NewPwd = caEmployeeView.NewPwd,
                    RoleId = caEmployeeView.RoleId,
                    SiteAbbreviation = caEmployeeView.SiteAbbreviation,
                    SiteId = caEmployeeView.SiteId,
                    SiteName = caEmployeeView.SiteName,
                    Team = caEmployeeView.Team,
                    TeamId = caEmployeeView.TeamId,
                    Timezone = caEmployeeView.Timezone,
                    TimeZoneDST = caEmployeeView.TimeZoneDST,
                    TimeZoneId = caEmployeeView.TimeZoneId
                };
                if (emp.TimeZoneDST.HasValue && emp.TimeZoneDST.Value && emp.HrDifference.HasValue)
                {
                    emp.HrDifference = emp.HrDifference + 1;
                }
                return emp;
            }
            return null;
        }

        public static Team[] ToTeams(this IEnumerable<CATeam> caTeams)
        {
            return caTeams.Select(b => b.ToTeam()).ToArray();
        }

        public static Team ToTeam(this CATeam caTeam)
        {
            return new Team()
            {
                TeamId = caTeam.TeamId,
                TeamName = caTeam.Team,
                SiteId = caTeam.SiteId
            };
        }

        public static Team[] ToTeams(this IEnumerable<CAListEmpTeams> caListEmpTeams)
        {
            return caListEmpTeams.Select(h => h.ToTeam()).ToArray();
        }

        public static Team ToTeam(this CAListEmpTeams caListEmpTeam)
        {
            return new Team()
            {
                TeamId = caListEmpTeam.TeamID,
                TeamName = caListEmpTeam.Team
            };
        }

        public static EmployeeTeamView[] ToEmployeeTeamViews(this IEnumerable<CAListEmpTeams> caListEmpTeams)
        {
            return caListEmpTeams.Select(b => b.ToEmployeeTeamView()).ToArray();
        }

        public static EmployeeTeamView ToEmployeeTeamView(this CAListEmpTeams caListEmpTeam)
        {
            return new EmployeeTeamView()
            {
                EmpId = caListEmpTeam.EmpID,
                EmpNumber = caListEmpTeam.EmpNumber,
                FirstName = caListEmpTeam.FirstName,
                ManagerId = caListEmpTeam.ManagerId,
                TeamId = caListEmpTeam.TeamID,
                TeamName = caListEmpTeam.Team,
                RoleId = caListEmpTeam.RoleId
            };
        }

        public static Employee[] ToEmployees(this IEnumerable<CAEmployee> caEmployees)
        {
            return caEmployees.Select(b => b.ToEmployee()).ToArray();
        }

        public static Employee ToEmployee(this CAEmployee caEmployee)
        {
            return new Employee()
            {
                EmpId = caEmployee.EmpId,
                EmpNumber = caEmployee.EmpNumber,
                FirstName = caEmployee.FirstName,
                LastName = caEmployee.LastName,
                ManagerId = caEmployee.ManagerId,
                MiddleName = caEmployee.MiddleName,
                NickName = caEmployee.NickName,
                SiteId = caEmployee.SiteId,
                TeamId = caEmployee.TeamId
            };
        }
    }
}
