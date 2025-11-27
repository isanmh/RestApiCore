using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestApi.DTO.Product;
using RestApi.Mappers;
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
            // var products = await _db.Products.ToListAsync();

            // var products = await _db.Products
            //                 .Select(p => new
            //                 {
            //                     p.Id,
            //                     p.Name,
            //                     p.Price,
            //                 })
            //                 .ToListAsync();

            // DTO & Mapper
            // var products = await _db.Products
            //                 .Select(p => ProductMapper.ToProductDto(p))
            //                 .ToListAsync();

            // include Categories
            var products = await _db.Products
                        .Include(p => p.Categories)
                        .ToListAsync();

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

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductDto request)
        {
            var product = new Product
            {
                Name = request.Name,
                Price = request.Price,
                Description = request.Description,
            };
            // masukan request
            await _db.Products.AddAsync(product);
            // simpan ke database
            await _db.SaveChangesAsync();
            return CreatedAtAction(
                nameof(GetProductById),
                new { id = product.Id }, new
                {
                    StatusCode = 201,
                    Message = "Create product success",
                    Data = product,
                });

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductDto request)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            product.Name = request.Name;
            product.Price = request.Price;
            product.Description = request.Description;
            await _db.SaveChangesAsync();
            var res = new
            {
                StatusCode = 200,
                Message = "Update product success",
                Data = ProductMapper.ToProductDto(product)
            };
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
            return Ok(new
            {
                StatusCode = 200,
                Message = "Delete product success",
            });
        }
    }
}