using ProjectHouseWithLeaves.Models;

namespace ProjectHouseWithLeaves.Services.EmailService
{
    public interface IEmailService
    {
        public Task SendEmailAsync(string toEmail, string subject, Contact model);
    }
}
