using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetPals_BackEnd_Group_9.Command;
using PetPals_BackEnd_Group_9.Helpers;
using PetPals_BackEnd_Group_9.Validators;
using Serilog;

namespace PetPals_BackEnd_Group_9.Handlers
{
    public class AddServiceCommandHandler : IRequestHandler<AddServiceCommand, Service>
    {
        private readonly PetPalsDbContext _context;

        public AddServiceCommandHandler(PetPalsDbContext context)
        {
            _context = context;
        }

        public async Task<Service> Handle(AddServiceCommand request, CancellationToken cancellationToken)
        {
            Log.Information("Adding new service: Name = {Name}, CategoryId = {CategoryId}", request.Name, request.CategoryId);

            var providerExists = await _context.Users.AnyAsync(u => u.UserId == request.ProviderId, cancellationToken);
            if (!providerExists)
            {
                Log.Error("Provider with ID {ProviderId} not found", request.ProviderId);
                throw new NotFoundException("Provider not found");
            }

            var categoryExists = await _context.ServiceCategories.AnyAsync(c => c.Id == request.CategoryId, cancellationToken);
            if (!categoryExists)
            {
                Log.Error("Category with ID {CategoryId} not found", request.CategoryId);
                throw new NotFoundException("Category not found");
            }

            var provider = await _context.Users.FirstOrDefaultAsync(u => u.UserId == request.ProviderId, cancellationToken);

            var service = new Service
            {
                ProviderId = request.ProviderId,
                Name = request.Name,
                CategoryId = request.CategoryId,
                Description = request.Description,
                Price = request.Price,
                Address = request.Address,
                City = request.City,
                Status = "available",
                IsRemoved = false,
                CreatedBy = provider?.Name,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedBy = provider?.Name,
                UpdatedAt = DateTimeOffset.UtcNow,
                Slug = await SlugHelper.GenerateUniqueSlugAsync(request.Name, _context.Services),
            };

            await _context.Services.AddAsync(service, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            Log.Information("Service {ServiceName} added successfully by {ProviderName}", service.Name, provider?.Name);
            return service;
        }
    }
}

