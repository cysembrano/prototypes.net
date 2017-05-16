using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlowClient.Swanson.Forms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnRequest_Click(object sender, EventArgs e)
        {
            NavmanService.ServiceSoapClient client = new NavmanService.ServiceSoapClient();
            NavmanService.DoLoginRequest request = new NavmanService.DoLoginRequest();
            request.UserCredential = new NavmanService.UserCredentialInfo() { UserName = txtUserName.Text, Password = txtPassword.Text, ApplicationID = Guid.NewGuid() };
            request.Session = new NavmanService.SessionInfo() { SessionId = Guid.NewGuid() };
            var response = client.DoLogin(request);

            label4.Text = "Is Authenticated?  " + response.Authenticated.ToString();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
