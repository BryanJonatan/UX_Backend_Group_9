using MediatR;

namespace PetPals_BackEnd_Group_9.Command
{
    public class AddServiceCommand : IRequest<Service>
    {
        public int ProviderId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; } = decimal.Zero;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
    }
}
