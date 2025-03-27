namespace PetPals_BackEnd_Group_9.Models
{
    public class PetResponse
    {
        public int PetId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Breed { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Species { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Status { get; set; } = "Available";
        public string Owner { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
    }
}
