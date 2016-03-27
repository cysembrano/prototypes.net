using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convergys.Assist.Data;
using Convergys.Assist.Repository;
using Convergys.Assist.Repository.BusinessModels;
using Convergys.Assist.BusinessLogic.Extensions;
using Convergys.Assist.Shared.Util;
using Convergys.Assist.Repository.Enums;
using Convergys.Assist.Logging;

namespace Convergys.Assist.BusinessLogic.SQLRepository
{
    public class EmployeeRepository : SQLRepositoryBase, IEmployeeRepository
    {
        public Employee ViewEmployeeByEmpNumber(EmployeeView securityCard, int EmpNumber)
        {
            if (securityCard == null) throw new ArgumentNullException("securityCard");

            using (SADataModel context = new SADataModel())
            {
                var emp = context.CAEmployee.FirstOrDefault(b => b.EmpNumber == EmpNumber);

                if (emp != null)
                {
                    var modelEmp = new Employee();
                    modelEmp.EmpId = emp.EmpId;
                    modelEmp.EmpNumber = emp.EmpNumber;
                    modelEmp.FirstName = emp.FirstName;
                    modelEmp.LastName = emp.LastName;
                    modelEmp.MiddleName = emp.MiddleName;
                    modelEmp.NickName = emp.NickName;
                    modelEmp.SiteId = emp.SiteId;
                    modelEmp.TeamId = emp.TeamId;

                    if (emp.CASite != null)
                    {
                        modelEmp.Site = new Site();
                        modelEmp.Site.CountryId = emp.CASite.CountryId;
                        modelEmp.Site.Location = emp.CASite.Location;
                        modelEmp.Site.RegionId = emp.CASite.RegionId;
                        modelEmp.Site.SiteAbbreviation = emp.CASite.SiteAbbreviation;
                        modelEmp.Site.SiteId = emp.CASite.SiteId;
                        modelEmp.Site.SiteName = emp.CASite.SiteName;
                        modelEmp.Site.SiteOffset = emp.CASite.SiteOffset;
                    }

                    return modelEmp;

                }
                return null;
            }
        }

        public Team[] ViewTeams(EmployeeView securityCard)
        {
            Team[] teams = new Team[] { };
            using (SADataModel context = new SADataModel())
            {
                if (securityCard.RoleId >= Enum_Roles.ITAdmin.GetHashCode()) //For ITAdmin Show all teams
                    teams = context.CATeam.OrderBy(g => g.Team).ToTeams();
                else if (securityCard.RoleId >= Enum_Roles.TeamManager.GetHashCode())  //For Managers, just teams they manage
                    teams = context.CAListEmpTeams
                        .Where(b => b.ManagerId == securityCard.EmpId)
                        .ToTeams().Distinct().ToArray();
                if(teams.Length < 1)      //For everyone else just show their team.
                    teams = new Team[] { new Team() { TeamId = securityCard.TeamId.GetValueOrDefault(), TeamName = securityCard.Team } };

            }
            return teams;
        }

        public EmployeeTeamView[] ViewEmployeesByTeam(EmployeeView securityCard, int? teamId)
        {
            if (securityCard == null) throw new ArgumentNullException("securityCard");

            EmployeeTeamView[] teams = new EmployeeTeamView[] { };
            using (SADataModel context = new SADataModel())
            {
                int tempTeamId;
                if (teamId.HasValue && securityCard.RoleId >= Enum_Roles.TeamManager.GetHashCode())
                    tempTeamId = teamId.Value;
                else
                    tempTeamId = securityCard.TeamId.Value;

                teams = context.CAListEmpTeams.Where(h => h.TeamID == tempTeamId).OrderBy(b => b.FirstName).ToEmployeeTeamViews();
            }

            return teams;
        }

