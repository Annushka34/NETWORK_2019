using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace _01_Socket
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress ip = IPAddress.Parse("66.85.73.147");
            IPEndPoint ep = new IPEndPoint(ip, 80);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            string result = String.Empty;
            try
            {
                socket.Connect(ep);
                if (socket.Connected)
                {
                    String strSend = "GET/index.html HTTP/1.1";
                    socket.Send(System.Text.Encoding.ASCII.GetBytes(strSend));
                    byte[] buffer = new byte[1024];
                    int ind;
                    do
                    {
                        ind = socket.Receive(buffer);
                        result += System.Text.Encoding.ASCII.GetString(buffer, 0, ind);
                    }
                    while (ind > 0);
                    Console.WriteLine(result);
                }
                else
                    Console.WriteLine("Error");

            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
        }
    }
}
