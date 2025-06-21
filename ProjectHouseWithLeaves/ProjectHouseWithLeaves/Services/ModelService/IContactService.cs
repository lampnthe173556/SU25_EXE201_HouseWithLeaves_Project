using ProjectHouseWithLeaves.Models;

namespace ProjectHouseWithLeaves.Services.ModelService
{
    public interface IContactService
    {
        public Task CreateContact(Contact contact);
        public Task<List<Contact>> GetAllContact();
        public Task<Contact> GetFeedback(int id);
        public Task UpdateStatus(int id, string status);
    }
}
