using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetPals_BackEnd_Group_9.Command;
using PetPals_BackEnd_Group_9.Helpers;
using PetPals_BackEnd_Group_9.Models;
using PetPals_BackEnd_Group_9.Validators;
using Serilog;

namespace PetPals_BackEnd_Group_9.Handlers
{
    public class AddPetCommandHandler : IRequestHandler<AddPetCommand, Pet>
    {
        private readonly PetPalsDbContext _context;

        public AddPetCommandHandler(PetPalsDbContext context)
        {
            _context = context;
        }

        public async Task<Pet> Handle(AddPetCommand request, CancellationToken cancellationToken)
        {
            Log.Information("Adding new pet: Name = {Name}, Breed = {Breed}, SpeciesId = {SpeciesId}", request.Name, request.Breed, request.SpeciesId);

            var ownerExists = await _context.Users.AnyAsync(u => u.UserId == request.OwnerId, cancellationToken);
            if (!ownerExists)
            {
                Log.Warning("Owner with ID {OwnerId} not found", request.OwnerId);
                throw new Exception($"Owner with ID {request.OwnerId} not found");
            }

            var speciesExists = await _context.Species.AnyAsync(s => s.Id == request.SpeciesId, cancellationToken);
            if (!speciesExists)
            {
                Log.Warning("Species with ID {SpeciesId} not found", request.SpeciesId);
                throw new Exception($"Species with ID {request.SpeciesId} not found");
            }

            var owner = await _context.Users.FirstOrDefaultAsync(u => u.UserId == request.OwnerId);
            var species = await _context.Species.FirstOrDefaultAsync(s => s.Id == request.SpeciesId);

            var pet = new Pet
            {
                Name = request.Name,
                Breed = request.Breed,
                Age = request.Age,
                Gender = request.Gender,
                SpeciesId = request.SpeciesId,
                Species = species,
                Description = request.Description,
                Price = request.Price,
                Status = "available",
                IsRemoved = false,
                OwnerId = request.OwnerId,
                Owner = owner,
                Slug = await SlugHelper.GenerateUniqueSlugAsync(request.Name, _context.Pets),
                ImageUrl = request.ImageUrl,
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = owner?.Name,
                UpdatedAt = DateTimeOffset.UtcNow,
                UpdatedBy = owner?.Name
            };

            await _context.Pets.AddAsync(pet, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            Log.Information("Pet {PetName} added successfully by {CreatedBy}", pet.Name, pet.CreatedBy);
            return pet;
        }
    }
}
