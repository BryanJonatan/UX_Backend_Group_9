using MediatR;
using Microsoft.EntityFrameworkCore;
using PetPals_BackEnd_Group_9.Models;
using Serilog;

namespace PetPals_BackEnd_Group_9.Handlers
{
    public class GetSinglePetHandler : IRequestHandler<GetSinglePetQuery, GetSinglePetResponse>
    {
        private readonly PetPalsDbContext _context;

        public GetSinglePetHandler(PetPalsDbContext context)
        {
            _context = context;
        }

        public async Task<GetSinglePetResponse> Handle(GetSinglePetQuery request, CancellationToken cancellationToken)
        {
            var pet = await _context.Pets
                .Include(p => p.Species)
                .Include(p => p.Owner)
                .Where(p => p.Slug == request.Slug)
                .Select(p => new GetSinglePetResponse
                {
                    PetId = p.PetId,
                    OwnerName = p.Owner.Name,
                    OwnerPhone = p.Owner.Phone,
                    Name = p.Name,
                    Species = p.Species.Name,
                    Breed = p.Breed,
                    Age = p.Age,
                    Description = p.Description,
                    Status = p.Status,
                    Price = p.Price,
                    Owner = p.Owner
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (pet == null)
            {
                Log.Information("Pet not found: {Slug}", request.Slug);
                throw new NotFoundException($"Pet with slug '{request.Slug}' not found.");
            }

            return pet;
        }

    }
}
