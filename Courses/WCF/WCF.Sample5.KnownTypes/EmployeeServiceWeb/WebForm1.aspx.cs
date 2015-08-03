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

            ddlEmployeeType.SelectedValue = ((int)emp.Type).ToString();
            txtID.Text = emp.ID.ToString();
            txtName.Text = emp.Name;
            txtGender.Text = emp.Gender;
            txtDateOfBirth.Text = emp.DateOfBirth.ToShortDateString();

            if (emp.Type == EmployeeService.EmployeeType.FullTimeEmployee)
            {
                txtAnnualSalary.Text = ((EmployeeService.FullTimeEmployee)emp).AnnualSalary.ToString();
                trAnnual.Visible = true;
                trHourlyPay.Visible = false;
                trHourWorked.Visible = false;
            }
            else
            {
                txtHourlyPay.Text = ((EmployeeService.PartTimeEmployee)emp).HourlyPay.ToString();
                txtHoursWorked.Text = ((EmployeeService.PartTimeEmployee)emp).HoursWorked.ToString();
                trAnnual.Visible = false;
                trHourlyPay.Visible = true;
                trHourWorked.Visible = true;

            }

            lblMessage.Text = "Employee Retrieved";

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var client = new EmployeeService.EmployeeServiceClient("BasicHttpBinding_IEmployeeService");

            if (((EmployeeService.EmployeeType)Convert.ToInt32(ddlEmployeeType.SelectedValue)) == EmployeeService.EmployeeType.FullTimeEmployee)
            {
                var emp = new EmployeeService.FullTimeEmployee
                {
                    ID = Convert.ToInt32(txtID.Text),
                    Name = txtName.Text,
                    Gender = txtGender.Text,
                    DateOfBirth = Convert.ToDateTime(txtDateOfBirth.Text),
                    Type = EmployeeService.EmployeeType.FullTimeEmployee,
                    AnnualSalary = Convert.ToInt32(txtAnnualSalary.Text)
                };
                client.SaveEmployee(emp);
            }
            else if (((EmployeeService.EmployeeType)Convert.ToInt32(ddlEmployeeType.SelectedValue)) == EmployeeService.EmployeeType.PartTimeEmployee)
            {
                var emp = new EmployeeService.PartTimeEmployee
                {
                    ID = Convert.ToInt32(txtID.Text),
                    Name = txtName.Text,
                    Gender = txtGender.Text,
                    DateOfBirth = Convert.ToDateTime(txtDateOfBirth.Text),
                    Type = EmployeeService.EmployeeType.PartTimeEmployee,
                    HourlyPay = Convert.ToInt32(txtHourlyPay.Text),
                    HoursWorked = Convert.ToInt32(txtHoursWorked.Text)
                };
                client.SaveEmployee(emp);
            }
            else
            {
                lblMessage.Text = "Please Select Employee Type";
            }
            lblMessage.Text = "Employee Saved";
        }

        protected void ddlEmployeeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEmployeeType.SelectedValue == "-1")
            {
                trAnnual.Visible = false;
                trHourlyPay.Visible = false;
                trHourWorked.Visible = false;
            }
            else if (ddlEmployeeType.SelectedValue == "1")
            {
                trAnnual.Visible = true;
                trHourlyPay.Visible = false;
                trHourWorked.Visible = false;
            }
            else
            {
                trAnnual.Visible = false;
                trHourlyPay.Visible = true;
                trHourWorked.Visible = true;
            }
        }
    }
}