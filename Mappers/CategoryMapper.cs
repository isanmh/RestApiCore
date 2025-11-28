
using RestApi.DTO.Category;
using RestApi.Models;

namespace RestApi.Mappers
{
    public static class CategoryMapper
    {
        public static CategoryDto ToCategoryDto(this Category category)
        {
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                ProductId = category.ProductId
            };
        }
        public static Category ToCategoryFromUpdate(this UpdateCategoryDto category, int productId)
        {
            return new Category
            {
                Name = category.Name,
                ProductId = productId
            };
        }
    }
}