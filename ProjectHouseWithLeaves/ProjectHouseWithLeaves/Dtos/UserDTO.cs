using System.ComponentModel.DataAnnotations;

namespace ProjectHouseWithLeaves.Dtos
{
    public class UserDTO
    {
        public int Id { get; set; }
               
        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        public string? FullName { get; set; }

        public string? Phone { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string? Gender { get; set; }

        public List<string> Roles { get; set; } = new List<string>();

        public bool IsActive { get; set; } = true;

        public DateTime? CreatedAt { get; set; } = DateTime.Now;


        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
    }

    
} 