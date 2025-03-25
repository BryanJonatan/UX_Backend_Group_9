namespace PetPals_BackEnd_Group_9.Models
{
    public class AdoptionTransactionResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int? AdoptionId { get; set; }
    }
}
