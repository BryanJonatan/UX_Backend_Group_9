using MediatR;
using Microsoft.EntityFrameworkCore;
using PetPals_BackEnd_Group_9.Models;

namespace PetPals_BackEnd_Group_9.Handlers
{
    public class GetAllUserRolesHandler : IRequestHandler<GetAllUserRolesQuery, List<UserRoleDto>>
    {
        private readonly PetPalsDbContext _context;
        private readonly ILogger<GetAllUserRolesHandler> _logger;

        public GetAllUserRolesHandler(PetPalsDbContext context, ILogger<GetAllUserRolesHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<UserRoleDto>> Handle(GetAllUserRolesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching user roles with filters: RoleId={RoleId}, Name={Name}", request.RoleId, request.Name);

            var query = _context.Roles.AsQueryable();

            if (request.RoleId.HasValue)
            {
                query = query.Where(r => r.Id == request.RoleId);
            }

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                query = query.Where(r => r.Name.Contains(request.Name));
            }

            var roles = await query
                .Select(r => new UserRoleDto
                {
                    Id = r.Id,
                    Name = r.Name
                })
                .ToListAsync(cancellationToken);

            return roles;
        }
    }

}
