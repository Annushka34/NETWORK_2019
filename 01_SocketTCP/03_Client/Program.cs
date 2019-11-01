using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace _03_Client
{
    class Program
    {
        // адрес и порт сервера, к которому будем подключаться
        static int port = 9300; // порт сервера
        static string address = "127.0.0.1"; // адрес сервера
        static void Main(string[] args)
        {
            try
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);

                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                // подключаемся к удаленному хосту
                socket.Connect(ipPoint);
                string message = "";

                while (true)
                {
                    Console.Write("Введите сообщение:");
                    message = Console.ReadLine();
                    byte[] data = Encoding.Unicode.GetBytes(message);
                    socket.Send(data);

                    // получаем ответ
                    data = new byte[256]; // буфер для ответа
                    string result = String.Empty;
                    int bytes = 0; // количество полученных байт

                    do
                    {
                        bytes = socket.Receive(data, data.Length, 0);
                        result += Encoding.Unicode.GetString(data, 0, bytes);
                    }
                    while (socket.Available > 0);
                    Console.WriteLine("ответ сервера: " + result);

                }
                // закрываем сокет
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Read();
        }
    }
}
