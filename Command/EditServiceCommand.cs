using MediatR;
using PetPals_BackEnd_Group_9.Models;

namespace PetPals_BackEnd_Group_9.Command
{
    public class EditServiceCommand : IRequest<EditServiceResponse>
    {
        public int ServiceId { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
    }


}
