namespace PetPals_BackEnd_Group_9.Models
{
    public class AdoptionTransactionRequest
    {
        public int PetId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Breed { get; set; } = string.Empty;
        public int UserId { get; set; }
        public DateTimeOffset? BookingDate { get; set; }
    }
}
