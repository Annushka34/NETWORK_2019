using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace _03_TCPLIstener_Client
{
    class Program
    {
        //---INSTALL NEWTONSOFT.JSON FROM NUGET
        static void Main(string[] args)
        {
            int port = 8001;
            TcpClient client = null;

            Message message = new Message();
            Console.WriteLine("Enter your name: ");
            message.Name = Console.ReadLine();

            while (true)
            {
                Console.Write("enter msg: ");
                message.OutputText = Console.ReadLine();
                Console.Write("enter receiver: ");
                message.Reciever = Console.ReadLine();

                client = new TcpClient("127.0.0.1", port);
                NetworkStream stream = client.GetStream();
                StreamWriter writer = new StreamWriter(stream);
                //----conver message object to string---
                string json = JsonConvert.SerializeObject(message);
                writer.WriteLine(json);
                writer.Flush();

                StreamReader reader = new StreamReader(stream);
                string answer = reader.ReadLine();
                Console.WriteLine("Получен ответ: " + answer);

                reader.Close();
                writer.Close();
                stream.Close();
            }
            client.Close();
        }
    }

    class Message
    {
        public string Name { get; set; }
        public string InputText { get; set; }
        public string OutputText { get; set; }
        public string Reciever { get; set; }
    }
}
