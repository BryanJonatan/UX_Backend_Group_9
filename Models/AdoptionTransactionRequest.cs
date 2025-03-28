namespace PetPals_BackEnd_Group_9.Models
{
    public class AdoptionTransactionRequest
    {
        public int AdopterId { get; set; }
        public int OwnerId { get; set; }
        public int PetId { get; set; }
    }
}
