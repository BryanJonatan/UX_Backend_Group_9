using MediatR;
using PetPals_BackEnd_Group_9.Command;
using PetPals_BackEnd_Group_9.Models;
using Serilog;
using System;

namespace PetPals_BackEnd_Group_9.Handlers
{
    public class AddPetCommandHandler : IRequestHandler<AddPetCommand, PetResponse>
    {
        private readonly PetPalsDbContext _context;

        public AddPetCommandHandler(PetPalsDbContext context)
        {
            _context = context;
        }

        public async Task<PetResponse> Handle(AddPetCommand request, CancellationToken cancellationToken)
        {
            Log.Information("Adding new pet: Name = {Name}, Breed = {Breed}, SpeciesId = {SpeciesId}", request.Name, request.Breed, request.SpeciesId);

            var owner = await _context.Users.FindAsync(new object[] { request.OwnerId }, cancellationToken);
            if (owner == null)
            {
                Log.Warning("Owner with ID {OwnerId} not found", request.OwnerId);
                throw new Exception($"Owner with ID {request.OwnerId} not found");
            }

            var species = await _context.Species.FindAsync(new object[] { request.SpeciesId }, cancellationToken);
            if (species == null)
            {
                Log.Warning("Species with ID {SpeciesId} not found", request.SpeciesId);
                throw new Exception($"Species with ID {request.SpeciesId} not found");
            }

            var pet = new Pet
            {
                Name = request.Name,
                Breed = request.Breed,
                Age = request.Age,
                SpeciesId = request.SpeciesId,
                Species = species,
                Description = request.Description,
                Price = request.Price,
                Status = "Available",
                OwnerId = request.OwnerId,
                Owner = owner,
                Slug = request.Name.ToLower().Replace(" ", "-"),
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = owner.Name,
                UpdatedAt = DateTimeOffset.UtcNow,
                UpdatedBy = owner.Name
            };

            await _context.Pets.AddAsync(pet, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            Log.Information("Pet added successfully with ID {PetId}", pet.PetId);

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
