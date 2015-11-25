using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace EmployeeService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EmployeeService" in both code and config file together.
    public class EmployeeService : IEmployeeService
    {
        public Employee GetEmployee(int id)
        {
            Employee employee = new Employee();
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection())
            {
                SqlCommand cmd = new SqlCommand("spGetEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter parameterId = new SqlParameter();
                parameterId.ParameterName = "@id";
                parameterId.Value = id;
                cmd.Parameters.Add(parameterId);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    employee.Id = Convert.ToInt32(reader["Id"]);
                    employee.Name = reader["Name"].ToString();
                    employee.Gender = reader["Gender"].ToString();
                    employee.DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                }
            }

            return employee;
        }

        public void SaveEmployee(Employee employee)
        {
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection())
            {
                SqlCommand cmd = new SqlCommand("spSaveEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = employee.Id;
                cmd.Parameters.Add("@name", SqlDbType.NVarChar, 50).Value = employee.Name;
                cmd.Parameters.Add("@gender", SqlDbType.NVarChar, 50).Value = employee.Gender;
                cmd.Parameters.Add("@DateOfBirth", SqlDbType.DateTime).Value = employee.DateOfBirth;
                con.Open();
                var tran = con.BeginTransaction();
                try
                {
                    cmd.Transaction = tran;
                    cmd.ExecuteNonQuery();
                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
                finally
                {
                    tran.Dispose();
                    cmd.Dispose();
                    con.Close();
                    con.Dispose();
                }

            }

        }
    }
}
