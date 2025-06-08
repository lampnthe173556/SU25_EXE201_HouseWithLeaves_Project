
using Microsoft.Extensions.Options;
using ProjectHouseWithLeaves.Helper.Email;
using System.Net.Mail;
using System.Net;
using RazorLight;
using ProjectHouseWithLeaves.Models;

namespace ProjectHouseWithLeaves.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly RazorLightEngine _razorEngine;

        public EmailService(IOptions<EmailSettings> options)
        {
            _emailSettings = options.Value;
            _razorEngine = new RazorLightEngineBuilder()
                .UseFileSystemProject(Path.Combine(Directory.GetCurrentDirectory(), "Views", "EmailTemplates"))
                .UseMemoryCachingProvider()
                .Build();
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
    }
}
