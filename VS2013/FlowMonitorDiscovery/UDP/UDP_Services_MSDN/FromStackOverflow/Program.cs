using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FromStackOverflow
{
    class CAA
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


        private Socket UDPSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        private IPAddress Target_IP;
        private int Target_Port;
        public static int bPause;

        public CAA()
        {
            Target_IP = IPAddress.Parse("192.168.5.8");
            Target_Port = 35699;

            try
            {
                IPEndPoint LocalHostIPEnd = new
                IPEndPoint(IPAddress.Any, Target_Port);
                UDPSocket.SetSocketOption(SocketOptionLevel.Udp, SocketOptionName.NoDelay, 1);
                UDPSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
                UDPSocket.Bind(LocalHostIPEnd);
                UDPSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, 0);
                UDPSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(Target_IP));
                Console.WriteLine("Starting Recieve");
                Recieve();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + " " + e.StackTrace);
            }
        }

        private void Recieve()
        {
            try
            {
                IPEndPoint LocalIPEndPoint = new
                IPEndPoint(IPAddress.Any, Target_Port);
                EndPoint LocalEndPoint = (EndPoint)LocalIPEndPoint;
                StateObject state = new StateObject();
                state.workSocket = UDPSocket;
                Console.WriteLine("Begin Recieve");
                UDPSocket.BeginReceiveFrom(state.buffer, 0, state.BufferSize, 0, ref LocalEndPoint, new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {

            IPEndPoint LocalIPEndPoint = new
            IPEndPoint(IPAddress.Any, Target_Port);
            EndPoint LocalEndPoint = (EndPoint)LocalIPEndPoint;
            StateObject state = (StateObject)ar.AsyncState;
            Socket client = state.workSocket;
            int bytesRead = client.EndReceiveFrom(ar, ref LocalEndPoint);



            client.BeginReceiveFrom(state.buffer, 0, state.BufferSize, 0, ref LocalEndPoint, new AsyncCallback(ReceiveCallback), state);
        }


        public static void Main()
        {
            CAA o = new CAA();
            Console.ReadLine();
        }

        public class StateObject
        {
            public int BufferSize = 512;
            public Socket workSocket;
            public byte[] buffer;

            public StateObject()
            {
                buffer = new byte[BufferSize];
            }
        }

    }
}
