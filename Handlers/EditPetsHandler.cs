using MediatR;
using PetPals_BackEnd_Group_9.Command;
using PetPals_BackEnd_Group_9.Models;
using System.Net;
using System;
using PetPals_BackEnd_Group_9;
using Microsoft.EntityFrameworkCore;

public class EditPetsHandler : IRequestHandler<EditPetsCommand, EditPetsResult>
{
    private readonly PetPalsDbContext _context;
    private readonly ILogger<EditPetsHandler> _logger;

    public EditPetsHandler(PetPalsDbContext context, ILogger<EditPetsHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<EditPetsResult> Handle(EditPetsCommand request, CancellationToken cancellationToken)
    {
        var pet = await _context.Pets.FirstOrDefaultAsync(x => x.PetId == request.PetId, cancellationToken);
        if (pet == null)
        {
            _logger.LogWarning("Pet {PetId} not found", request.PetId);
            return EditPetsResult.Failure(HttpStatusCode.NotFound, "Pet not found", "The requested pet ID does not exist in the database.");
        }

        pet.Name = request.Name;
        pet.Breed = request.Breed;
        pet.Age = request.Age;
        pet.Description = request.Description;
        pet.Price = request.Price;
        pet.UpdatedAt = DateTimeOffset.UtcNow;
        pet.UpdatedBy = "System"; // Replace with actual user context

        await _context.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Pet {PetId} updated successfully", pet.PetId);

        return EditPetsResult.Success();
    }
}