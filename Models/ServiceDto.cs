using MediatR;

namespace PetPals_BackEnd_Group_9.Models
{
    public class ServiceDto : IRequest<List<Service>>
    {
        public int ServiceId { get; set; }  // Added ServiceId
        public string ProviderName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
    }

}
