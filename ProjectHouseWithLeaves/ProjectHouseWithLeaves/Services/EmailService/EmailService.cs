using Microsoft.Extensions.Options;
using ProjectHouseWithLeaves.Helper.Email;
using System.Net.Mail;
using System.Net;
using RazorLight;
using ProjectHouseWithLeaves.Models;
using ProjectHouseWithLeaves.Dtos;
using ProjectHouseWithLeaves.Helper.Session;

namespace ProjectHouseWithLeaves.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly RazorLightEngine _razorEngine;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EmailService(IOptions<EmailSettings> options, IHttpContextAccessor httpContextAccessor)
        {
            _emailSettings = options.Value;
            _razorEngine = new RazorLightEngineBuilder()
                .UseFileSystemProject(Path.Combine(Directory.GetCurrentDirectory(), "Views", "EmailTemplates"))
                .UseMemoryCachingProvider()
                .Build();
            _httpContextAccessor = httpContextAccessor;
        }

        
        public async Task SendEmailAsync(string toEmail, string subject, Contact model)
        {
            string htmlBody = await _razorEngine.CompileRenderAsync("ContactTemplate.cshtml", model);

            var message = new MailMessage();
            message.From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName);
            message.To.Add(toEmail);
            message.Subject = subject;
            message.Body = htmlBody;
            message.IsBodyHtml = true;

            using var smtp = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort)
            {
                Credentials = new NetworkCredential(_emailSettings.SenderEmail, _emailSettings.SenderPassword),
                EnableSsl = true
            };

            await smtp.SendMailAsync(message);
        }

        public async Task SendOtpByEmailAsync(string toEmail)
        {
            var otp = await GenerateOtp();
            OtpDataDtos otpDataDtos = new OtpDataDtos() { 
               Code = otp,
               Expiration = DateTime.Now.AddMinutes(3)
            };
            _httpContextAccessor.HttpContext.Session.SetObject("Otp", otpDataDtos);
            string htmlBody = await _razorEngine.CompileRenderAsync("OtpTemplate.cshtml", otp);

            var message = new MailMessage();
            message.From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName);
            message.To.Add(toEmail);
            message.Subject = "Mã xác thực OTP - Đặt lại mật khẩu";
            message.Body = htmlBody;
            message.IsBodyHtml = true;

            using var smtp = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort)
            {
                Credentials = new NetworkCredential(_emailSettings.SenderEmail, _emailSettings.SenderPassword),
                EnableSsl = true
            };

            await smtp.SendMailAsync(message);
        }

        
        public Task<string?> GenerateOtp()
        {
            Random random = new Random();
            string otp = "";
            for (int i = 0; i < 6; i++)
            {
                otp += random.Next(0, 10).ToString();
            }
            return Task.FromResult<string?>(otp);
        }

    }
}
