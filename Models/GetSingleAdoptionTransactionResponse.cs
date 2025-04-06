namespace PetPals_BackEnd_Group_9.Models
{
    public class GetSingleAdoptionTransactionResponse
    {
        public int AdoptionId { get; set; }
        public string BookingDate { get; set; }
        public string Status { get; set; }
        public decimal Price { get; set; }
        public User Adopter { get; set; }
        public User Owner { get; set; }
        public Pet Pet { get; set; }
    }
}
