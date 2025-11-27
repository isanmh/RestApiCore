
using Microsoft.AspNetCore.Mvc;
using RestApi.Interfaces;
using RestApi.Mappers;

namespace RestApi.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        // Dependency Injection (DI) untuk ICategoryRepo Service
        private readonly ICategoryRepo _categoryRepo;

        public CategoryController(ICategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
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
    }
}