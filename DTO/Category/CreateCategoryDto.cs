
using System.ComponentModel.DataAnnotations;

namespace RestApi.DTO.Category
{
    public class CreateCategoryDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Nama category minimal 3 karakter")]
        public string? Name { get; set; }
    }
}