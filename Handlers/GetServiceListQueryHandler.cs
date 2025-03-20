using MediatR;
using Microsoft.EntityFrameworkCore;
using PetPals_BackEnd_Group_9.Models;
using Serilog;

namespace PetPals_BackEnd_Group_9.Handlers
{
    public class GetServiceListQueryHandler : IRequestHandler<GetServiceListQuery, List<ServiceDto>>
    {
        private readonly PetPalsDbContext _dbContext;

        public GetServiceListQueryHandler(PetPalsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ServiceDto>> Handle(GetServiceListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Information("Fetching services list with filters: {@Request}", request);

                var query = _dbContext.Services
                    .Include(s => s.Provider)
                    .Include(s => s.Category)
                    .Where(s => string.IsNullOrEmpty(request.Name) || s.Name.Contains(request.Name))
                    .Select(s => new ServiceDto
                    {
                        ProviderName = s.Provider != null ? s.Provider.Name : "Unknown",
                        Name = s.Name,
                        CategoryName = s.Category != null ? s.Category.Name : "Unknown",
                        Description = s.Description ?? "No description",
                        Price = s.Price,
                        Address = s.Address ?? "No address",
                        City = s.City ?? "No city"
                    });

                Log.Information("Executing query: {Query}", query.ToQueryString()); // Debug SQL query

                var result = await query.ToListAsync(cancellationToken);
                Log.Information("Fetched {Count} services", result.Count);

                return result;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in GetServiceListHandler");
                throw;
            }
        }


    }
}
