
namespace RestApi.DTO.Category
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        // untuk product id
        public int? ProductId { get; set; }
    }
}