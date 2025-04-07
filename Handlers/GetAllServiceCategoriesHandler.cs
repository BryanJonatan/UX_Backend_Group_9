using MediatR;
using Microsoft.EntityFrameworkCore;
using PetPals_BackEnd_Group_9.Models;
using Serilog;
using System;

namespace PetPals_BackEnd_Group_9.Handlers
{
    public class GetAllServiceCategoriesHandler : IRequestHandler<GetAllServiceCategoriesQuery, List<ServiceCategoryDto>>
    {
        private readonly PetPalsDbContext _dbContext;

        public GetAllServiceCategoriesHandler(PetPalsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ServiceCategoryDto>> Handle(GetAllServiceCategoriesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var query = _dbContext.ServiceCategories.AsQueryable();

                if (request.CategoryId.HasValue)
                {
                    query = query.Where(sc => sc.Id == request.CategoryId);
                }

                if (!string.IsNullOrWhiteSpace(request.Name))
                {
                    query = query.Where(sc => sc.Name.Contains(request.Name));
                }

                var categories = await query
                    .Select(sc => new ServiceCategoryDto
                    {
                        Id = sc.Id,
                        Name = sc.Name
                    })
                    .ToListAsync(cancellationToken);

                Log.Information("Fetched {Count} service categories.", categories.Count);
                return categories;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error fetching service categories.");
                throw new Exception("Terjadi kesalahan pada sistem.");
            }
        }
    }
}
