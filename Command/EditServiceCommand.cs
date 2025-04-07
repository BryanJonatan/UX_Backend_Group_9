using MediatR;
using PetPals_BackEnd_Group_9.Models;

namespace PetPals_BackEnd_Group_9.Command
{
    public record EditServiceCommand(int ServiceId, string Name, int CategoryId, string Description, decimal Price, string Address, string City) : IRequest<EditServiceResult>;
}
