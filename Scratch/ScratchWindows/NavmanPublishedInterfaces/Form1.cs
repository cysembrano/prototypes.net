using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NavmanPublishedInterfaces.NavmanPublishInterface;

namespace NavmanPublishedInterfaces
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void doLogonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoLogon();
        }

        private void DoLogon()
        {
            using(var client = new NavmanPublishInterface.ServiceSoapClient())
            {
                var loginRequest = new DoLoginRequest();
                loginRequest.Session = new SessionInfo();
                loginRequest.Session.SessionId = Guid.NewGuid();
                loginRequest.UserCredential = new UserCredentialInfo();
                loginRequest.UserCredential.UserName = "A";
                loginRequest.UserCredential.Password = "B";

                var response = client.DoLogin(loginRequest);
                this.textBox1.Text = string.Format("Am I authenticated? {0}", response.Authenticated ? "yes" : "no");
            }
        }
    }
}
