﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using PetPals_BackEnd_Group_9.Command;
using PetPals_BackEnd_Group_9.Models;
using Serilog;

namespace PetPals_BackEnd_Group_9.Handlers
{
    public class AdoptionTransactionHandler : IRequestHandler<AdoptionTransactionCommand, AdoptionTransactionResponse>
    {
        private readonly PetPalsDbContext _context;
        private readonly ILogger<AdoptionTransactionHandler> _logger;

        public AdoptionTransactionHandler(PetPalsDbContext context, ILogger<AdoptionTransactionHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<AdoptionTransactionResponse> Handle(AdoptionTransactionCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Processing adoption request for PetId: {PetId}, AdopterId: {AdopterId}, OwnerId: {OwnerId}", command.PetId, command.AdopterId, command.OwnerId);

            var pet = await _context.Pets
                .FirstOrDefaultAsync(p => p.PetId == command.PetId && p.Status == "Available", cancellationToken);

            if (pet == null)
            {
                _logger.LogWarning("Pet with ID {PetId} is not available for adoption.", command.PetId);
                throw new CustomException2("Pet is either not found or already adopted.", "pet_not_available", 400);
            }


            var adoption = new Adoption
            {
                AdopterId = command.AdopterId,
                OwnerId = command.OwnerId,
                PetId = command.PetId,
                Status = "approved",
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = command.AdopterId.ToString(),
                Price = pet.Price,
                BookingDate = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow,
                UpdatedBy = command.AdopterId.ToString(),

            };

            pet.Status = "Adopted";
            pet.UpdatedAt = DateTimeOffset.UtcNow;
            pet.UpdatedBy = command.AdopterId.ToString();


            _context.Adoptions.Add(adoption);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Pet {PetId} successfully adopted by User {AdopterId}.", pet.PetId, command.AdopterId);

            return new AdoptionTransactionResponse
            {
                Success = true,
                Message = "Adoption successful.",
                AdoptionId = adoption.AdoptionId
            };
        }

    }
}