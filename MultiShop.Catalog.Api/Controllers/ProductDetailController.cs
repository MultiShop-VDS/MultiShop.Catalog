using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Api.Dtos.ProductsDetailDtos;
using MultiShop.Catalog.Api.Services.ProductDetailServices;

namespace MultiShop.Catalog.Api.Controllers
{
    [Authorize]

    [Route("api/[controller]")]
    [ApiController]
    public class ProductDetailController : ControllerBase
    {
        private readonly IProductDetailService _ProductDetailService;

        public ProductDetailController(IProductDetailService ProductDetailService)
        {
            _ProductDetailService = ProductDetailService;
        }

        [HttpGet]
        public async Task<IActionResult> ProductDetailList()
        {
            List<ResultProductDetailDto> resultProductDetailDtos = await _ProductDetailService.GetAllProductDetailAsync();
            return Ok(resultProductDetailDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductDetailById(string id)
        {
            GetByIdProductDetailDto getByIdProductDetailDto = await _ProductDetailService.GetByIdProductDetailAsync(id);
            return Ok(getByIdProductDetailDto);
        }

        [HttpGet("GetByProductIdProductDetailAsync")]
        public async Task<IActionResult> GetByProductIdProductDetailAsync(string id)
        {
            var values = await _ProductDetailService.GetByProductIdProductDetailAsync(id);
            return Ok(values);
        }


        [HttpPost]
        public async Task<IActionResult> CreateProductDetail(CreateProductDetailDto createProductDetailDto)
        {
            await _ProductDetailService.CreateProductDetailAsync(createProductDetailDto);
            return Ok("ProductDetail Created");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProductDetail(UpdateProductDetailDto updateProductDetailDto)
        {
            await _ProductDetailService.UpdateProductDetailAsync(updateProductDetailDto);
            return Ok("ProductDetail Updated");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProductDetail(string id)
        {
            await _ProductDetailService.DeleteProductDetailAsync(id);
            return Ok("ProductDetail Deleted");
        }
    }
}
