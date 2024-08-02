using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Api.Dtos.FeatureDtos;
using MultiShop.Catalog.Api.Services.FeatureServices;

namespace MultiShop.Catalog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureController : ControllerBase
    {
        private readonly IFeatureService _FeatureService;

        public FeatureController(IFeatureService FeatureService)
        {
            _FeatureService = FeatureService;
        }

        [HttpGet]
        public async Task<IActionResult> FeatureList()
        {
            List<ResultFeatureDto> resultFeatureDtos = await _FeatureService.GetAllFeatureAsync();
            return Ok(resultFeatureDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeatureById(string id)
        {
            GetByIdFeatureDto getByIdFeatureDto = await _FeatureService.GetByIdFeatureAsync(id);
            return Ok(getByIdFeatureDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeature(CreateFeatureDto createFeatureDto)
        {
            await _FeatureService.CreateFeatureAsync(createFeatureDto);
            return Ok("Feature Created");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFeature(UpdateFeatureDto updateFeatureDto)
        {
            await _FeatureService.UpdateFeatureAsync(updateFeatureDto);
            return Ok("Feature Updated");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFeature(string id)
        {
            await _FeatureService.DeleteFeatureAsync(id);
            return Ok("Feature Deleted");
        }
    }
}
