using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Api.Dtos.SpecialOfferDtos;
using MultiShop.Catalog.Api.Services.SpecialOfferServices;

namespace MultiShop.Catalog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialOfferController : ControllerBase
    {
        private readonly ISpecialOfferService _SpecialOfferService;

        public SpecialOfferController(ISpecialOfferService SpecialOfferService)
        {
            _SpecialOfferService = SpecialOfferService;
        }

        [HttpGet]
        public async Task<IActionResult> SpecialOfferList()
        {
            List<ResultSpecialOfferDto> resultSpecialOfferDtos = await _SpecialOfferService.GetAllSpecialOfferAsync();
            return Ok(resultSpecialOfferDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSpecialOfferById(string id)
        {
            GetByIdSpecialOfferDto getByIdSpecialOfferDto = await _SpecialOfferService.GetByIdSpecialOfferAsync(id);
            return Ok(getByIdSpecialOfferDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSpecialOffer(CreateSpecialOfferDto createSpecialOfferDto)
        {
            await _SpecialOfferService.CreateSpecialOfferAsync(createSpecialOfferDto);
            return Ok("SpecialOffer Created");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSpecialOffer(UpdateSpecialOfferDto updateSpecialOfferDto)
        {
            await _SpecialOfferService.UpdateSpecialOfferAsync(updateSpecialOfferDto);
            return Ok("SpecialOffer Updated");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSpecialOffer(string id)
        {
            await _SpecialOfferService.DeleteSpecialOfferAsync(id);
            return Ok("SpecialOffer Deleted");
        }
    }
}
