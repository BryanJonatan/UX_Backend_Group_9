namespace PetPals_BackEnd_Group_9.Models
{
    public class GetSingleServiceResponse
    {
       public int ServiceId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public User Provider { get; set; }
        public ServiceCategory Category { get; set; }
    }
}
