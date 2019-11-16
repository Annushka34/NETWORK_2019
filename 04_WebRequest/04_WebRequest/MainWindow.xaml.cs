using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

namespace _04_WebRequest
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

        private async void BtnShowAll_Click(object sender, RoutedEventArgs e)
        {
            //WebRequest request = WebRequest.Create(@"http://www.annushka.somee.com/api/Good/all");
            //WebResponse response =  request.GetResponse();

            //Stream stream = response.GetResponseStream();
            //using (StreamReader sr = new StreamReader(stream))                
            //{
            //    result.Text =  sr.ReadToEnd();
            //}
            //response.Close();

            HttpClient client = new HttpClient();          
            HttpResponseMessage httpResult = await client.GetAsync(@"https://localhost:44379/api/Good/all");
            string data = await httpResult.Content.ReadAsStringAsync();
            var listGoods = JsonConvert.DeserializeObject<List<Good>>(data);
            result.Items.Clear();
            foreach (var item in listGoods)
            {
                result.Items.Add(item);
            }
           // result.DataContext = listGoods;
        }
        //---POST---
        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Good newGood = new Good();
                newGood.Title = txtName.Text;
                newGood.Description = txtDescr.Text;
                newGood.Price = int.Parse(txtPrice.Text);
                string json = JsonConvert.SerializeObject(newGood);


                //WebRequest request = WebRequest.Create(@"http://www.annushka.somee.com/api/Good/Create");
                //request.Method = "POST";
                //request.ContentType = "application/json";
                //var stream =  request.GetRequestStream();
                //using (StreamWriter sw = new StreamWriter(stream))
                //{
                //    sw.Write(json);
                //}
                //WebResponse response = request.GetResponse();
                //Stream responceStream = response.GetResponseStream();
                //using (StreamReader sr = new StreamReader(responceStream))
                //{
                //    result.Text = sr.ReadToEnd();
                //}
                //response.Close();

                HttpClient client = new HttpClient();
                StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage httpResult = await client.PostAsync(@"http://www.annushka.somee.com/api/Good/Create", stringContent);
                btnSave.Content = await httpResult.Content.ReadAsStringAsync();
            }
            catch
            {

            }
        }

        private async void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (result.SelectedIndex != -1)
            {
                Good good = result.SelectedItem as Good;
                EditWindow editWindow = new EditWindow(good);
                editWindow.Show();
            }
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var Id = (result.SelectedItem as Good).Id;
            HttpClient client = new HttpClient();

            //UriBuilder builder = new UriBuilder(@"http://www.annushka.somee.com/api/Good/Delete");
            //builder.Query = "id=" + Id;
            //use builder.Uri;

            HttpResponseMessage httpResult = await client.DeleteAsync(@"http://www.annushka.somee.com/api/Good/Delete?id="+Id);
            btnDelete.Content = await httpResult.Content.ReadAsStringAsync();
        }
    }
}
