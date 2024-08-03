using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Api.Dtos.OfferDiscountDtos;
using MultiShop.Catalog.Api.Services.OfferDiscountServices;

namespace MultiShop.Catalog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferDiscountController : ControllerBase
    {
        private readonly IOfferDiscountService _OfferDiscountService;

        public OfferDiscountController(IOfferDiscountService OfferDiscountService)
        {
            _OfferDiscountService = OfferDiscountService;
        }

        [HttpGet]
        public async Task<IActionResult> OfferDiscountList()
        {
            List<ResultOfferDiscountDto> resultOfferDiscountDtos = await _OfferDiscountService.GetAllOfferDiscountAsync();
            return Ok(resultOfferDiscountDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOfferDiscountById(string id)
        {
            GetByIdOfferDiscountDto getByIdOfferDiscountDto = await _OfferDiscountService.GetByIdOfferDiscountAsync(id);
            return Ok(getByIdOfferDiscountDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOfferDiscount(CreateOfferDiscountDto createOfferDiscountDto)
        {
            await _OfferDiscountService.CreateOfferDiscountAsync(createOfferDiscountDto);
            return Ok("OfferDiscount Created");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOfferDiscount(UpdateOfferDiscountDto updateOfferDiscountDto)
        {
            await _OfferDiscountService.UpdateOfferDiscountAsync(updateOfferDiscountDto);
            return Ok("OfferDiscount Updated");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOfferDiscount(string id)
        {
            await _OfferDiscountService.DeleteOfferDiscountAsync(id);
            return Ok("OfferDiscount Deleted");
        }
    }
}
