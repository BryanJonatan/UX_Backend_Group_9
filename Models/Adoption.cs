using System.ComponentModel.DataAnnotations;

namespace PetPals_BackEnd_Group_9.Models
{
    public class Adoption
    {
        [Key]
        public int AdoptionId { get; set; }

        [Required]
        public int AdopterId { get; set; }
        public User Adopter { get; set; } = null!;

        [Required]
        public int PetId { get; set; }
        public Pet Pet { get; set; } = null!;

        public DateTimeOffset AdoptionDate { get; set; } = DateTimeOffset.UtcNow;

        [Required, MaxLength(20)]
        public string Status { get; set; } = "pending";
    }
}
