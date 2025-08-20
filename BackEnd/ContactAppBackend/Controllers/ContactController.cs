using Application.Common;
using Application.DTOs.Actions;
using Application.Serializer;
using ContactAppBackend.Models.Dtos;
using ContactAppBackend.Models.Dtos.ContactDto;
using ContactAppBackend.Services.ContactServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace ContactAppBackend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IJsonFieldsSerializer _jsonFieldsSerializer;

        public ContactController(IJsonFieldsSerializer jsonFieldsSerializer, IContactService contactService)
        {
            _jsonFieldsSerializer = jsonFieldsSerializer;
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            // Directly call the service and let middleware handle exceptions.
            var result = await _contactService.GetAllContactsAsync();
            var apiResponse = new ApiResponse(true, "Contacts retrieved successfully.", StatusCodes.Status200OK, result);
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(apiResponse, string.Empty));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await _contactService.GetContactByIdAsync(id);

            // Handle the expected "not found" case, which is not an exception.
            if (result == null)
            {
                var notFoundResponse = new ApiResponse(false, "Contact not found.", StatusCodes.Status404NotFound);
                return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(notFoundResponse, string.Empty));
            }

            var apiResponse = new ApiResponse(true, "Contact retrieved successfully.", StatusCodes.Status200OK, result);
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(apiResponse, string.Empty));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateContactDto createDto, CancellationToken cancellationToken)
        {
            var result = await _contactService.CreateContactAsync(createDto);
            var apiResponse = new ApiResponse(true, "Contact created successfully.", StatusCodes.Status201Created, result);
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(apiResponse, string.Empty));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateContactDto updateDto, CancellationToken cancellationToken)
        {
            var result = await _contactService.UpdateContactAsync(updateDto);

            if (result == null)
            {
                var notFoundResponse = new ApiResponse(false, "Contact not found to update.", StatusCodes.Status404NotFound);
                return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(notFoundResponse, string.Empty));
            }

            var apiResponse = new ApiResponse(true, "Contact updated successfully.", StatusCodes.Status200OK, result);
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(apiResponse, string.Empty));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var success = await _contactService.DeleteContactAsync(id);

            if (!success)
            {
                var notFoundResponse = new ApiResponse(false, "Contact not found to delete.", StatusCodes.Status404NotFound);
                return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(notFoundResponse, string.Empty));
            }

            var apiResponse = new ApiResponse(true, "Contact deleted successfully.", StatusCodes.Status200OK);
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(apiResponse, string.Empty));
        }
    }
}