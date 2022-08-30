using Microsoft.AspNetCore.SignalR;
using SoluCSharp.Demo05.ConfPuebla.Entities;
using SoluCSharp.Demo05.ConfPuebla.Socket.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoluCSharp.Demo05.ConfPuebla.Socket.Hubs
{
    public class NorthwindHub : Hub
    {
        private readonly ProductService _service;

        public NorthwindHub(ProductService _service)
        {
            this._service = _service;
        }

        public async Task SendInsertProduct(Product product)
        {
            var result = await _service.InsertOne(product);
            if (result != null)
            {
                await Clients.All.SendAsync("ReceiveInsertProduct", result);
            }
        }

        public async Task SendUpdateProduct(int id, Product product)
        {
            var result = await _service.UpdateOne(id, product);
            if (result)
            {
                await Clients.All.SendAsync("ReceiveUpdateProduct", id, product);
            }
        }
    }
}
