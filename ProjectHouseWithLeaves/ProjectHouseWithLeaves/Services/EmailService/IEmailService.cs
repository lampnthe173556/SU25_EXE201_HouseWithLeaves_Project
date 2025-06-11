using ProjectHouseWithLeaves.Models;

namespace ProjectHouseWithLeaves.Services.EmailService
{
    public interface IEmailService
    {
        //email feedback
        public Task SendEmailAsync(string toEmail, string subject, Contact model);
        //email sendOtp
        public Task SendOtpByEmailAsync(string toEmail);
        public Task<string?> GenerateOtp();
    }
}
