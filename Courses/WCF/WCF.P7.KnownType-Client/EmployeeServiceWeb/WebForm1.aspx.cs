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
            var client = new EmployeeService.EmployeeServiceClient("BasicHttpBinding_IEmployeeService");
            var emp = client.GetEmployee(Convert.ToInt32(txtID.Text));

            if (emp.Type == EmployeeService.EmployeeType.FullTimeEmployee)
            {
                txtAnnualSalary.Text = ((EmployeeService.FullTimeEmployee)emp).AnnualSalary.ToString();
                trAnnualSalary.Visible = true;
                trHourlyPay.Visible = false;
                trHoursWorked.Visible = false;
            }
            else
            {
                txtHourlyPay.Text = ((EmployeeService.PartTimeEmployee)emp).HourlyPay.ToString();
                txtHoursWorked.Text = ((EmployeeService.PartTimeEmployee)emp).HoursWorked.ToString();
                trAnnualSalary.Visible = false;
                trHourlyPay.Visible = true;
                trHoursWorked.Visible = true;
            }
            ddlEmployeeType.SelectedValue = ((int)emp.Type).ToString();

            txtID.Text = emp.ID.ToString();
            txtName.Text = emp.Name;
            txtGender.Text = emp.Gender;
            txtDateOfBirth.Text = emp.DateOfBirth.ToShortDateString();

            lblMessage.Text = "Employee Retrieved";

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var client = new EmployeeService.EmployeeServiceClient("BasicHttpBinding_IEmployeeService");
            EmployeeService.Employee emp = null;

            if (((EmployeeService.EmployeeType)Convert.ToInt32(ddlEmployeeType.SelectedValue)) == EmployeeService.EmployeeType.FullTimeEmployee)
            {
                emp = new EmployeeService.FullTimeEmployee
                {
                    ID = Convert.ToInt32(txtID.Text),
                    Name = txtName.Text,
                    Gender = txtGender.Text,
                    DateOfBirth = Convert.ToDateTime(txtDateOfBirth.Text),
                    AnnualSalary = Convert.ToInt32(txtAnnualSalary.Text),
                };
                client.SaveEmployee(emp);
                lblMessage.Text = "Employee Saved";
            }
            else if (((EmployeeService.EmployeeType)Convert.ToInt32(ddlEmployeeType.SelectedValue)) == EmployeeService.EmployeeType.PartTimeEmployee)
            {
                emp = new EmployeeService.PartTimeEmployee
                {
                    ID = Convert.ToInt32(txtID.Text),
                    Name = txtName.Text,
                    Gender = txtGender.Text,
                    DateOfBirth = Convert.ToDateTime(txtDateOfBirth.Text),
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