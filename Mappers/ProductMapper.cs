using RestApi.DTO.Product;
using RestApi.Models;

namespace RestApi.Mappers
{
    public static class ProductMapper
    {
        public static ProductDto ToProductDto(Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description
            };
        }
    }
}