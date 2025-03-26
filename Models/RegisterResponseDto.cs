namespace PetPals_BackEnd_Group_9.Models
{
    public class RegisterResponseDto
    {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = "User";
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public int RoleId { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public string CreatedBy { get; set; } = "SYSTEM";
        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
        public string UpdatedBy { get; set; } = "SYSTEM";
    }
}
