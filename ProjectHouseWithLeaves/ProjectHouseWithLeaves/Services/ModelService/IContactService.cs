using ProjectHouseWithLeaves.Models;

namespace ProjectHouseWithLeaves.Services.ModelService
{
    public interface IContactService
    {
        public Task CreateContact(Contact contact);
    }
}
