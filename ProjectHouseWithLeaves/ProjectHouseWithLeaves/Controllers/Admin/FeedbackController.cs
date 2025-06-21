using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectHouseWithLeaves.Models;
using ProjectHouseWithLeaves.Services.ModelService;
using ProjectHouseWithLeaves.Helper.Authorization;

namespace ProjectHouseWithLeaves.Controllers.Admin
{
    [Area("Admin")]
    [AdminAuthorize]
    public class FeedbackController : Controller
    {
        private readonly IContactService _contactService;
        private readonly ILogger<FeedbackController> _logger;

        public FeedbackController(
           IContactService contactService,
           ILogger<FeedbackController> logger)
        {
            _contactService = contactService;
            _logger = logger;
        }

        public async Task<IActionResult> Feedback()
        {
            try
            {
                // Fetch feedback data from the service
                var feedbacks = await _contactService.GetAllContact();
                return View(feedbacks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching feedback data.");
                return View(new List<Contact>()); // Return an empty list in case of error
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetFeedback(int id)
        {
            try
            {
                // Fetch feedback by ID
                var feedback = await _contactService.GetFeedback(id);
                return Json(new {
                    emailContact = feedback.EmailContact,
                    descriptionContact = feedback.DescriptionContact,
                    sendAt = feedback.SendAt,
                    status = feedback.Status
                });
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Feedback with ID {Id} not found.", id);
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching feedback with ID {Id}.", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            try
            {
                await _contactService.UpdateStatus(id, status);
                string statusText = status == "Processed" ? "đã xử lý" : "đã từ chối";
                TempData["SuccessMessage"] = $"Đã cập nhật trạng thái phản hồi thành {statusText}.";
                return RedirectToAction("Feedback");
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Feedback with ID {Id} not found.", id);
                TempData["ErrorMessage"] = "Không tìm thấy phản hồi.";
                return RedirectToAction("Feedback");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating feedback status with ID {Id}.", id);
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi cập nhật trạng thái.";
                return RedirectToAction("Feedback");
            }
        }
    }
}
