using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Api.Dtos.BrandsDto;
using MultiShop.Catalog.Api.Services.BrandServices;

namespace MultiShop.Catalog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _BrandService;

        public BrandController(IBrandService BrandService)
        {
            _BrandService = BrandService;
        }

        [HttpGet]
        public async Task<IActionResult> BrandList()
        {
            List<ResultBrandDto> resultBrandDtos = await _BrandService.GetAllBrandAsync();
            return Ok(resultBrandDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBrandById(string id)
        {
            GetByIdBrandDto getByIdBrandDto = await _BrandService.GetByIdBrandAsync(id);
            return Ok(getByIdBrandDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrand(CreateBrandDto createBrandDto)
        {
            await _BrandService.CreateBrandAsync(createBrandDto);
            return Ok("Brand Created");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBrand(UpdateBrandDto updateBrandDto)
        {
            await _BrandService.UpdateBrandAsync(updateBrandDto);
            return Ok("Brand Updated");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBrand(string id)
        {
            await _BrandService.DeleteBrandAsync(id);
            return Ok("Brand Deleted");
        }
    }
}
