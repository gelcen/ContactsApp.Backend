using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using WebApi.Entities;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var contacts = await _contactService.AllContacts();
            return Ok(contacts);
        }

        [HttpGet("mine/{user}")]
        public async Task<IActionResult> GetAll(string user)
        {
            var contacts = await _contactService.AllContacts();
            var usersContacts = contacts.Where(c => c.User == user);
            return Ok(usersContacts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var contact = await _contactService.GetContact(id);
            return Ok(contact);
        }

        [HttpPost]
        public async Task<IActionResult> AddContact([FromBody] AddContactModel model)
        {
            await _contactService.AddContact(new Contact
            {
                Surname = model.Surname,
                Name = model.Name,
                Patronymic = model.Patronymic,
                Organization = model.Organization,
                Position = model.Position,
                Email = model.Email,
                Phone = model.Phone,
                User = model.User
            });
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateContact([FromBody] Contact contact)
        {
            await _contactService.UpdateContact(contact);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            await _contactService.DeleteContact(id);
            return Ok();
        }
    }
}
