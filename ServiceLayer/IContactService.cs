using EntityLayer;

namespace ServiceLayer
{
    public interface IContactService
    {
        Task<List<Contact>> GetContactsAsync();
        Task<Contact> GetContactAsync(int id);
        Task AddContactAsync(Contact contact);
        Task UpdateContactAsync(Contact contact);
        Task DeleteContactAsync(int id);
        Task<bool> ContactExists(int id);
    }
}
