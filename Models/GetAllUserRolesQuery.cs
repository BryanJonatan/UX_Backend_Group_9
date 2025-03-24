using MediatR;

namespace PetPals_BackEnd_Group_9.Models
{
    public class GetAllUserRolesQuery : IRequest<List<UserRoleDto>>
    {
        public int? RoleId { get; set; }
        public string? Name { get; set; }
    }

}
