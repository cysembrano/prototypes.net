using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FlowClient.Swanson.Forms.NavmanService;

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

        private void button1_Click(object sender, EventArgs e)
        {
            SessionInfo currentSession = null;



            NavmanService.ServiceSoapClient client = new NavmanService.ServiceSoapClient();
            NavmanService.DoLoginRequest request = new NavmanService.DoLoginRequest();
            request.UserCredential = new NavmanService.UserCredentialInfo() { UserName = txtUserName.Text, Password = txtPassword.Text, ApplicationID = Guid.NewGuid() };
            request.Session = new NavmanService.SessionInfo() { SessionId = Guid.NewGuid() };
            var loginResponse = client.DoLogin(request);

            if (loginResponse == null || !loginResponse.OperationStatus || !loginResponse.Authenticated)
            {
                //Handle the Login Failure Appropriately.
                throw (new ApplicationException("Login Failed."));
            }
            currentSession = loginResponse.SecurityProfile.Session;
            DoSendFormsRequest doSendFormsRequest = new DoSendFormsRequest();
            //Supply Owner ID to which forms need to be sent
            doSendFormsRequest.OwnerId = new Guid("9F79EA9B-8B43-47A6-92C1-2461167BB8D5");

            //Set the credentials using a valid OnlineAVl2 token
            doSendFormsRequest.Session = currentSession;
            //Supply list of forms. For each form set Tracking ID,Recipient IDs,Form
            //Definition Number, Form fields etc.
            List<SendFormInstanceMessage> doSendFormsInstnaceMessages = new
            List<SendFormInstanceMessage>();
            //Create first form
            SendFormInstanceMessage tempSendFormInstanceMessage = new
            SendFormInstanceMessage();
            //Set Tracking ID
            tempSendFormInstanceMessage.TrackingID = Guid.NewGuid();
            //Set Form Definition Number
            tempSendFormInstanceMessage.DefinitionNumber = 240;
            //Set Form Number
            tempSendFormInstanceMessage.FormNumber = 50;
            //Set Recipients
            Guid[] recipientIDs = new Guid[2];
            recipientIDs[0] = new Guid("1AD64143-75A2-4665-B0B1-C4B4307CBFEC");
            recipientIDs[1] = new Guid("54701928-F7FC-45CA-BED7-9853276EBB6B");
            tempSendFormInstanceMessage.RecipientIds = recipientIDs;
            //instantiate the BaseFieldValues
            tempSendFormInstanceMessage.BaseFieldValues = new BaseFieldValues();
            //Set Form Fields
            tempSendFormInstanceMessage.BaseFieldValues.Values = new BaseFieldValue[7];
            //Decimal Field
            DecimalFieldValue objDecimalFieldValue = new DecimalFieldValue();
            objDecimalFieldValue.Value = 12.22m;
            tempSendFormInstanceMessage.BaseFieldValues.Values.SetValue(objDecimalFieldValue, 0);
            //String Field
            StringFieldValue objStringFieldValue = new StringFieldValue();
            objStringFieldValue.Value = "Dispatcher1";
            tempSendFormInstanceMessage.BaseFieldValues.Values.SetValue(objStringFieldValue, 1);
            //Select Field
            SelectFieldValue objSelectFieldValue = new SelectFieldValue();
            objSelectFieldValue.Value = "1";
            tempSendFormInstanceMessage.BaseFieldValues.Values.SetValue(
            objSelectFieldValue, 2);
            //Address Field
            AddressFieldValue objAddressFieldValue = new AddressFieldValue();
            objAddressFieldValue.Value = "London";
            objAddressFieldValue.Lat = 75.33f;
            objAddressFieldValue.Lon = 80.22f;
            tempSendFormInstanceMessage.BaseFieldValues.Values.SetValue(
            objAddressFieldValue, 3);

            DateFieldValue objDateFieldValue = new DateFieldValue();
            objDateFieldValue.Value = DateTime.Now.Date;
            tempSendFormInstanceMessage.BaseFieldValues.Values.SetValue(
            objDateFieldValue, 4);
            //Time Field
            TimeFieldValue objTimeFieldValue = new TimeFieldValue();
            objTimeFieldValue.Value = DateTime.Now.ToLocalTime();
            tempSendFormInstanceMessage.BaseFieldValues.Values.SetValue(
            objTimeFieldValue, 5);
            //Date Time Field
            DateTimeFieldValue objDateTimeFieldValue = new DateTimeFieldValue();
            objDateTimeFieldValue.Value = DateTime.Now;
            tempSendFormInstanceMessage.BaseFieldValues.Values.SetValue(
            objDateTimeFieldValue, 6);
            doSendFormsInstnaceMessages.Add(tempSendFormInstanceMessage);
            //Create Second Form
            tempSendFormInstanceMessage = new SendFormInstanceMessage();
            //Set Tracking ID
            tempSendFormInstanceMessage.TrackingID = Guid.NewGuid();
            //Set Form Definition Number
            tempSendFormInstanceMessage.DefinitionNumber = 241;
            //Set Form Number
            tempSendFormInstanceMessage.FormNumber = 51;
            //Set Recipients
            recipientIDs = new Guid[1];
            recipientIDs[0] = new Guid("54701928-F7FC-45CA-BED7-9853276EBB6B");
            tempSendFormInstanceMessage.RecipientIds = recipientIDs;
            //instantiate the BaseFieldValues
            tempSendFormInstanceMessage.BaseFieldValues = new BaseFieldValues();
            //Set Form Fields
            tempSendFormInstanceMessage.BaseFieldValues.Values = new BaseFieldValue[1];
            objStringFieldValue = new StringFieldValue();
            objStringFieldValue.Value = "Dispatcher2";
            tempSendFormInstanceMessage.BaseFieldValues.Values.SetValue(
            objStringFieldValue, 0);
            doSendFormsInstnaceMessages.Add(tempSendFormInstanceMessage);
            //Set list of forms
            doSendFormsRequest.SendFormInstanceMessages =
            doSendFormsInstnaceMessages.ToArray();
            //Set culture from which this API call is made
            doSendFormsRequest.Culture =
            System.Threading.Thread.CurrentThread.CurrentCulture.Name;
            DoSendFormsResponse DoSendFormsResponse = null;


            DoSendFormsResponse = client.DoSendForms(doSendFormsRequest);

        }
    }
}
