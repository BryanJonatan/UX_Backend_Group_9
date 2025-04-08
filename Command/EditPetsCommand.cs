using MediatR;
using PetPals_BackEnd_Group_9.Models;

namespace PetPals_BackEnd_Group_9.Command
{
    public record EditPetsCommand(int PetId, string Name, decimal Age, string Description, decimal Price) : IRequest<EditPetsResult>;
}