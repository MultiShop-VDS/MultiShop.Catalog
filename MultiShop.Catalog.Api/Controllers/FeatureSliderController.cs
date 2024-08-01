using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Api.Dtos.FutureSliderDtos;
using MultiShop.Catalog.Api.Services.FeatureSliderServices;

namespace MultiShop.Catalog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureSliderController : ControllerBase
    {
        private readonly IFeatureSliderService _FeatureSliderService;

        public FeatureSliderController(IFeatureSliderService FeatureSliderService)
        {
            _FeatureSliderService = FeatureSliderService;
        }

        [HttpGet]
        public async Task<IActionResult> FeatureSliderList()
        {
            List<ResultFeatureSliderDto> resultFeatureSliderDtos = await _FeatureSliderService.GetAllFeatureSliderAsync();
            return Ok(resultFeatureSliderDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeatureSliderById(string id)
        {
            GetByIdFeatureSliderDto getByIdFeatureSliderDto = await _FeatureSliderService.GetByIdFeatureSliderAsync(id);
            return Ok(getByIdFeatureSliderDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeatureSlider(CreateFeatureSliderDto createFeatureSliderDto)
        {
            await _FeatureSliderService.CreateFeatureSliderAsync(createFeatureSliderDto);
            return Ok("FeatureSlider Created");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFeatureSlider(UpdateFeatureSliderDto updateFeatureSliderDto)
        {
            await _FeatureSliderService.UpdateFeatureSliderAsync(updateFeatureSliderDto);
            return Ok("FeatureSlider Updated");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFeatureSlider(string id)
        {
            await _FeatureSliderService.DeleteFeatureSliderAsync(id);
            return Ok("FeatureSlider Deleted");
        }
    }
}
