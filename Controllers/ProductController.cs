using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestApi.Models;

namespace RestApi.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ProductController(AppDbContext db)
        {
            _db = db;
        }
        // test api
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _db.Products.ToListAsync();
            return Ok(new
            {
                StatusCode = 200,
                Message = "Get all products success",
                Data = products
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            if (_db.Products == null)
            {
                return NotFound();
            }
            // var product = await _db.Products.FindAsync(id);
            var products = await _db.Products
                            .Where(p => p.Id == id)
                            .ToListAsync();
            if (products == null)
            {
                return NotFound();
            }
            return Ok(new
            {
                StatusCode = 200,
                Message = "Get product by id success",
                Data = products,
            });
        }
    }
}