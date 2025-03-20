using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetPals_BackEnd_Group_9.Models
{
    public class Species
    {
        [Key]
        [Column("species_id")]
        public int SpeciesId { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Description { get; set; }
    }
}

