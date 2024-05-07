using DataAccessLayer;
using EntityLayer;
using Microsoft.EntityFrameworkCore;

namespace ServiceLayer
{
    public class ContactService : IContactService
    {
        private readonly DatabaseContext _context;

        public ContactService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task AddContactAsync(Contact contact)
        {
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteContactAsync(int id)
        {
            var contact = await GetContactAsync(id);
            if (contact != null)
            {
                _context.Contacts.Remove(contact);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Contact> GetContactAsync(int id)
        {
            return await _context.Contacts.FindAsync(id);
        }

        public async Task<List<Contact>> GetContactsAsync()
        {
            return await _context.Contacts.ToListAsync();
        }

        public async Task UpdateContactAsync(Contact contact)
        {
            _context.Contacts.Update(contact);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ContactExists(int id)
        {
            return await _context.Contacts.AnyAsync(e => e.Id == id);
        }
    }
}
