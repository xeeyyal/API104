using API104.DAL;
using API104.DTOs;
using API104.Entities;
using API104.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API104.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IRepository _repository;

        public CategoriesController(AppDbContext context, IRepository repository)
        {
            _context = context;
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> Get(int page=1,int take=3)
        {
            IEnumerable<Category> categories = await _repository.GetAllAsync();

            //return StatusCode(StatusCodes.Status200OK,categories);

            return Ok(categories);
        }
        [HttpGet("{id}")]
        //[Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest); //return BadRequest();

            Category? category= await _repository.GetByIdAsync(id);

            if (category is null) return StatusCode(StatusCodes.Status404NotFound);

            return StatusCode(StatusCodes.Status200OK,category);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateCategoryDto categoryDto)
        {
            Category category = new()
            {
                Name = categoryDto.Name,
            };

            await _repository.AddAsync(category);
            await _repository.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created,category);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,string name)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest); //return BadRequest();

            Category? existed = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (existed is null) return StatusCode(StatusCodes.Status404NotFound);

            existed.Name = name;

            _repository.Update(existed);
            await _repository.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest); //return BadRequest();

            Category? existed = await _repository.GetByIdAsync(id);

            if (existed is null) return StatusCode(StatusCodes.Status404NotFound);

            _context.Categories.Remove(existed);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
