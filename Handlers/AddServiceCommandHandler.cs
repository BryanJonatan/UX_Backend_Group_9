using MediatR;
using Microsoft.EntityFrameworkCore;
using PetPals_BackEnd_Group_9.Command;
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
            var providerExists = await _context.Users.AnyAsync(u => u.UserId == request.ProviderId, cancellationToken);
            if (!providerExists)
            {
                Log.Error("Provider with ID {ProviderId} not found", request.ProviderId);
                throw new NotFoundException("Provider not found");
            }

            var categoryExists = await _context.ServiceCategories.AnyAsync(c => c.CategoryId == request.CategoryId, cancellationToken);
            if (!categoryExists)
            {
                Log.Error("Category with ID {CategoryId} not found", request.CategoryId);
                throw new NotFoundException("Category not found");
            }

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
                CreatedBy = request.CreatedBy,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedBy = request.CreatedBy,
                UpdatedAt = DateTimeOffset.UtcNow,
                Slug = request.Name.ToLower().Replace(" ", "-")
            };

            _context.Services.Add(service);
            await _context.SaveChangesAsync(cancellationToken);

            Log.Information("Service {ServiceName} added successfully by {CreatedBy}", service.Name, request.CreatedBy);
            return service;
        }
    }
}
