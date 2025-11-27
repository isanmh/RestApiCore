
using System.ComponentModel.DataAnnotations;

namespace RestApi.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }

        // relasi ke data Product (navigation property)
        public int? ProductId { get; set; }
        public Product? Product { get; set; }
    }
}