namespace ProjectHouseWithLeaves.Dtos
{
    public class UserRegisterDtos
    {
        public string Email { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public string? FullName { get; set; }

        public string? Phone { get; set; }

        public DateOnly? DateOfBirth { get; set; }
    }
}
