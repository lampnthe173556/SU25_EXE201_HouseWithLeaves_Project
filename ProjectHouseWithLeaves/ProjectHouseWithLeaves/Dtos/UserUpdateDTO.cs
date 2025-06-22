using System.ComponentModel.DataAnnotations;

namespace ProjectHouseWithLeaves.Dtos
{
    public class UserUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        public string FullName { get; set; }

        public string? Phone { get; set; }

        public string? Gender { get; set; }

        public bool IsActive { get; set; }
    }
} 