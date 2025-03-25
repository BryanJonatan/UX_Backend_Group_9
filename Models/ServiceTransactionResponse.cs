namespace PetPals_BackEnd_Group_9.Models
{
    public class ServiceTransactionResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public int TransactionId { get; set; }
        public string TransactionName;
        public DateTimeOffset BookingDate { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public string CreatedBy { get; set; } = "SYSTEM";
        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
        public string UpdatedBy { get; set; } = "SYSTEM";
    }
}
