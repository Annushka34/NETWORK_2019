using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace _05_PhoneBook
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
            HttpClient client = new HttpClient();
            HttpResponseMessage httpResult = await client.GetAsync(@"https://localhost:44305/api/Phone");
            string data = await httpResult.Content.ReadAsStringAsync();
            var listPhones = JsonConvert.DeserializeObject<List<Phone>>(data);

            result.Text = listPhones.FirstOrDefault()?.PhoneNumber;
        }

        private async void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            PhoneModel phone = new PhoneModel();
            phone.Message = Message.Text;
            phone.Name = Name.Text;
            phone.Surname = Surname.Text;
            phone.PhoneNumber = Phone.Text;
            string json = JsonConvert.SerializeObject(phone);


            HttpClient client = new HttpClient();
            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResult = await client.PostAsync(@"https://localhost:44305/api/Phone", stringContent);
            AddBtn.Content = await httpResult.Content.ReadAsStringAsync();
        }
    }
}
