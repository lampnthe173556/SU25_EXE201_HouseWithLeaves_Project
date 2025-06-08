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
    }
}
