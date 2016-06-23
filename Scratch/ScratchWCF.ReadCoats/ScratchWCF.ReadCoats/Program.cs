using ScratchWCF.ReadCoats.MessageInspector;
using ScratchWCF.ReadCoats.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ScratchWCF.ReadCoats
{
    public class Program
    {
        static void Main(string[] args)
        {
            //DoWCF.CallRegular_WithInspect();

            DoWCF.CallRegular();

            //var tokenStream = DoWCF.CallManual_GetToken();
            //DoWCF.CallManual_GetAgents(tokenStream);

        }
    }

    public static class DoWCF
    {
        public static void CallRegular_Alt1()
        {
            ServiceReference1.ServiceClient client = new ServiceReference1.ServiceClient();

        }

        public static void CallRegular()
        {
            ServiceReference1.ServiceClient client = new ServiceReference1.ServiceClient();
            client.ClientCredentials.UserName.UserName = "development@flowsoftware.co.nz";
            client.ClientCredentials.UserName.Password = "GT56Di7fs30sw2GjkH";


            // Treat the test certificate as trusted
            //client.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.PeerOrChainTrust;

            // Call the service operation using the proxy
            var agents = client.GetAgents();
            foreach (var agent in agents)
            {
                Console.WriteLine(String.Format("{0} {1} {2}", agent.AgentCode, agent.AgentFullName, agent.AgentUserName));
            }

            client.Close();
        }


        public static void CallRegular_WithInspect()
        {
            ServiceReference1.ServiceClient client = new ServiceReference1.ServiceClient();
            client.ClientCredentials.UserName.UserName = "development@flowsoftware.co.nz";
            client.ClientCredentials.UserName.Password = "GT56Di7fs30sw2GjkH";
            client.Endpoint.EndpointBehaviors.Add(new CustomBehaviour());


            // Treat the test certificate as trusted
            //client.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.PeerOrChainTrust;

            // Call the service operation using the proxy
            var agents = client.GetAgents();
            foreach (var agent in agents)
            {
                Console.WriteLine(String.Format("{0} {1} {2}", agent.AgentCode, agent.AgentFullName, agent.AgentUserName));
            }

            client.Close();
        }


        public static Stream CallManual_GetToken()
        {
            DateTime now = DateTime.Now;
            String SoapRequest = String.Format(Resources.Request_GetToken,
                                               now.ToUniversalTime().ToString("o"),
                                               now.AddMinutes(60).ToUniversalTime().ToString("o"),
                                               "development@flowsoftware.co.nz",
                                               "GT56Di7fs30sw2GjkH",
                                               EncodeTo64("flowbinarysecret"),
                                               Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://serviceapi.realbaselive.com/Service.svc/soapService");
            ASCIIEncoding encoding = new ASCIIEncoding();

            byte[] bytesToWrite = encoding.GetBytes(SoapRequest);
            request.Method = "POST";
            request.ContentLength = bytesToWrite.Length;
            request.ContentType = "application/soap+xml; charset=UTF-8";

            Stream newStream = request.GetRequestStream();
            newStream.Write(bytesToWrite, 0, bytesToWrite.Length);
            newStream.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return response.GetResponseStream();

        }

        public static Stream CallManual_GetAgents(Stream TokenRequest)
        {
            StreamReader reader = new StreamReader(TokenRequest);

            XmlDocument x = new XmlDocument();
            x.Load(reader);

            XmlNodeList requestedSecurityToken = x.GetElementsByTagName("t:RequestedSecurityToken");
            String strsecurityContextToken = "";
            if (requestedSecurityToken != null)
            {
                foreach (XmlNode node in requestedSecurityToken)
                {
                    strsecurityContextToken += node.InnerXml;
                }
            }

            XmlNodeList securityContextToken = x.GetElementsByTagName("c:SecurityContextToken");
            string actualToken = x.GetElementsByTagName("c:Identifier")[0].InnerText;
            String strsecurityContextTokenId = "";
            Console.WriteLine("SecurityTokenContext:  " + strsecurityContextToken);
            for (int i = 0; i < securityContextToken.Count; i++)
            {
                var attributeCollection = securityContextToken[i].Attributes["u:Id"];
                if (attributeCollection != null)
                {
                    strsecurityContextTokenId = attributeCollection.Value;
                    Console.WriteLine("SecurityTokenContextId:  " + strsecurityContextTokenId);
                }
            }

            DateTime now = DateTime.Now;
            string created = now.ToUniversalTime().ToString("o");
            string expires = now.AddMinutes(60).ToUniversalTime().ToString("o");



            SHA1CryptoServiceProvider sha1Hasher = new SHA1CryptoServiceProvider();
            byte[] hashedDataBytes = sha1Hasher.ComputeHash(Encoding.UTF8.GetBytes(CanonicalizeDsig(GetDigestValue_Timestamp(created, expires))));
            string digestValue = Convert.ToBase64String(hashedDataBytes);

            string binarySecret = x.GetElementsByTagName("t:BinarySecret")[0].InnerText;
            

            byte[] signedInfoBytes = Encoding.UTF8.GetBytes(GetSignatureValue_SignedInfo2(digestValue));

            HMACSHA1 hmac = new HMACSHA1();            
            byte[] binarySecretBytes = Convert.FromBase64String(binarySecret);
            
            hmac.Key = binarySecretBytes;
            byte[] hmacHash = hmac.ComputeHash(signedInfoBytes);
            string signatureValue = Convert.ToBase64String(hmacHash);


            String SoapRequest = String.Format(Resources.Request_GetAgents,
                                               created,
                                               expires,
                                               digestValue,
                                               signatureValue,
                                               strsecurityContextTokenId,
                                               strsecurityContextTokenId, 
                                               Guid.NewGuid(),
                                               actualToken);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://serviceapi.realbaselive.com/Service.svc/soapService");
            ASCIIEncoding encoding = new ASCIIEncoding();

            byte[] bytesToWrite = encoding.GetBytes(SoapRequest);
            request.Method = "POST";
            request.ContentLength = bytesToWrite.Length;
            request.ContentType = "application/soap+xml; charset=UTF-8";
            
            Stream newStream = request.GetRequestStream();
            newStream.Write(bytesToWrite, 0, bytesToWrite.Length);
            newStream.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return response.GetResponseStream();

        }

        static string EncodeTo64(string toEncode)
        {
            byte[] toEncodeAsBytes
                  = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
            string returnValue
                  = System.Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }

        static string GetDigestValue_Timestamp(string created, string expires)
        {
            return "<u:Timestamp u:Id=\"_0\" xmlns:u=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd\"><u:Created>" + created + "</u:Created><u:Expires>" + expires + "</u:Expires></u:Timestamp>";
        }

        static string GetSignatureValue_SignedInfo(string digestValue)
        {

            StringBuilder strBuild = new StringBuilder();
            strBuild.Append("<SignedInfo>");
            strBuild.Append("<CanonicalizationMethod Algorithm=\"http://www.w3.org/2001/10/xml-exc-c14n#\"/>");
            strBuild.Append("<SignatureMethod Algorithm=\"http://www.w3.org/2000/09/xmldsig#hmac-sha1\"/><Reference URI=\"#_0\">");
            strBuild.Append("<Transforms><Transform Algorithm=\"http://www.w3.org/2001/10/xml-exc-c14n#\"></Transform></Transforms>");
            strBuild.Append("<DigestMethod Algorithm=\"http://www.w3.org/2000/09/xmldsig#sha1\"/><DigestValue>" + digestValue);
            strBuild.Append("</DigestValue></Reference></SignedInfo>");

            return strBuild.ToString();
        }

        static string GetSignatureValue_SignedInfo2(string digestValue)
        {
            return "<SignedInfo xmlns=\"http://www.w3.org/2000/09/xmldsig#\"><CanonicalizationMethod Algorithm=\"http://www.w3.org/2001/10/xml-exc-c14n#\"></CanonicalizationMethod><SignatureMethod Algorithm=\"http://www.w3.org/2000/09/xmldsig#hmac-sha1\"></SignatureMethod><Reference URI=\"#_0\"><Transforms><Transform Algorithm=\"http://www.w3.org/2001/10/xml-exc-c14n#\"></Transform></Transforms><DigestMethod Algorithm=\"http://www.w3.org/2000/09/xmldsig#sha1\"></DigestMethod><DigestValue>" + digestValue + "</DigestValue></Reference></SignedInfo>";
        }

        static void CallManual_GetAgentsWithSigning(Stream TokenRequest)
        {
            StreamReader reader = new StreamReader(TokenRequest);
            XmlDocument x = new XmlDocument();
            x.Load(reader);

            SignedXml signedXml = new SignedXml(x);

            signedXml.SigningKey = new RSACryptoServiceProvider();
        }

        private static string CanonicalizeDsig(string input)
        {
            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = false;
            try
            {
                doc.LoadXml(input);
                XmlDsigC14NTransform trans = new XmlDsigC14NTransform();
                trans.LoadInput(doc);
                String c14NInput = new StreamReader((Stream)trans.GetOutput(typeof(Stream))).ReadToEnd();

                return c14NInput;


            }
            catch (Exception ex)
            {
                return String.Empty;
            }

        }
        
    }


}
