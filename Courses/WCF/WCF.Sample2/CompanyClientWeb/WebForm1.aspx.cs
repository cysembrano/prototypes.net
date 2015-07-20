using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CompanyClientWeb
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnPublic_Click(object sender, EventArgs e)
        {
            CompanyService.CompanyPublicServiceClient client = new
                CompanyService.CompanyPublicServiceClient("BasicHttpBinding_ICompanyPublicService");

            lblPublic.Text = client.GetPublicInformation();
        }

        protected void btnConfidential_Click(object sender, EventArgs e)
        {
            CompanyService.CompanyConfidentialServiceClient client = new
                CompanyService.CompanyConfidentialServiceClient("NetTcpBinding_ICompanyConfidentialService");

            lblConfidential.Text = client.GetConfidentialInformation();
        }
    }
}