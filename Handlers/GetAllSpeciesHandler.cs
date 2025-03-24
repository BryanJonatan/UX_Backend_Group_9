using MediatR;
using PetPals_BackEnd_Group_9.Models;
using Serilog;
using System.Net;
using System;
using Microsoft.EntityFrameworkCore;

namespace PetPals_BackEnd_Group_9.Handlers
{
    public class GetAllSpeciesHandler : IRequestHandler<GetAllSpeciesQuery, List<SpeciesDto>>
    {
        private readonly PetPalsDbContext _dbContext;

        public GetAllSpeciesHandler(PetPalsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<SpeciesDto>> Handle(GetAllSpeciesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var query = _dbContext.Species.AsQueryable();

                if (request.SpeciesId.HasValue)
                {
                    query = query.Where(s => s.SpeciesId == request.SpeciesId);
                }

                if (!string.IsNullOrWhiteSpace(request.Name))
                {
                    query = query.Where(s => s.Name.Contains(request.Name));
                }

                var speciesList = await query
                    .Select(s => new SpeciesDto
                    {
                        SpeciesId = s.SpeciesId,
                        Name = s.Name,
                        Description = s.Description,
                        CreatedAt = s.CreatedAt
                    })
                    .ToListAsync(cancellationToken);

                Log.Information("Fetched {Count} species.", speciesList.Count);
                return speciesList;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error fetching species.");
                throw new CustomException("Terjadi kesalahan pada sistem.", HttpStatusCode.InternalServerError);
            }
        }
    }
}
