namespace PetPals_BackEnd_Group_9.Models
{
    public class GetOwnerPetsResponse
    {
        public int PetId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Breed { get; set; } = String.Empty;
        public decimal Age { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string Description { get; set; } = String.Empty;
        public string Status { get; set; } = String.Empty;
        public decimal Price { get; set; }
        public string CreatedAt { get; set; } = String.Empty;
        public string UpdatedAt { get; set; } = String.Empty;
        public User Owner { get; set; }
        public Species Species { get; set; }
    }
}
