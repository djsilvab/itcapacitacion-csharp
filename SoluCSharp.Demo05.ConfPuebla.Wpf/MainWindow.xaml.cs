using Microsoft.AspNetCore.SignalR.Client;
using SoluCSharp.Demo05.ConfPuebla.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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

namespace SoluCSharp.Demo05.ConfPuebla.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Product> products;
        private readonly HubConnection _hubConnection;

        public MainWindow()
        {
            InitializeComponent();
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("http://localhost:8099/ConfPubebla.Socket/northwindhub")
                .WithAutomaticReconnect()
                .Build();

            _hubConnection.On<Product>("ReceiveInsertProduct", product => InsertProduct(product));
            _hubConnection.On<int, Product>("ReceiveUpdateProduct", (id, product) => UpdateProduct(id, product) );
            _hubConnection.StartAsync().GetAwaiter();

            btnGetProducts.Click += async (sender, e) =>
            {
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync("http://localhost:8099/ConfPuebla.Api/api/Products");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    products = JsonSerializer.Deserialize<List<Product>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    dgProducts.ItemsSource = products;
                }
            };

            btnInsertProduct.Click += async (sender, e) =>
            {
                var product = new Product
                {
                    Name = "Producto de WPF-41",
                    UnitPrice = 415.52M,
                    UnitsIntStock = 150,
                    CategoryId = 1
                };

                await _hubConnection.InvokeAsync("SendInsertProduct", product);                
            };

            btnUpdateProduct.Click += async (sender, e) =>
            {
                var product = new Product
                {
                    Id = 1,
                    Name = "Producto de WPF Update",
                    UnitPrice = 46.52M,
                    UnitsIntStock = 110,
                    CategoryId = 2
                };

                await _hubConnection.InvokeAsync("SendUpdateProduct", product.Id, product);
            };
        }

        public void InsertProduct(Product product)
        {
            products.Add(product);
            dgProducts.Items.Refresh();
        }

        public void UpdateProduct(int id, Product product)
        {
            products.RemoveAt(id - 1);
            products.Insert(id - 1, product);
            dgProducts.Items.Refresh();
        }

    }
}
