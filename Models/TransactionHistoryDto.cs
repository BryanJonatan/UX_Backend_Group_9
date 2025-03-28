namespace PetPals_BackEnd_Group_9.Models
{
    public class TransactionHistoryDto
    {
        public string TransactionType { get; set; }
        public DateTimeOffset BookingDate { get; set; }
        public decimal? Price { get; set; }
        public User User { get; set; }
        public object Item { get; set; }
    }
}
