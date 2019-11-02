using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace _02_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8001);
         //   IPEndPoint ip = new IPEndPoint(IPAddress.Parse("224.0.0.1"), 8001);
            // UdpClient server = new UdpClient(ip);
            //UdpClient server = new UdpClient("127.0.0.1", 8001);
            UdpClient server = new UdpClient();
            string msg = "";
            while (msg != "close")
            {
                Console.WriteLine("enter message from server:");
                msg = Console.ReadLine();
                var data = Encoding.Unicode.GetBytes(msg);
                server.Send(data, data.Length, ip);
                Console.WriteLine("Sended!");
            }
            server.Close();
        }
    }
}
