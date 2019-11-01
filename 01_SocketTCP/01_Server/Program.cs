using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace _01_Server
{
    class Program
    {
            static int port = 9300; // порт для приема входящих запросов
            static void Main(string[] args)
            {
                // получаем адреса для запуска сокета
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
                // создаем сокет
                Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                // связываем сокет с локальной точкой, по которой будем принимать данные
                listenSocket.Bind(ipPoint);
                // начинаем прослушивание
                listenSocket.Listen(10);
                Console.WriteLine("Сервер стартував. Очікуємо підключення...");
                String result = String.Empty;
                Socket socket = listenSocket.Accept();
                while (result != "close")
                {
                    result = String.Empty;
                    // получаем сообщение
                    int bytes = 0; // количество полученных байтов
                    byte[] data = new byte[256]; // буфер для получаемых данных

                    do
                    {
                        bytes = socket.Receive(data);
                        result += Encoding.Unicode.GetString(data, 0, bytes);
                    }
                    while (socket.Available > 0);
                    Console.WriteLine(result);

                    // отправляем ответ
                    string message = "ваше повідомлення отримано";
                    data = Encoding.Unicode.GetBytes(message);
                    socket.Send(data);
                }
                // закрываем сокет

                listenSocket.Shutdown(SocketShutdown.Both);
                listenSocket.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            }
        }
}
