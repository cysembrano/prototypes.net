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

            txtID.Text = emp.ID.ToString();
            txtName.Text = emp.Name;
            txtGender.Text = emp.Gender;
            txtDateOfBirth.Text = emp.DateOfBirth.ToShortDateString();

            lblMessage.Text = "Employee Retrieved";

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var client = new EmployeeService.EmployeeServiceClient("BasicHttpBinding_IEmployeeService");
            var emp = new EmployeeService.Employee();

            emp.ID = Convert.ToInt32(txtID.Text);
            emp.Name = txtName.Text;
            emp.Gender = txtGender.Text;
            emp.DateOfBirth = Convert.ToDateTime(txtDateOfBirth.Text);

            client.SaveEmployee(emp);

            lblMessage.Text = "Employee Saved";
        }
    }
}