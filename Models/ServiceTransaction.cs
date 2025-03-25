using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetPals_BackEnd_Group_9.Models
{
    public class ServiceTransaction
    {
        [Key]
        [Column("transaction_id")]
        public int TransactionId { get; set; }
        [Column("adopter_id")]
        public int AdopterId { get; set; }
        [Column("service_id")]
        public int ServiceId { get; set; }
        [Column("booking_date")]
        public DateTimeOffset BookingDate { get; set; }
        [Column("price")]
        public decimal Price { get; set; }
        [Column("status")]
        public string Status { get; set; } = "Pending";
        [Column("created_at")]
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        [Column("created_by")]
        public string? CreatedBy { get; set; }
        [Column("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
        [Column("updated_by")]
        public string? UpdatedBy { get; set; }


        public Service Service { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
