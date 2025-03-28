namespace PetPals_BackEnd_Group_9.Models
{
    public class AdoptionTransactionRequestDto
    {
        public int TransactionId { get; set; }
        public string TransactionType { get; set; }
        public decimal? Price { get; set; }
        public DateTimeOffset BookingDate { get; set; }
        public User Adopter { get; set; }
        public Pet Pet { get; set; }
    }
}
