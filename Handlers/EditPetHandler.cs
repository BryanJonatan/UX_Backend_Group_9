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
        private readonly PetPalsDbContext _context;

        public EditPetHandler(PetPalsDbContext context)
        {
            _context = context;
        }

        public async Task<PetResponse> Handle(int petId, EditPetCommand request, CancellationToken cancellationToken)
        {
            Log.Information("Edit Pet: New Name = {Name}", request.Name);

            var pet = await _context.Pets.FirstOrDefaultAsync(p => p.PetId == petId, cancellationToken);
            if (pet == null)
            {
                Log.Warning("Pet with ID {PetId} not found", petId);
                throw new KeyNotFoundException($"Pet with ID {petId} not found");
            }

            var species = await _context.Species.AsNoTracking()
                .FirstOrDefaultAsync(s => s.SpeciesId == request.SpeciesId, cancellationToken);
            if (species == null)
            {
                Log.Warning("Species with ID {SpeciesId} not found", request.SpeciesId);
                throw new KeyNotFoundException($"Species with ID {request.SpeciesId} not found");
            }

            pet.Name = request.Name;
            pet.Slug = request.Name.ToLower().Replace(" ", "-");
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
                Owner = pet.Owner.Name,
                Slug = pet.Slug
            };
        }

    }
}
