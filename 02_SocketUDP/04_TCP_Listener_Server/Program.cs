using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace _04_TCP_Listener_Server
{
    //---INSTALL NEWTONSOFT.JSON FROM NUGET
    class Program
    {
        static void Main(string[] args)
        {
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8001);
            TcpListener listener = new TcpListener(ip);
            listener.Start();
            Console.WriteLine("start listen...");

            List<Message> activeUsers = new List<Message>();

            //loop
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                NetworkStream stream = client.GetStream();
                StreamReader sr = new StreamReader(stream);
                string json = sr.ReadLine();
                //----user send data
                Message msgFromUser = JsonConvert.DeserializeObject<Message>(json);
                //find this user in active (save if it is new)
                Message sender = activeUsers.FirstOrDefault(x => x.Name == msgFromUser.Name);
                if (sender == null)
                {
                    activeUsers.Add(msgFromUser);
                }
                //---знайти кому юзер надсилає повідомлення серед активних
                Message reciever = activeUsers.FirstOrDefault(x => x.Name == msgFromUser.Reciever);
                if(reciever != null)
                {
                    reciever.InputText = msgFromUser.Name + ": send - " + msgFromUser.OutputText;
                }


                if (sender != null && sender.InputText != null)
                {
                    StreamWriter sw = new StreamWriter(stream);
                    sw.WriteLine(sender.InputText);
                    sender.InputText = null;
                    sw.Close();
                }
                else
                {
                    StreamWriter sw = new StreamWriter(stream);
                    sw.WriteLine("no messages for you");
                    sw.Close();
                }
                sr.Close();
                stream.Close();
                client.Close();
            }
            //loop end

            listener.Stop();
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
