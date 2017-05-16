using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowClient.Swanson
{
    class Program
    {
        static void Main(string[] args)
        {
            NavmanService.ServiceSoapClient client = new NavmanService.ServiceSoapClient();
            NavmanService.DoLoginRequest request = new NavmanService.DoLoginRequest();
            request.UserCredential = new NavmanService.UserCredentialInfo() { UserName = "swansonapi02", Password = "apipassword" };
            request.Session = new NavmanService.SessionInfo() { SessionId = Guid.NewGuid() };
            var response = client.DoLogin(request);
            Console.Read();
        }
    }
}
