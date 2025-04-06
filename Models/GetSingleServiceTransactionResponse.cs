namespace PetPals_BackEnd_Group_9.Models
{
    public class GetSingleServiceTransactionResponse
    {
        public int TransactionId { get; set; }
        public string BookingDate { get; set; }
        public string Status { get; set; }
        public decimal Price { get; set; }
        public User Adopter { get; set; }
        public User Provider { get; set; }
        public Service Service { get; set; }
    }
}
