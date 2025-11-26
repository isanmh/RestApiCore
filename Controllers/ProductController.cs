using Microsoft.AspNetCore.Mvc;

namespace RestApi.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        // test api
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(new { Message = "Get all products success" });
        }
    }
}