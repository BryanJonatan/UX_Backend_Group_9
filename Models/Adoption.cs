using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetPals_BackEnd_Group_9.Models
{
    [Table("adoptions")]
    public class Adoption
    {
        [Key]
        [Column("adoption_id")]
        public int AdoptionId { get; set; }
        [Column("adopter_id")]
        public int AdopterId { get; set; }
        [Column("owner_id")]
        public int OwnerId { get; set; }
        [Column("pet_id")]
        public int PetId { get; set; }
        [Column("status")]
        public string Status { get; set; } 
        [Column("created_at")]
        public DateTimeOffset CreatedAt { get; set; }  // ✅ Correct
        [Column("created_by")]
        public string CreatedBy { get; set; } = string.Empty;
        [Column("updated_at")]
        public DateTimeOffset? UpdatedAt { get; set; }
        [Column("updated_by")]
        public string? UpdatedBy { get; set; }
        [Column("Price")]
        public decimal Price { get; set; }
        [Column("booking_date")]
        public DateTimeOffset BookingDate { get; set; }

        public virtual User Adopter { get; set; }
        public virtual User Owner { get; set; }
        public virtual Pet Pet { get; set; } 
    }
}

