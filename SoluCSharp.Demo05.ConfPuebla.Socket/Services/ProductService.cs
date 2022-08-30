using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using SoluCSharp.Demo05.ConfPuebla.Entities;
using System.Text.Json;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace SoluCSharp.Demo05.ConfPuebla.Socket.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;        
        private readonly string urlAPI;

        public ProductService(HttpClient _httpClient)
        {
            this._httpClient = _httpClient;
            var strUrlApi = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings")["UrlAPI"];
            this.urlAPI = strUrlApi;
        }

        public async Task<Product> InsertOne(Product product)
        {
            Product result = null;
            try
            {
                var jsonProduct = JsonSerializer.Serialize(product);
                var content = new StringContent(jsonProduct, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(urlAPI, content);
                if (response.IsSuccessStatusCode)
                {
                    jsonProduct = await response.Content.ReadAsStringAsync();
                    result = JsonSerializer.Deserialize<Product>(jsonProduct, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                else
                {
                    string contentx = response.StatusCode.ToString();
                    System.IO.File.WriteAllText(@"C:\inetpub\wwwroot\ConfPubebla.Socket\LogError.txt", contentx);
                }
            }
            catch (Exception ex)
            {
                System.IO.File.WriteAllText(@"C:\inetpub\wwwroot\ConfPubebla.Socket\LogError.txt", ex.Message);
            }
          

            return result;
        }

        public async Task<bool> UpdateOne(int id, Product product)
        {            
            var jsonProduct = JsonSerializer.Serialize(product);
            var content = new StringContent(jsonProduct, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{urlAPI}/{ id }", content);
            return response.IsSuccessStatusCode;
        }
    }
}
