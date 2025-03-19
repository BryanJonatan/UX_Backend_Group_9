namespace PetPals_BackEnd_Group_9.Models
{
    public class Service
    {
        public int ServiceId { get; set; }
        public int ProviderId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public object Category { get; internal set; }
        public object Provider { get; internal set; }
    }
}
