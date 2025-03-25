namespace PetPals_BackEnd_Group_9.Models
{
    public class ServiceTransactionResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public int TransactionId { get; set; }
    }
}
