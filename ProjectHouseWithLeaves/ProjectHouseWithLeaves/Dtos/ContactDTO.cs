using System.ComponentModel.DataAnnotations;

namespace ProjectHouseWithLeaves.Dtos
{
    public class ContactDTO
    {
        public int ContactId { get; set; }

        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string EmailContact { get; set; }

        [Required(ErrorMessage = "Nội dung phản hồi là bắt buộc")]
        public string DescriptionContact { get; set; }

        public string Status { get; set; } = "Pending";

        public DateTime? SendAt { get; set; } = DateTime.Now;
    }
}
