using Microsoft.AspNetCore.Mvc;
using Sample.Api.Extensions;
using Sample.Api.RequestResponse;
using Sample.Domain.Contacts;

namespace Sample.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _service;

        public ContactController(IContactService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactDto>>> GetAsync(CancellationToken ct)
        {
            var results = await _service.GetAllAsync(ct);
            return Ok(results);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ContactDto>> GetByIdAsync(CancellationToken ct)
        {
            var results = await _service.GetAllAsync(ct);
            return Ok(results);
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] ContactRequest request, CancellationToken ct)
        {
            if(request is null)
            {
                return BadRequest();
            }

            var result = await _service.CreateAsync(request.Name, request.Email, request.PhoneNumber, ct);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Id });
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> PutAsync([FromRoute] Guid id, [FromBody] ContactRequest request, CancellationToken ct)
        {
            var dto = request.ToDto(id);
            var results = await _service.UpdateAsync(dto, ct);
            return Ok(results);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteAsync(Guid id, CancellationToken ct)
        {
            await _service.DeleteAsync(id, ct);
            return NoContent();
        }
    }
}
