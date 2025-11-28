
using Microsoft.AspNetCore.Mvc;
using RestApi.DTO.Category;
using RestApi.Interfaces;
using RestApi.Mappers;
using RestApi.Models;

namespace RestApi.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        // Dependency Injection (DI) untuk ICategoryRepo Service
        private readonly ICategoryRepo _categoryRepo;
        private readonly IProductRepo _productRepo;

        public CategoryController(ICategoryRepo categoryRepo, IProductRepo productRepo)
        {
            _categoryRepo = categoryRepo;
            _productRepo = productRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryRepo.GetAllAsync();
            var categoryDto = categories
                    .Select(c => c.ToCategoryDto());

            return Ok(new
            {
                statusCode = "200",
                message = "Data categories berhasil diambil",
                Data = categoryDto
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryRepo.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            var categoryDto = category?.ToCategoryDto();
            return Ok(new
            {
                statusCode = "200",
                message = "Data category berhasil diambil",
                Data = categoryDto
            });
        }

        [HttpPost]
        [Route("{ProductId}")]
        public async Task<IActionResult> CreateCategory(int ProductId, [FromBody] CreateCategoryDto createCategoryDto)
        {
            // cek product ada atau tidak
            var productExists = await _productRepo.ProductExists(ProductId);
            if (!productExists)
            {
                return NotFound();
            }

            var category = new Category
            {
                Name = createCategoryDto.Name,
                ProductId = ProductId
            };
            var createdCategory = await _categoryRepo.CreateAsync(category);
            return CreatedAtAction(
                nameof(GetById),
                new { id = createdCategory.Id },
                new
                {
                    Data = createdCategory.ToCategoryDto()
                }
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] int id, [FromBody] UpdateCategoryDto updateDto)
        {
            var category = await _categoryRepo.UpdateAsync(id, updateDto.ToCategoryFromUpdate(id));

            if (category == null)
            {
                return NotFound();
            }
            return StatusCode(200, new
            {
                statusCode = "200",
                message = "Data category berhasil diupdate",
                Data = category.ToCategoryDto()
            });
        }
    }
}