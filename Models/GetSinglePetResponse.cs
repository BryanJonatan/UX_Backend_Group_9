namespace PetPals_BackEnd_Group_9.Models
{
    public class GetSinglePetResponse
    {
        public int PetId { get; set; }
        public string OwnerName { get; set; } = String.Empty;
        public string Name { get; set; } = string.Empty;
        public string Species { get; set; } = string.Empty;
        public string Breed { get; set; } = String.Empty;
        public int Age { get; set; }
        public string Description { get; set; } = String.Empty ;
        public string Status { get; set; } = String.Empty;
        public decimal Price { get; set; }
        public string CreatedAt { get; set; } = String.Empty;
        public string UpdatedAt { get; set; } = String.Empty;
    }
}
