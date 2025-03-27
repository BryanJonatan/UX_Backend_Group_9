using MediatR;

namespace PetPals_BackEnd_Group_9.Command
{
    public class AddServiceCommand : IRequest<Service>
    {
        public int ProviderId { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string CreatedBy { get; set; }
    }
}
