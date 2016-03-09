using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security.OAuth.Messages;
using Newtonsoft.Json.Linq;
using System.Web;


namespace Shopify.ConsoleApp
{
    class Program
    {
        
        static void Main(string[] args)
        {
            GetRequestOne();
            //GetRequestTwo();
            //GetRequestThree();
        }


        static void GetRequestOne()
        {
                        // Create a request for the URL.         
            //WebRequest request = WebRequest.Create("https://dev-hobbs-global.myshopify.com/admin/oauth/access_token");

            var _apiKey = "87f3086a74c3bb60ea7b9dbf5eff5a8a";
            var _password = "a268ac5e175fdef73f8b6a7462c0b003";

            WebRequest request = WebRequest.Create("https://dev-hobbs-global.myshopify.com/admin/orders.xml");
            // Set the credentials.

            request.Credentials = new NetworkCredential(_apiKey, _password);
            //request.Credentials = new NetworkCredential("cythecy3@gmail.com", "Toink1235");
            // Get the response.

            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            HttpWebResponse response = null;
            try
            {
                // This is where the HTTP GET actually occurs.
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception a)
            {
                Console.WriteLine(a.ToString());
            }
            // Display the status. You want to see "OK" here.
            Console.WriteLine(response.StatusDescription);
            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content. This is the XML that represents all the products for the shop.
            string responseFromServer = reader.ReadToEnd();
            // Display the content. 
            Console.WriteLine(responseFromServer);
            // Cleanup the streams and the response.
            reader.Close();
            dataStream.Close();
            response.Close();
        }

        static void GetRequestTwo()
        {
            var _shopName = "dev-hobbs-global";
            var _apiKey = "87f3086a74c3bb60ea7b9dbf5eff5a8a";
            var _secret = "0d60f2b56a637dede5344573e7723212";
            var code = "c7b1e233dbf3321ac06afd48041009ce-1455163765";

            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            string url = String.Format("https://{0}.myshopify.com/admin/oauth/access_token", _shopName);
            string postBody = String.Format("client_id={0}&client_secret={1}&code={2}",
                _apiKey,    // {0}
                _secret,    // {1}
                code);      // {2}

            HttpWebRequest authRequest = (HttpWebRequest)WebRequest.Create(url);
            authRequest.Method = "POST";
            authRequest.ContentType = "application/x-www-form-urlencoded";
            using (var ms = new MemoryStream())
            {
                using (var writer = new StreamWriter(authRequest.GetRequestStream()))
                {
                    writer.Write(postBody);
                    writer.Close();
                }
            }

            var response = (HttpWebResponse)authRequest.GetResponse();
            string result = null;

            using (Stream stream = response.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                result = sr.ReadToEnd();
                sr.Close();
            }

            if (!String.IsNullOrEmpty(result))
            {
                // it's JSON so decode it
                JObject jsonResult = JObject.Parse(result);
                //return new ShopifyAuthorizationState
                //{
                //    ShopName = this._shopName,
                //    AccessToken = (string)jsonResult["access_token"]
                //};
            }

        }

        static void GetRequestThree()
        {
            var authURL = new StringBuilder();
            var _shopName = "dev-hobbs-global";
            var _apiKey = "1373bfa19fb568c6c2885d65947ef546";
            var redirectUrl = @"http://www.rentalcozy.com";
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            string[] scope = new string[] { "read_products", "read_customers" };

            authURL.AppendFormat("http://{0}.myshopify.com/admin/oauth/authorize", _shopName);
            authURL.AppendFormat("?client_id={0}", _apiKey);
            
            if (scope != null && scope.Length > 0)
            {
                string commaSeperatedScope = String.Join(",", scope);
                if (!String.IsNullOrEmpty(commaSeperatedScope))
                    authURL.AppendFormat("&scope={0}", HttpUtility.UrlEncode(commaSeperatedScope));
            }

            if (redirectUrl != null && redirectUrl.Length > 0)
            {
                authURL.AppendFormat("&redirect_uri={0}", HttpUtility.UrlEncode(redirectUrl));
            }

        }
    }
}
