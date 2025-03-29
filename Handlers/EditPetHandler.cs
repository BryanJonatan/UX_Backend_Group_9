using MediatR;
using Microsoft.EntityFrameworkCore;
using PetPals_BackEnd_Group_9.Command;
using PetPals_BackEnd_Group_9.Models;
using Serilog;
using System;

namespace PetPals_BackEnd_Group_9.Handlers
{
    public class EditPetHandler : IRequestHandler<EditPetCommand, PetResponse>
    {
        private readonly PetPalsDbContext _context

        public EditPetHandler(PetPalsDbContext context)
        {
            _context = context;
        }

        public async Task<PetResponse> Handle(EditPetCommand request, CancellationToken cancellationToken)
        {
            Log.Information("Edit Pet: New Name = {Name}", request.Name);

            var owner = await _context.Users.AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == request.OwnerId, cancellationToken);
            if (owner == null)
            {
                Log.Warning("Owner with ID {OwnerId} not found", request.OwnerId);
                throw new KeyNotFoundException($"Owner with ID {request.OwnerId} not found");
            }

            var pet = await _context.Pets.FirstOrDefaultAsync(p => p.PetId == request.PetId, cancellationToken);
            if (pet == null)
            {
                Log.Warning("Pet with ID {PetId} not found", request.PetId);
                throw new KeyNotFoundException($"Pet with ID {request.PetId} not found");
            }

            pet.Name = request.Name;
            pet.Slug = request.Name.ToLower().Replace(" ", "-"),
            pet.Breed = request.Breed;
            pet.Age = request.Age;
            pet.SpeciesId = request.SpeciesId;
            pet.Description = request.Description;
            pet.Price = request.Price;

            if (_context.ChangeTracker.HasChanges())
            {
                await _context.SaveChangesAsync(cancellationToken);
            }

            Log.Information("Pet updated successfully with ID {PetId}", pet.PetId);

            return new PetResponse
            {
                PetId = pet.PetId,
                Name = pet.Name,
                Breed = pet.Breed,
                Age = pet.Age,
                Species = species.Name,
                Description = pet.Description,
                Price = pet.Price,
                Status = pet.Status,
                Owner = owner.Name,
                Slug = pet.Slug
            };
        }

    }
}
