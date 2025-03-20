using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetPals_BackEnd_Group_9.Models
{
    public record Pet
    {
        [Key]
        [Column("pet_id")]
        public int PetId { get; set; }

        [Required, Column("name")]
        public required string Name { get; set; }

        [Required, Column("breed")]
        public required string Breed { get; set; }

        [Column("age")]
        public int Age { get; set; }

        [Column("species_id")]
        public int SpeciesId { get; set; }

        [Required]
        public required Species Species { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [Required, Column("status")]
        public required string Status { get; set; }

        [Column("owner_id")]
        public int OwnerId { get; set; }

        [Required]
        public required User Owner { get; set; }
    }


}
