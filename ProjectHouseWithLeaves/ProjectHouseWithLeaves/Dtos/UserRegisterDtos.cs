namespace ProjectHouseWithLeaves.Dtos
{
    public class UserRegisterDtos
    {
        public string Email { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public string? FullName { get; set; }

        public string? Phone { get; set; }

        public DateOnly? DateOfBirth { get; set; }
        //set default value
        public string? Gender { get; set; } = "MALE";
        public int? RoleId { get; set; } = 1;
        public int? Status { get; set; } = 1;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
    }
}
