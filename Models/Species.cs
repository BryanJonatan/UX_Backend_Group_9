using System.ComponentModel.DataAnnotations;

namespace PetPals_BackEnd_Group_9.Models
{
    public class Species
    {
        [Key]
        public int SpeciesId { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Description { get; set; }
    }
}

