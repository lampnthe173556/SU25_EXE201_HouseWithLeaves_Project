using Microsoft.EntityFrameworkCore;
using ProjectHouseWithLeaves.Models;

namespace ProjectHouseWithLeaves.Services.ModelService
{
    public class ContactService : IContactService
    {
        private readonly MiniPlantStoreContext _context;

        public ContactService(MiniPlantStoreContext context)
        {
            _context = context;
        }
        public async Task CreateContact(Contact contact)
        {
            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Contact>> GetAllContact()
        {
            return await _context.Contacts.ToListAsync();
        }
        public async Task<Contact> GetFeedback(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                throw new KeyNotFoundException($"Contact with ID {id} not found");
            }
            return contact;
        }
        public async Task UpdateStatus(int id, string status)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                throw new KeyNotFoundException($"Contact with ID {id} not found");
            }
            
            contact.Status = status;
            await _context.SaveChangesAsync();
        }
    }
}
