using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class DeletedContactsController : ControllerBase
    {
        private readonly IContactService _service;

        public DeletedContactsController(IContactService service)
        {
            _service = service;
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        public async Task<IActionResult> DeletedContacts()
        {
            var result = await _service.DeletedContacts();
            return Ok(result);
        }
    }
}
