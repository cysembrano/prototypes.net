using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;

namespace EmployeeService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EmployeeService" in both code and config file together.
    public class EmployeeService : IEmployeeService
    {

        public Employee GetEmployee(int Id)
        {
            Employee employee = null;
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spGetEmployee2", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter parameterId = new SqlParameter("@Id", Id);
                cmd.Parameters.Add(parameterId);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if ((EmployeeType)reader["EmployeeType"] == EmployeeType.FullTimeEmployee)
                    {
                        employee = new FullTimeEmployee()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            Gender = reader["Gender"].ToString(),
                            DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                            Type = EmployeeType.FullTimeEmployee,
                            AnnualSalary = Convert.ToInt32(reader["AnnualSalary"])
                        };
                    }
                    else
                    {
                        employee = new PartTimeEmployee()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            Gender = reader["Gender"].ToString(),
                            DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                            Type = EmployeeType.PartTimeEmployee,
                            HourlyPay = Convert.ToInt32(reader["HourlyPay"]),
                            HoursWorked = Convert.ToInt32(reader["HoursWorked"])
                        };
                    }
                }
                return employee;
            }
        }

        public void SaveEmployee(Employee employee)
        {
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                var cmd = new SqlCommand("spSaveEmployee2", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Id",employee.Id));
                cmd.Parameters.Add(new SqlParameter("@Name", employee.Name));
                cmd.Parameters.Add(new SqlParameter("@Gender", employee.Gender));
                cmd.Parameters.Add(new SqlParameter("@DateOfBirth", employee.DateOfBirth));
                cmd.Parameters.Add(new SqlParameter("@EmployeeType", employee.Type));
                if (employee.GetType() == typeof(FullTimeEmployee))
                {
                    cmd.Parameters.Add(new SqlParameter("@AnnualSalary", ((FullTimeEmployee)employee).AnnualSalary));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@HourlyPay", ((PartTimeEmployee)employee).HourlyPay));
                    cmd.Parameters.Add(new SqlParameter("@HoursWorked", ((PartTimeEmployee)employee).HoursWorked));
                }
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
