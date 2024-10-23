using IndustryConnect_Week5_WebApi.Dtos;
using IndustryConnect_Week5_WebApi.Models;

namespace IndustryConnect_Week5_WebApi.Mappers
{
    public static class ProductMapper
    {
        public static Product DtoToEntity(ProductDto productDto)
        {
            var entity = new Product
            {
                Id = productDto.Id,
                Name = productDto.Name,
                Description = productDto.Description,
                Active = productDto.Active,
                Price = productDto.Price
            };

            return entity;
        }

        public static ProductDto EntityToDto(Product product)
        {
            var dto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Active = product.Active,
                Price = product.Price
            };

            return dto;
        }
    }
}
