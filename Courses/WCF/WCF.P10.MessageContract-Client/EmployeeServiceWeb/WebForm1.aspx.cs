using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeeServiceWeb
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGet_Click(object sender, EventArgs e)
        {
            EmployeeService.IEmployeeService client = new EmployeeService.EmployeeServiceClient("BasicHttpBinding_IEmployeeService");
            EmployeeService.EmployeeRequest request = new EmployeeService.EmployeeRequest("AXMO2349EIO", Convert.ToInt32(txtID.Text));
            EmployeeService.EmployeeInfo emp = client.GetEmployee(request);
            
            if (emp.Type == EmployeeService.EmployeeType.FullTimeEmployee)
            {
                txtAnnualSalary.Text = emp.AnnualSalary.ToString();
                trAnnualSalary.Visible = true;
                trHourlyPay.Visible = false;
                trHoursWorked.Visible = false;
            }
            else
            {
                txtHourlyPay.Text = emp.HourlyPay.ToString();
                txtHoursWorked.Text = emp.HoursWorked.ToString();
                trAnnualSalary.Visible = false;
                trHourlyPay.Visible = true;
                trHoursWorked.Visible = true;
            }
            ddlEmployeeType.SelectedValue = ((int)emp.Type).ToString();

            txtID.Text = emp.Id.ToString();
            txtName.Text = emp.Name;
            txtGender.Text = emp.Gender;
            txtDateOfBirth.Text = emp.DOB.ToShortDateString();

            lblMessage.Text = "Employee Retrieved";

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            EmployeeService.IEmployeeService client = new EmployeeService.EmployeeServiceClient("BasicHttpBinding_IEmployeeService");
            EmployeeService.EmployeeInfo emp = null;

            if (((EmployeeService.EmployeeType)Convert.ToInt32(ddlEmployeeType.SelectedValue)) == EmployeeService.EmployeeType.FullTimeEmployee)
            {
                emp = new EmployeeService.EmployeeInfo
                {
                    Id = Convert.ToInt32(txtID.Text),
                    Name = txtName.Text,
                    Gender = txtGender.Text,
                    DOB = Convert.ToDateTime(txtDateOfBirth.Text),
                    AnnualSalary = Convert.ToInt32(txtAnnualSalary.Text),
                };
                client.SaveEmployee(emp);
                lblMessage.Text = "Employee Saved";
            }
            else if (((EmployeeService.EmployeeType)Convert.ToInt32(ddlEmployeeType.SelectedValue)) == EmployeeService.EmployeeType.PartTimeEmployee)
            {
                emp = new EmployeeService.EmployeeInfo
                {
                    Id = Convert.ToInt32(txtID.Text),
                    Name = txtName.Text,
                    Gender = txtGender.Text,
                    DOB = Convert.ToDateTime(txtDateOfBirth.Text),
                    HourlyPay = Convert.ToInt32(txtHourlyPay.Text),
                    HoursWorked = Convert.ToInt32(txtHoursWorked.Text),
                };
                client.SaveEmployee(emp);
                lblMessage.Text = "Employee Saved";
            }


        }

        protected void ddlEmployeeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEmployeeType.SelectedValue == "-1")
            {
                trAnnualSalary.Visible = false;
                trHourlyPay.Visible = false;
                trHoursWorked.Visible = false;
            }
            else if (ddlEmployeeType.SelectedValue == "1")
            {
                trAnnualSalary.Visible = true;
                trHourlyPay.Visible = false;
                trHoursWorked.Visible = false; ;
            }
            else
            {
                trAnnualSalary.Visible = false;
                trHourlyPay.Visible = true;
                trHoursWorked.Visible = true;
            }
        }
    }
}