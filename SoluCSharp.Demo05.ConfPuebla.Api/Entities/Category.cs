using SoluCSharp.Demo05.ConfPuebla.Api.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoluCSharp.Demo05.ConfPuebla.Api.Entities
{
    public class Category
    {
        public int Id { get; set; }
        //[Required]
        //[MaxLength(50, ErrorMessage = "El campo {0} debe ser de maximo {1} caracteres")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();

        public static implicit operator CategoryDto(Category c)
        {            
            return new CategoryDto(c?.Id, c?.Name, c?.Description, c?.Picture);            
        }
    }
}
