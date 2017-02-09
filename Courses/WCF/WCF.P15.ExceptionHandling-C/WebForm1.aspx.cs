using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ServiceModel;

namespace WCF.P15.ExceptionHandling_C
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int numerator = Convert.ToInt32(TextBox1.Text);
            int denominator = Convert.ToInt32(TextBox2.Text);
            CalculatorService.CalculatorServiceClient client = new CalculatorService.CalculatorServiceClient();
            try
            {
                Label3.Text = client.Divide(numerator, denominator).ToString();
            }
            catch (FaultException fault)
            {
                Label3.Text = fault.Message;
            }
        }
    }
}