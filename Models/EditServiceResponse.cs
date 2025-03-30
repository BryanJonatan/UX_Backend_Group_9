namespace PetPals_BackEnd_Group_9.Models
{
    public class EditServiceResponse
    {
        public int ServiceId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public string Slug { get; set; }
    }

}
