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
            return await _context.Pets
       .Where(p => p.Name.Contains(request.Name) && p.Status == "available")
       .Include(p => p.Species)
       .Select(p => new AdoptionListDto
       {
           PetId = p.PetId,
           Name = p.Name,
           Breed = p.Breed,
           Age = p.Age,
           Species = p.Species.Name ?? "Unknown", // ✅ Handle nulls safely
           Price = p.Price,
           Status = p.Status
       })
       .ToListAsync(cancellationToken);
        }
    }

}
