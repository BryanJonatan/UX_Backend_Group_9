using MediatR;
using Microsoft.EntityFrameworkCore;
using PetPals_BackEnd_Group_9.Models;

namespace PetPals_BackEnd_Group_9.Handlers
{
    public class GetProviderServiceHandler : IRequestHandler<GetProviderServiceQuery, List<ServiceResponse>>
    {
        private readonly PetPalsDbContext _context;
        private readonly ILogger<GetProviderServiceHandler> _logger;

        public GetProviderServiceHandler(PetPalsDbContext context, ILogger<GetProviderServiceHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<ServiceResponse>> Handle(GetProviderServiceQuery request, CancellationToken cancellationToken)
        {
            var services = await _context.Services
                .Where(s => s.ProviderId == request.ProviderId)
                .Select(s => new ServiceResponse
                {
                    ServiceId = s.ServiceId,
                    Name = s.Name,
                    Slug = s.Slug,
                    Category = s.Category.Name,
                    Price = s.Price,
                    Address = s.Address,
                    City = s.City,
                    CreatedAt = s.CreatedAt
                })
                .ToListAsync(cancellationToken);

            _logger.LogInformation("Retrieved {Count} services for ProviderId {ProviderId}", services.Count, request.ProviderId);
            return services;
        }
    }
}
