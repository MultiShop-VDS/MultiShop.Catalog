using MultiShop.Catalog.Api.Dtos.CategoryDtos;

namespace MultiShop.Catalog.Api.Dtos.ProductDtos
{
    public class ResultProductsWithCategoryDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageName { get; set; }
        public string Description { get; set; }
        public ResultCategoryDto Category { get; set; }
    }
}
