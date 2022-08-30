using SoluCSharp.Demo05.ConfPuebla.Api.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoluCSharp.Demo05.ConfPuebla.Api.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsIntStock { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }

        public static implicit operator ProductDto(Product p)
        {
            return new ProductDto(p?.Id, p?.Name, p?.UnitPrice, p?.UnitsIntStock);
        }
    }
}
