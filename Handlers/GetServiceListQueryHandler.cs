﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using PetPals_BackEnd_Group_9.Models;

namespace PetPals_BackEnd_Group_9.Handlers
{
    public class GetServiceListQueryHandler : IRequestHandler<GetServiceListQuery, List<ServiceDto>>
    {
        private readonly PetPalsDbContext _context;

        public GetServiceListQueryHandler(PetPalsDbContext context)
        {
            _context = context;
        }

        public async Task<List<ServiceDto>> Handle(GetServiceListQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Services
                .Include(s => s.Provider)
                .Include(s => s.Category)
                .AsQueryable();

            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(s => s.Name.ToLower().Contains(request.Name.ToLower()));
            }

            if (!string.IsNullOrEmpty(request.CategoryName))
            {
                query = query.Where(s => s.Category != null && s.Category.Name.ToLower().Contains(request.CategoryName.ToLower()));
            }

            if (request.MinPrice.HasValue)
            {
                query = query.Where(s => s.Price >= request.MinPrice.Value);
            }

            if (request.MaxPrice.HasValue)
            {
                query = query.Where(s => s.Price <= request.MaxPrice.Value);
            }

            if (!string.IsNullOrEmpty(request.City))
            {
                query = query.Where(s => s.City != null && s.City.ToLower().Contains(request.City.ToLower()));
            }

            return await query
    .Select(s => new ServiceDto
    {
        ServiceId = s.ServiceId,
        Name = s.Name,
        CategoryName = s.Category != null ? s.Category.Name : "Unknown",  // Prevent null reference
        ProviderName = s.Provider != null ? s.Provider.Name : "Unknown",  // Prevent null reference
        Description = s.Description ?? "No description",
        Price = s.Price,
        Address = s.Address ?? "No address",
        City = s.City ?? "No city"
    })
    .ToListAsync(cancellationToken);

        }
    }
}
