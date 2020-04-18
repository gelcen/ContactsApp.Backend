using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Services
{
    public class ContactService : IContactService
    {
        private readonly ContactsAppContext _context;

        public ContactService(ContactsAppContext context)
        {
            _context = context;
        }
        public async Task AddContact(Contact contact)
        {
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Contact>> AllContacts()
        {
            var list = await _context.Contacts.ToListAsync();
            return list;
        }

        public async Task DeleteContact(int id)
        {
            var contact = await GetContact(id);
            DeletedContact deletedOne = new DeletedContact
            {
                Surname = contact.Surname,
                Name = contact.Name,
                Patronymic = contact.Patronymic,
                Organization = contact.Organization,
                Position = contact.Position,
                Email = contact.Email,
                Phone = contact.Phone,
                User = contact.User
            };
            _context.DeletedContacts.Add(deletedOne);
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DeletedContact>> DeletedContacts()
        {
            var list = await _context.DeletedContacts.ToListAsync();
            return list;
        }

        public async Task<Contact> GetContact(int id)
        {
            var contact = await _context.Contacts
                .FirstOrDefaultAsync(c => c.Id == id);
            return contact;
        }


        public async Task UpdateContact(Contact contact)
        {
            _context.Contacts.Update(contact);
            await _context.SaveChangesAsync();
        }
    }
}
