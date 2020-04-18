using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Services
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> AllContacts();
        Task<IEnumerable<DeletedContact>> DeletedContacts();
        Task<Contact> GetContact(int id);
        Task AddContact(Contact contact);
        Task UpdateContact(Contact contact);
        Task DeleteContact(int id);
    }
}
