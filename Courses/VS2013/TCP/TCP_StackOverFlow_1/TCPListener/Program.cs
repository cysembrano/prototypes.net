using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPListener
{
    class Program
    {
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }

        public static void Main()
        {
            try
            {
                IPAddress ipAddress = IPAddress.Parse(GetLocalIPAddress());
                

                Console.WriteLine("Starting TCP listener...");

                TcpListener listener = new TcpListener(ipAddress, 35699);
                

                listener.Start();

                while (true)
                {
                    Console.WriteLine("Server is listening on " + listener.LocalEndpoint);

                    Console.WriteLine("Waiting for a connection...");

                    Socket client = listener.AcceptSocket();

                    Console.WriteLine("Connection accepted.");

                    Console.WriteLine("Reading data...");

                    byte[] data = new byte[200000];
                    int size = client.Receive(data);
                    Console.WriteLine("Recieved data: ");
                    for (int i = 0; i < size; i++)
                        Console.Write(Convert.ToChar(data[i]));

                    Console.WriteLine();

                    client.Close();
                }

                listener.Stop();
   
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.StackTrace);
                Console.ReadLine();
            }
        }
    }
}
