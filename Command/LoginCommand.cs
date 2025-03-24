using MediatR;
using PetPals_BackEnd_Group_9.Models;

namespace PetPals_BackEnd_Group_9.Command
{
    public record LoginCommand(string Email, string Password) : IRequest<LoginResponseDto>;
}
