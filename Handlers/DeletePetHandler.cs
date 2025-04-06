using MediatR;
using PetPals_BackEnd_Group_9.Command;
using PetPals_BackEnd_Group_9.Models;
using System.Net;
using System;
using PetPals_BackEnd_Group_9;
using Microsoft.EntityFrameworkCore;

namespace PetPals_BackEnd_Group_9.Handlers
{
    public class DeletePetHandler : IRequestHandler<DeletePetQuery, DeletePetResult>
    {
        private readonly PetPalsDbContext _context;
        private readonly ILogger<DeletePetHandler> _logger;

        public DeletePetHandler(PetPalsDbContext context, ILogger<DeletePetHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<DeletePetResult> Handle(DeletePetQuery request, CancellationToken cancellationToken)
        {
            var pet = await _context.Pets.FirstOrDefaultAsync(p => p.PetId == request.PetId, cancellationToken);

            if (pet == null)
            {
                _logger.LogWarning("Pet {PetId} not found", request.PetId);
                return DeletePetResult.Failure(HttpStatusCode.NotFound, "Pet not found", "The requested pet ID does not exist in the database.");
            }

            pet.IsRemoved = true;

            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Pet {PetId} has been removed", pet.PetId);

            return DeletePetResult.Success();
        }

    }
}
