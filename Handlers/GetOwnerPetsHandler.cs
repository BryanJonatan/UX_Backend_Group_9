using Microsoft.EntityFrameworkCore;
using PetPals_BackEnd_Group_9.Models;
using Serilog;
using MediatR;

namespace PetPals_BackEnd_Group_9.Handlers
{
    public class GetOwnerPetsHandler : IRequestHandler<GetOwnerPetsQuery, List<GetOwnerPetsResponse>>
    {
        private readonly PetPalsDbContext _context;

        public GetOwnerPetsHandler(PetPalsDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetOwnerPetsResponse>> Handle (GetOwnerPetsQuery request, CancellationToken cancellationToken)
        {
            var ownerPets = await _context.Pets
                .Include(p => p.Owner)
                .Include(p => p.Species)
                .Where(p => p.OwnerId == request.ownerId && !p.IsRemoved)
                .Select(p => new GetOwnerPetsResponse
                {
                    PetId = p.PetId,
                    Name = p.Name,
                    Slug = p.Slug,
                    Breed = p.Breed,
                    Age = p.Age,
                    Gender = p.Gender,
                    Description = p.Description,
                    Status = p.Status,
                    Price = p.Price,
                    Owner = p.Owner,
                    Species = p.Species,
                })
                .ToListAsync(cancellationToken);

            if (ownerPets == null)
            {
                Log.Information("Owner pets not found: {OwnerId}", request.ownerId);
                throw new NotFoundException($"Pets from owner with id '{request.ownerId}' not found.");
            }

            return ownerPets;
        }
    }
}
