using MediatR;
using Microsoft.EntityFrameworkCore;
using PetPals_BackEnd_Group_9.Models;
using Serilog;

namespace PetPals_BackEnd_Group_9.Handlers
{
    public class GetSingleServiceHandler : IRequestHandler<GetSingleServiceQuery, GetSingleServiceResponse>
    {
        private readonly PetPalsDbContext _context;

        public GetSingleServiceHandler(PetPalsDbContext context)
        {
            _context = context;
        }

        public async Task<GetSingleServiceResponse> Handle(GetSingleServiceQuery request, CancellationToken cancellationToken)
        {
            var service = await _context.Services
    .Include(s => s.Category)
    .Include(s => s.Provider)
    .Where(s => s.Slug == request.Slug)
    .Select(s => new GetSingleServiceResponse
    {
        ProviderName = s.Provider.Name,
        Name  = s.Name,
         CategoryName = s.Category.Name,
         Description = s.Description,
         Price = s.Price,
         Address = s.Address,
         City  = s.City
    })
    .FirstOrDefaultAsync(cancellationToken);

            if (service == null)
            {
                Log.Information("Service not found: {Slug}", request.Slug);
                throw new NotFoundException($"Service with slug '{request.Slug}' not found.");
            }

            return service;
        }
    }
}
