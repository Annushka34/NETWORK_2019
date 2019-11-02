using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace _02_SocketUDP
{
    class Program
    {
        static void Main(string[] args)
        {
            IPEndPoint ip = null;
            UdpClient client = new UdpClient(8001);
            client.JoinMulticastGroup(IPAddress.Parse("127.0.0.1"), 20);
            string msg = "";
            while (msg != "close")
            {
                var data = client.Receive(ref ip);
                msg = Encoding.Unicode.GetString(data);
                Console.WriteLine("SERVER SEND ME: " + msg);
            }
            client.Close();


            //if broadcast
            //224.0.0.0 до 239.255.255.255.
        }
    }
}
