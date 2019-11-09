using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _03_SMTP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {            
            string[] mails = new string[]
            {
               "yuryudod@gmail.com","etonea27@gmail.com","annsamoluk@mail.ru", "Anatolii.Zamlynnyi@gmail.com", "mykola.melnichuk@gmail.com", "radyvoniuk.kate@gmail.com"
            };
            foreach (var item in mails)
            {
              SendMail(item);
            }

            btnSend.Content = "SENDED";
            //----
        }


        async void SendMail(string address)
        {
            MailAddress from = new MailAddress(
                "annsamoluk82@gmail.com", "ORIFLAME");
           // MailAddress from = new MailAddress(SmtpSettings.UserName, "ORIFLAME");
            // MailAddress to = new MailAddress(txtTo.Text);
            MailAddress to = new MailAddress(address);

            MailMessage msg = new MailMessage(from, to);

            //msg.Body = txtText.Text;
            
            //---IF READ FROM TEMPLATE HTML FILE
            msg.IsBodyHtml = true;
            using (StreamReader sr = new StreamReader(Directory.GetCurrentDirectory()
                + "/mail.html"))
            {
                msg.Body = sr.ReadToEnd();
            }

            msg.Subject = txtHeader.Text;

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
          //  SmtpClient client = new SmtpClient(SmtpSettings.Host, SmtpSettings.Port);
            client.Credentials = new NetworkCredential("annsamoluk82@gmail.com", 
                SmtpSettings.Password);
            client.EnableSsl = true;

            await client.SendMailAsync(msg);
        }
    }
}
