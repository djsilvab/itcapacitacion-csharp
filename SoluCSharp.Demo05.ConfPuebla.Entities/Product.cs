using System;

namespace SoluCSharp.Demo05.ConfPuebla.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsIntStock { get; set; }
        public int CategoryId { get; set; }
    }
}
