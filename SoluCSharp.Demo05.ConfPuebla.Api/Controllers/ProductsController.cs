using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoluCSharp.Demo05.ConfPuebla.Api.Data;
using SoluCSharp.Demo05.ConfPuebla.Api.Dtos;
using SoluCSharp.Demo05.ConfPuebla.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoluCSharp.Demo05.ConfPuebla.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetProducts() =>
            await _context.Products.Select(p => new ProductDto(p.Id, p.Name, p.UnitPrice, p.UnitsIntStock)).ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            ProductDto productDto = await _context.Products.FindAsync(id);
            if (productDto == null || productDto?.Id == null) return NotFound();
            return productDto;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            _context.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id}, product);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, Product product)
        {
            var existProd = await _context.Products.AnyAsync(x => x.Id.Equals(id));
            if (!existProd) return NotFound();
            _context.Update(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();
            _context.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
