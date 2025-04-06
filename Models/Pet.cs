using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace PetPals_BackEnd_Group_9.Models
{
    [Table("pets")]
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
        public decimal Age { get; set; }
        [Column("gender")]
        public string Gender { get; set; }

        [Column("species_id")]
        public int SpeciesId { get; set; }

        [Required]
        public required Species Species { get; set; }

        [Column("description")]
        public required string Description { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [Required, Column("status")]
        public required string Status { get; set; }
        [Column("is_removed")]
        public bool IsRemoved { get; set; } = false;

        [Column("owner_id")]
        public int OwnerId { get; set; }

        [Required]
        public required User Owner { get; set; }

        [Required, Column("slug")]
        public required string Slug { get; set; }
        [Column("image_url")]
        public string? ImageUrl { get; set; }

        [Column("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [Column("created_by")]
        public string CreatedBy { get; set; }

        [Column("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }

        [Column("updated_by")]
        public string UpdatedBy { get; set; }

    }


}
