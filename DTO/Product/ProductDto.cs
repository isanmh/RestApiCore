
using System.ComponentModel.DataAnnotations;
using RestApi.DTO.Category;

namespace RestApi.DTO.Product
{
    public class ProductDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nama produk wajib diisi")]
        [StringLength(50, ErrorMessage = "Nama produk tidak boleh lebih dari 50 karakter")]
        public string? Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string? Description { get; set; }

        // list Categories
        public List<CategoryDto>? Categories { get; set; }

        // public List<CategoryDto> Categories { get; set; } = new List<CategoryDto>();

    }
}