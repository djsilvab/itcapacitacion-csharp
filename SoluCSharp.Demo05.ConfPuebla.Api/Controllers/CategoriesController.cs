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
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDto>>> GetCategories() =>
            await _context.Categories.Select(x => new CategoryDto(x.Id, x.Name, x.Description, x.Picture)).ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            System.IO.File.AppendAllText(@"C:\inetpub\wwwroot\ConfPuebla.Api\LogError.txt", "david");
            //CategoryDto categoryDto = await _context.Categories.FindAsync(id);
            //if (categoryDto == null || categoryDto?.Id == null) return NotFound();
            //return categoryDto;
            CategoryDto categoryDto = null;
            try
            {
                categoryDto = await _context.Categories.FindAsync(id);
                if (categoryDto == null || categoryDto?.Id == null) return NotFound();                
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText(@"C:\inetpub\wwwroot\ConfPuebla.Api\LogError.txt", ex.Message);
            }

            return categoryDto;
        }

        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory(Category category)
        {
            //var category = new Category { };

            _context.Add(category);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategory(int id, Category category)
        {
            var existCateg = await _context.Categories.AnyAsync(x => x.Id.Equals(id));
            if (!existCateg) return NotFound();
            _context.Update(category);
            await _context.SaveChangesAsync();
            return NoContent();            
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();
            _context.Remove(category);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
