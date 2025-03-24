using MediatR;
using PetPals_BackEnd_Group_9.Models;

namespace PetPals_BackEnd_Group_9.Command
{
    public class RegisterCommand : IRequest<RegisterResponseDto>
    {
        public string Name { get; }
        public string Email { get; }
        public string Password { get; }
        public string? Phone { get; }
        public string? Address { get; }
        public int RoleId { get; }

        public RegisterCommand(RegisterRequestDto request)
        {
            Name = request.Name;
            Email = request.Email;
            Password = request.Password;
            Phone = request.Phone;
            Address = request.Address;
            RoleId = request.RoleId;
        }
    }

}