        public EmployeeView ChangePassword(EmployeeView securityCard, decimal empId, string OldPwd, string NewPwd, out bool Success)
        {
            if (securityCard == null) throw new ArgumentNullException("securityCard");

            using (SADataModel dbContext = new SADataModel())
            {
                string encOldPwd = Crypto.Encrypt(OldPwd);

                var found = dbContext.CAPassword.Where(d => d.NewPwd == encOldPwd && d.EmpId == empId).FirstOrDefault();

                int records = 0;
                if (found != null)
                {
                    found.OldPwd = found.NewPwd;
                    found.NewPwd = Crypto.Encrypt(NewPwd);
                    records = dbContext.SaveChanges();
                }

                if (records > 0)
                {
                    Success = true;
                    Log4NetManager.Instance.Info(this.GetType(), String.Format("Change of Timezone for ({1}) successful.", empId));
                }
                else
                {
                    Success = false;
                    Log4NetManager.Instance.Warn(this.GetType(), String.Format("Change of Timezone for ({1}) failed.", empId));
                }


                return dbContext.CAEmployeeView.FirstOrDefault(g => g.EmpId == empId).ToEmployeeView();
            }

        }

        public EmployeeView ChangeTimezone(EmployeeView securityCard, decimal empId, string selectedTimezone, bool dst, out bool Success)
        {
            if (securityCard == null) throw new ArgumentNullException("securityCard");

            using (SADataModel dbContext = new SADataModel())
            {
                decimal decSelectedTimezone = Decimal.Parse(selectedTimezone);
                var timeZone = dbContext.CATimezone.FirstOrDefault(b => b.HrDifference == decSelectedTimezone);
                var preferences = dbContext.CAPreferences.FirstOrDefault(g => g.EmpId == empId);
                if (timeZone == null)
                    Success = false;
                else if (preferences == null)
                    Success = false;
                else
                {
                    preferences.TimeZoneDST = dst;
                    preferences.TimeZoneId = timeZone.TimezoneId;
                    preferences.DateChanged = DateTime.Now;
                    int records = dbContext.SaveChanges();
                    if (records > 0)
                    {
                        Success = true;
                        Log4NetManager.Instance.Info(this.GetType(), String.Format("Change of Timezone to ({0}) for ({1}) successful.", decSelectedTimezone, empId));
                    }
                    else
                    {
                        Success = false;
                        Log4NetManager.Instance.Warn(this.GetType(), String.Format("Change of Timezone to ({0}) for ({1}) failed.", decSelectedTimezone, empId));
                    }
                }

                return dbContext.CAEmployeeView.FirstOrDefault(g => g.EmpId == empId).ToEmployeeView();
            }
        }

        public EmployeeView ChangeRole(EmployeeView securityCard, decimal empId, Enum_Roles role, out bool Success)
        {
            if (securityCard == null) throw new ArgumentNullException("securityCard");
            using (SADataModel dbContext = new SADataModel())
            {                
                var found = dbContext.CAPermission.FirstOrDefault(d => d.EmpId == empId);

                int records = 0;
                if (found != null)
                {
                    found.RoleId = role.GetHashCode();
                    records = dbContext.SaveChanges();
                }

                if (records > 0)
                {
                    Success = true;
                    Log4NetManager.Instance.Info(this.GetType(), String.Format("Change of Role to ({0}) for ({1}) successful.", role, empId));
                }
                else
                {
                    Success = false;
                    Log4NetManager.Instance.Warn(this.GetType(), String.Format("Change of Role to ({0}) for ({1}) failed.", role ,empId));
                }
                   
                return dbContext.CAEmployeeView.FirstOrDefault(g => g.EmpId == empId).ToEmployeeView();
            }            
        }

        public EmployeeTeamView[] ViewEmployeesByTeamWithRole(EmployeeView securityCard, int? TeamId)
        {
            EmployeeTeamView[] all = ViewEmployeesByTeam(securityCard, TeamId);

            List<EmployeeTeamView> filtered = new List<EmployeeTeamView>();
            foreach (var mem in all)
            {
                if (mem.RoleId <= securityCard.RoleId)
                    filtered.Add(mem);
            }

            return filtered.ToArray();
        }
    }
}
