using Microsoft.EntityFrameworkCore;
using PetPals_BackEnd_Group_9.Models;
using Serilog;
using MediatR;

namespace PetPals_BackEnd_Group_9.Handlers
{
    public class GetProviderServicesHandler : IRequestHandler<GetProviderServicesQuery, List<GetProviderServicesResponse>>
    {
        private readonly PetPalsDbContext _context;

        public GetProviderServicesHandler(PetPalsDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetProviderServicesResponse>> Handle (GetProviderServicesQuery request, CancellationToken cancellationToken)
        {
            var providerServices = await _context.Services
                .Include(s => s.Provider)
                .Include(s => s.Category)
                .Where(s => s.ProviderId == request.providerId)
                .Select(s => new GetProviderServicesResponse
                {
                    ServiceId = s.ServiceId,
                    Name = s.Name,
                    Slug = s.Slug,
                    Description = s.Description,
                    Price = s.Price,
                    Address = s.Address,
                    City = s.City,
                    Provider = s.Provider,
                    Category = s.Category,
                }).ToListAsync(cancellationToken);

            if (providerServices == null)
            {
                Log.Information("Provider services not found: {ProviderId}", request.providerId);
                throw new NotFoundException($"Services from provider witj id '{request.providerId}' not found.");
            }

            return providerServices;
        }
    }
}
