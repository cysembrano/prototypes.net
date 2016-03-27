using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convergys.Assist.Sched
{
    public class ScheduleProcessor
    {
        public IEnumerable<string> EmpIds { get; set; }

        public ScheduleProcessor()
        {
        }

        public void StartLoadByTeam(int TeamId, Action<string> updateUI)
        {
            var Tasker = Task.Factory.StartNew(() =>
            {
                Logger.Instance.Info("StartLoadByTeam: Start Execution", updateUI);
                LoadByTeam(TeamId, updateUI);
                Logger.Instance.Info("StartLoadByTeam: End Execution", updateUI);

            }).ContinueWith((t) =>
                {
                    if (t.Exception != null)
                    {
                        var exs = t.Exception.Flatten().InnerExceptions;
                        foreach (var e in exs)
                            Logger.Instance.Error("StartLoadByTeam: End Exception - " + e.Message, updateUI);
                    }
                }, TaskContinuationOptions.OnlyOnFaulted);
        }

        private void LoadByTeam(int TeamId, Action<string> updateUI)
        {
            Logger.Instance.Info("LoadByTeam: Start ", updateUI);
            var empIds = GetEmpIds(TeamId, updateUI); //Get List of EmpIds
            LoadByEmpIds(empIds, updateUI);
            Logger.Instance.Info("LoadByTeam: End ", updateUI);
        }

        public void StartLoadByEmpId(IEnumerable<int> empIds, Action<string> updateUI)
        {
            var Tasker = Task.Factory.StartNew(() =>
            {
                Logger.Instance.Info("StartLoadByEmpId: Start Execution", updateUI);
                LoadByEmpIds(empIds, updateUI);
                Logger.Instance.Info("StartLoadByEmpId: End Execution", updateUI);

            }).ContinueWith((t) =>
            {
                if (t.Exception != null)
                {
                    var exs = t.Exception.Flatten().InnerExceptions;
                    foreach (var e in exs)
                        Logger.Instance.Error("StartLoadByEmpId: End Exception - " + e.Message, updateUI);
                }
            }, TaskContinuationOptions.OnlyOnFaulted);
        }

        private void LoadByEmpIds(IEnumerable<int> empIds, Action<string> updateUI)
        {
            Logger.Instance.Info("LoadByEmpIds: Start ", updateUI);
            foreach (var id in empIds)
            {
                Schedule sched = GetSchedule(id.ToString(), updateUI); //Get Schedule from RTA
                if (sched != null)
                {
                    var existingId = IsScheduleExisting(id, updateUI);
                    if (existingId.HasValue)  //Check if Schedule exists in StreamAssist
                        DeleteScheduleExisting(existingId.Value, updateUI);//If existing, delete it first before existing.

                    sched.Id = InsertSchedule(sched, updateUI).Value;
                    List<ScheduleLine> lines = GetLinesFromSchedule(sched);
                    foreach (var lin in lines)
                    {
                        InsertScheduleLine(lin, updateUI);
                    }
                }
            }
            Logger.Instance.Info("LoadByEmpIds: End ", updateUI);
        }

        private List<int> GetEmpIds(int TeamId, Action<string> updateUI)
        {
            List<int> empIds = new List<int>();
            using (var conn = new SqlConnection(Settings.Instance.SAConnection))
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("SELECT DISTINCT(ET.EmpID) ");
                    sb.Append("FROM streamassist_dbo.tblEmpTeams ET ");
                    sb.Append("INNER JOIN streamassist_dbo.tblEmp E on ");
                    sb.Append("E.empid = ET.empID or E.empnumber = ET.EmpID ");
                    sb.Append("WHERE E.Active = 1 AND ET.TeamID = @TeamID ");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
                    cmd.Parameters.Add("@TeamID", SqlDbType.Int).Value = TeamId;
                    Logger.Instance.Info("GetEmpIds: Opening Connection", updateUI);
                    conn.Open();
                    Logger.Instance.Info("GetEmpIds: Start Query", updateUI);
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var empId = Convert.ToInt32(reader[0]);
                        Logger.Instance.Info("GetEmpIds: FoundId - " + empId.ToString(), updateUI);
                        empIds.Add(empId);
                    }
                }
                catch (Exception e)
                {
                    Logger.Instance.Error("GetEmpIds: End Exception - " + e.ToString(), updateUI);
                    throw;
                }
                finally
                {
                    Logger.Instance.Info("GetEmpIds: End Query", updateUI);
                }
            }
            Logger.Instance.Info("GetEmpIds: Closed Connection", updateUI);
            Logger.Instance.Info("GetEmpIds: Return", updateUI);
            return empIds;
        }

        private Schedule GetSchedule(string empId, Action<string> updateUI)
        {
            Schedule sched = null;
            using (var conn = new SqlConnection(Settings.Instance.RTAConnection))
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("SELECT TOP 1 [Emp_id], [ScheduleDate], [ScheduleDetail], ");
                    sb.Append("[ADJ_SCHEDULE_DATE_START], [ADJ_SCHEDULE_DATE_END] ");
                    sb.Append("FROM [VW_SA_SCHEDULEDATA] ");
                    sb.Append("WHERE EMP_ID = @EmpId ");
                    sb.Append("ORDER BY Scheduledate DESC");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
                    cmd.Parameters.Add("@EmpId", SqlDbType.VarChar).Value = empId;
                    Logger.Instance.Info("GetSchedule: Opening Connection", updateUI);

                    conn.Open();

                    Logger.Instance.Info("GetSchedule: Start Query", updateUI);
                    Logger.Instance.Info("GetSchedule: For " + empId, updateUI);
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        sched = new Schedule();
                        sched.EmpId = Convert.ToString(reader[0]);
                        sched.ScheduleDate = Convert.ToDateTime(reader[1]);
                        sched.ScheduleDetail = Convert.ToString(reader[2]);
                        sched.AdjScheduleDateStart = Convert.ToDateTime(reader[3]);
                        sched.AdjScheduleDateEnd = Convert.ToDateTime(reader[4]);
                        Logger.Instance.Info("GetSchedule: Schedule(empid) - " + sched.EmpId, updateUI);
                        Logger.Instance.Info("GetSchedule: Schedule(scheduledate) - " + sched.ScheduleDate.ToShortDateString(), updateUI);
                        Logger.Instance.Info("GetSchedule: Schedule(scheduledetail) - " + sched.ScheduleDetail, updateUI);
                        Logger.Instance.Info("GetSchedule: Schedule(adjscheduledatestart) - " + sched.AdjScheduleDateStart.ToShortDateString(), updateUI);
                        Logger.Instance.Info("GetSchedule: Schedule(adjscheduledateend) - " + sched.AdjScheduleDateEnd.ToShortDateString(), updateUI);
                    }

                }
                catch (Exception e)
                {
                    Logger.Instance.Error("GetSchedule: End Exception - " + e.ToString(), updateUI);
                    throw;
                }
                finally
                {
                    Logger.Instance.Info("GetSchedule: End Query", updateUI);
                    if (sched == null)
                        Logger.Instance.Info("GetSchedule: Schedule(result) - Not found", updateUI);
                }
            }
            Logger.Instance.Info("GetSchedule: Closed Connection", updateUI);
            Logger.Instance.Info("GetSchedule: Return", updateUI);
            return sched;
        }

        private int? InsertSchedule(Schedule sched, Action<string> updateUI)
        {
            int? idResult = null;
            using (var conn = new SqlConnection(Settings.Instance.SAConnection))
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("INSERT INTO [streamassist_dbo].[tblEmp_Schedule] ");
                    sb.Append("VALUES (@EmpId, @ScheduleData, @ScheduleDetail, @AdjScheduleStart, @AdjScheduleEnd, @LoadedDate); ");
                    sb.Append("SELECT SCOPE_IDENTITY()");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
                    cmd.Parameters.Add("@EmpId", SqlDbType.VarChar).Value = sched.EmpId;
                    cmd.Parameters.Add("@ScheduleData", SqlDbType.DateTime).Value = sched.ScheduleDate;
                    cmd.Parameters.Add("@ScheduleDetail", SqlDbType.VarChar).Value = sched.ScheduleDetail;
                    cmd.Parameters.Add("@AdjScheduleStart", SqlDbType.SmallDateTime).Value = sched.AdjScheduleDateStart;
                    cmd.Parameters.Add("@AdjScheduleEnd", SqlDbType.SmallDateTime).Value = sched.AdjScheduleDateEnd;
                    cmd.Parameters.Add("@LoadedDate", SqlDbType.DateTime).Value = DateTime.Now;

                    Logger.Instance.Info("InsertSchedule: Opening Connection", updateUI);
                    conn.Open();

                    Logger.Instance.Info("InsertSchedule: Start Insert", updateUI);
                    var idInserted = cmd.ExecuteScalar();

                    if (idInserted != null)
                    {
                        idResult = Convert.ToInt32(idInserted);
                        Logger.Instance.Info(string.Format("InsertSchedule: Insert Successful (id:{0})", idResult), updateUI);
                    }

                }
                catch (Exception e)
                {
                    Logger.Instance.Error("InsertSchedule: End Exception - " + e.ToString(), updateUI);
                    throw;
                }
                finally
                {
                    Logger.Instance.Info("InsertSchedule: End Query", updateUI);
                }
            }
            Logger.Instance.Info("InsertSchedule: Closed Connection", updateUI);
            Logger.Instance.Info("InsertSchedule: Return", updateUI);
            return idResult;
        }

        private int? IsScheduleExisting(int empId, Action<string> updateUI)
        {
            int? idResult = null;
            using (var conn = new SqlConnection(Settings.Instance.SAConnection))
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("SELECT TOP 1 [Id] ");
                    sb.Append("FROM [streamassist_dbo].[tblEmp_Schedule] ");
                    sb.Append("WHERE EMP_ID = @EmpId");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
                    cmd.Parameters.Add("@EmpId", SqlDbType.VarChar).Value = empId;
                    Logger.Instance.Info("IsScheduleExisting: Opening Connection", updateUI);

                    conn.Open();

                    Logger.Instance.Info("IsScheduleExisting: Start Query", updateUI);
                    var res = cmd.ExecuteScalar();

                    if (res != null)
                    {
                        idResult = Convert.ToInt32(res);
                        Logger.Instance.Info(String.Format("IsScheduleExisting: Schedule(result) - Found (id:{0})", idResult), updateUI);
                    }
                    else
                    {
                        Logger.Instance.Info("IsScheduleExisting: Schedule(result) - Not found", updateUI);
                    }

                }
                catch (Exception e)
                {
                    Logger.Instance.Error("IsScheduleExisting: End Exception - " + e.ToString(), updateUI);
                    throw;
                }
                finally
                {
                    Logger.Instance.Info("IsScheduleExisting: End Query", updateUI);
                }
            }
            Logger.Instance.Info("IsScheduleExisting: Closed Connection", updateUI);
            Logger.Instance.Info("IsScheduleExisting: Return", updateUI);
            return idResult;
        }

        private void DeleteScheduleExisting(int schedId, Action<string> updateUI)
        {
            using (var conn = new SqlConnection(Settings.Instance.SAConnection))
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("DELETE FROM [streamassist_dbo].[tblEmp_ScheduleLine] ");
                    sb.Append("WHERE ScheduleId = @SchedId; ");
                    sb.Append("DELETE FROM [streamassist_dbo].[tblEmp_Schedule] ");
                    sb.Append("WHERE Id = @SchedId; ");


                    SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
                    cmd.Parameters.Add("@SchedId", SqlDbType.Int).Value = schedId;

                    Logger.Instance.Info("DeleteScheduleExisting: Opening Connection", updateUI);
                    conn.Open();

                    Logger.Instance.Info("DeleteScheduleExisting: Start Delete", updateUI);
                    int countDeleted = cmd.ExecuteNonQuery();

                    if (countDeleted > 0)
                        Logger.Instance.Info(String.Format("DeleteScheduleExisting: Delete Successful (id:{0})", schedId), updateUI);

                }
                catch (Exception e)
                {
                    Logger.Instance.Error("DeleteScheduleExisting: End Exception - " + e.ToString(), updateUI);
                    throw;
                }
                finally
                {
                    Logger.Instance.Info("DeleteScheduleExisting: End Query", updateUI);
                }
            }
            Logger.Instance.Info("DeleteScheduleExisting: Closed Connection", updateUI);
            Logger.Instance.Info("DeleteScheduleExisting: Return", updateUI);
        }

        private List<ScheduleLine> GetLinesFromSchedule(Schedule sched)
        {
            List<ScheduleLine> schedLines = new List<ScheduleLine>();

            string rawSched = sched.ScheduleDetail;
            string trimmedDetailBegin = rawSched.Insert(0, "#");
            string trimmedDetail = rawSched.Remove(rawSched.Length - 1); //remove the last #
            string[] detailParsed = trimmedDetail.Split('#');

            int resetter = 0;
            ScheduleLine line = new ScheduleLine();
            foreach (var det in detailParsed)
            {
                if (resetter == 1) //Activity is index 1
                    line.Activity = det;
                if (resetter == 3) //Start is index 3
                    line.ActivityStart = det; 
                if (resetter == 5) //End is index 5
                    line.ActivityEnd = det;

                resetter++;
                if (resetter == 8)
                {
                    line.ScheduleId = sched.Id;
                    schedLines.Add(line);
                    line = new ScheduleLine();
                    resetter = 0;
                }
                
            }

            return schedLines;
        }

        private int? InsertScheduleLine(ScheduleLine line, Action<string> updateUI)
        {
            int? idResult = null;
            using (var conn = new SqlConnection(Settings.Instance.SAConnection))
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("INSERT INTO [streamassist_dbo].[tblEmp_ScheduleLine] ");
                    sb.Append("VALUES (@ScheduleId, @Activity, @ActivityStart, @ActivityEnd, @LoadedDate); ");
                    sb.Append("SELECT SCOPE_IDENTITY()");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
                    cmd.Parameters.Add("@ScheduleId", SqlDbType.Int).Value = line.ScheduleId;
                    cmd.Parameters.Add("@Activity", SqlDbType.VarChar).Value = line.Activity;
                    cmd.Parameters.Add("@ActivityStart", SqlDbType.VarChar).Value = line.ActivityStart;
                    cmd.Parameters.Add("@ActivityEnd", SqlDbType.VarChar).Value = line.ActivityEnd;
                    cmd.Parameters.Add("@LoadedDate", SqlDbType.DateTime).Value = DateTime.Now;

                    Logger.Instance.Info("InsertScheduleLine: Opening Connection", updateUI);
                    conn.Open();

                    Logger.Instance.Info("InsertScheduleLine: Start Insert", updateUI);
                    var idInserted = cmd.ExecuteScalar();

                    if (idInserted != null)
                    {
                        idResult = Convert.ToInt32(idInserted);
                        Logger.Instance.Info(string.Format("InsertScheduleLine: Insert Successful (id:{0})", idResult), updateUI);
                    }

                }
                catch (Exception e)
                {
                    Logger.Instance.Error("InsertScheduleLine: End Exception - " + e.ToString(), updateUI);
                    throw;
                }
                finally
                {
                    Logger.Instance.Info("InsertScheduleLine: End Query", updateUI);
                }
            }
            Logger.Instance.Info("InsertScheduleLine: Closed Connection", updateUI);
            Logger.Instance.Info("InsertScheduleLine: Return", updateUI);
            return idResult;
        }
        

    }
}
