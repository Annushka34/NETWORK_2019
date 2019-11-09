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
using System.Windows.Shapes;

namespace _04_WebRequest
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        Good _good;
        public EditWindow()
        {
            InitializeComponent();
        }

        public EditWindow(Good good)
        {
            InitializeComponent();
            txtName.Text = good.Title;
            txtDescr.Text = good.Description;
            txtPrice.Text = good.Price.ToString();
            _good = good;
        }

        private async Task BtnEdit_ClickAsync(object sender, RoutedEventArgs e)
        {
         
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            _good.Title = txtName.Text;
            _good.Description = txtDescr.Text;
            _good.Price = int.Parse(txtPrice.Text);
            string json = JsonConvert.SerializeObject(_good);

            HttpClient client = new HttpClient();
            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResult = await client.PutAsync(@"http://www.annushka.somee.com/api/Good/Edit", stringContent);
            btnEdit.Content = await httpResult.Content.ReadAsStringAsync();
        }
    }
}
