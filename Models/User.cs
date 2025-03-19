using System.ComponentModel.DataAnnotations;
using System.Data;

namespace PetPals_BackEnd_Group_9.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required, MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required, MaxLength(255)]
        public string Password { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? Phone { get; set; }

        [MaxLength(255)]
        public string? Address { get; set; }

        [Required]
        public int RoleId { get; set; }
        public Role Role { get; set; } = null!;

        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public string CreatedBy { get; set; } = "SYSTEM";
        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
        public string UpdatedBy { get; set; } = "SYSTEM";
    }
}
