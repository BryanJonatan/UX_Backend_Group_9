using MediatR;
using Microsoft.EntityFrameworkCore;
using PetPals_BackEnd_Group_9.Models;

namespace PetPals_BackEnd_Group_9.Handlers
{
    public class SearchAdoptionListHandler : IRequestHandler<SearchAdoptionListQuery, List<AdoptionListDto>>
    {
        private readonly PetPalsDbContext _context;

        public SearchAdoptionListHandler(PetPalsDbContext context)
        {
            _context = context;
        }

        public async Task<List<AdoptionListDto>> Handle(SearchAdoptionListQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Pets
                .Include(p => p.Species)
                .Where(p => p.Status == "available");

            if (!string.IsNullOrEmpty(request.Name) || !string.IsNullOrEmpty(request.Breed))
            {
                query = query.Where(p => 
                    (!string.IsNullOrEmpty(request.Name) && p.Name.ToLower().Contains(request.Name.ToLower())) ||
                    (!string.IsNullOrEmpty(request.Breed) && p.Breed.ToLower().Contains(request.Breed.ToLower()))
                );
            }

            if (request.MinAge.HasValue)
            {
                query = query.Where(p => p.Age >= request.MinAge.Value);
            }

            if (request.MaxAge.HasValue)
            {
                query = query.Where(p => p.Age <= request.MaxAge.Value);
            }

            if (!string.IsNullOrEmpty(request.Species))
            {
                query = query.Where(p => p.Species.Name.ToLower().Contains(request.Species.ToLower()));
            }

            if (request.MinPrice.HasValue)
            {
                query = query.Where(p => p.Price >= request.MinPrice.Value);
            }

            if (request.MaxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= request.MaxPrice.Value);
            }

            return await query
                .Include(p => p.Species)
                .Select(p => new AdoptionListDto
                {
                    PetId = p.PetId,
                    Name = p.Name,
                    Slug = p.Slug,
                    Breed = p.Breed,
                    Age = p.Age,
                    Gender = p.Gender,
                    Price = p.Price,
                    Status = p.Status,
                    Species = p.Species,
                })
                .ToListAsync(cancellationToken);
        }
    }
}
