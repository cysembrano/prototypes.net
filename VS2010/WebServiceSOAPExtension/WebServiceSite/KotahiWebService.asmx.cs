using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using WebServiceSite.SoapExtensions;

namespace WebServiceSite
{
    [WebService(Namespace = "/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class KotahiWebService : System.Web.Services.WebService
    {


        public KotahiWebService()
        {

            //Uncomment the following line if using designed components
            //InitializeComponent();

        }

        [WebMethod(EnableSession = true)]
        [TraceExtension]
        [KotahiExtension]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("L008_WebServices_ProcessASNMessage_Binder_publishASNMessagecanonical", RequestNamespace = "http://www.silverfernfarms.co.nz/", ResponseNamespace = "http://www.silverfernfarms.co.nz/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public Boolean L008_WebServices_ProcessASNMessage_Binder_publishASNMessagecanonical(publishASNMessagecanonical publishASNMessagecanonical, out publishASNMessagecanonicalResponse publishASNMessagecanonicalResponse)
        {
            var asn = publishASNMessagecanonical.ASNMessage.ASNMessage1.ASN;


            publishASNMessagecanonicalResponse response = new publishASNMessagecanonicalResponse();
            response.Response = new WSResponse();
            response.Response.Msg = "You got response";
            response.Response.Type = "ResponseType";

            publishASNMessagecanonicalResponse = response;

            return true;
        }

    }
}
