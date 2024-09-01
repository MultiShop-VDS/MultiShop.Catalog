using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Api.Dtos.ContactDtos;
using MultiShop.Catalog.Api.Services.ContactServices;

namespace MultiShop.Catalog.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _ContactService;

        public ContactController(IContactService ContactService)
        {
            _ContactService = ContactService;
        }

        [HttpGet]
        public async Task<IActionResult> ContactList()
        {
            List<ResultContactDto> resultContactDtos = await _ContactService.GetAllContactAsync();
            return Ok(resultContactDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContactById(string id)
        {
            GetByIdContactDto getByIdContactDto = await _ContactService.GetByIdContactAsync(id);
            return Ok(getByIdContactDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact(CreateContactDto createContactDto)
        {
            await _ContactService.CreateContactAsync(createContactDto);
            return Ok("Contact Created");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateContact(UpdateContactDto updateContactDto)
        {
            await _ContactService.UpdateContactAsync(updateContactDto);
            return Ok("Contact Updated");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteContact(string id)
        {
            await _ContactService.DeleteContactAsync(id);
            return Ok("Contact Deleted");
        }
    }
}
