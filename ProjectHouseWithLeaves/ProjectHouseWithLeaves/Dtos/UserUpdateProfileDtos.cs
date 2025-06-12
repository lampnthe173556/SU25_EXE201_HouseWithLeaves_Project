namespace ProjectHouseWithLeaves.Dtos
{
    public class UserUpdateProfileDtos
    {
        public string Email { get; set; } = null!;
        public string? FullName { get; set; }

        public string? Phone { get; set; }

        public DateOnly? DateOfBirth { get; set; }

        public string? Gender { get; set; }
    }
}
