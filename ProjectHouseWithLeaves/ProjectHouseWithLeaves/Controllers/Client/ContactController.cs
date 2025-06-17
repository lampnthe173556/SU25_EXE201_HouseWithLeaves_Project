using Microsoft.AspNetCore.Mvc;
using ProjectHouseWithLeaves.Models;
using ProjectHouseWithLeaves.Services.EmailService;
using ProjectHouseWithLeaves.Services.ModelService;
using System.Threading.Tasks;

namespace ProjectHouseWithLeaves.Controllers.Client
{
    public class ContactController : Controller
    {
        private readonly IEmailService _emailService;
        private readonly IContactService _contactService;

        public ContactController(IEmailService emailService, IContactService contactService)
        {
            _emailService = emailService;
            _contactService = contactService;
        }
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Contact(Contact contact)
        {
            contact.SendAt = DateTime.Now;
            contact.Status = "CHƯA XEM";
            if (ModelState.IsValid)
            {
                //set default value
                await _emailService.SendEmailAsync(
                    toEmail: "pntlam12g03@gmail.com",
                    subject: "📬 YÊU CẦU LIÊN HỆ TỪ KHÁCH HÀNG",
                    model: contact
                );
                await _contactService.CreateContact(contact);
                ViewBag.Message = "✅ Gửi liên hệ thành công!";
            }

            return View();
        }
    }
}
