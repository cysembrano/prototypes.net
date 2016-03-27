using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convergys.Assist.Sched
{
    public static class TeamLookup
    {
        public static IDictionary<int, string> GetTeams()
        {
            using (var conn = new SqlConnection(Settings.Instance.SAConnection))
            {
                Dictionary<int, string> dic = new Dictionary<int, string>();
                SqlCommand cmd = new SqlCommand("SELECT TeamId, Team FROM streamassist_dbo.tblTeam WHERE Active = 1 ORDER BY team", conn);
                try
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        dic.Add(Convert.ToInt32(reader[0]), Convert.ToString(reader[1]));
                    }
                }
                catch (Exception e)
                {
                    Logger.Instance.Error("GetTeams: " + e.ToString());
                    throw;
                }
                finally
                {
                    conn.Close();                    
                }
                return dic;
            }
        }

    }
}
