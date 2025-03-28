using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetPals_BackEnd_Group_9.Models;
using System.Net;

namespace PetPals_BackEnd_Group_9.Handlers
{
    public class AdoptionTransactionRequestHandler : IRequestHandler<AdoptionTransactionRequestQuery, List<AdoptionTransactionRequestDto>>
    {
        private readonly PetPalsDbContext _dbContext;
        private readonly ILogger<TransactionHistoryHandler> _logger;

        public AdoptionTransactionRequestHandler(PetPalsDbContext dbContext, ILogger<TransactionHistoryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<List<AdoptionTransactionRequestDto>> Handle(AdoptionTransactionRequestQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching transaction request for OnwerId: {OwnerId}", request.OwnerId);

            var adoptionTransactions = await _dbContext.Adoptions
                .Where(at => at.OwnerId == request.OwnerId)
                .Include(at => at.Adopter)
                .Include(at => at.Pet)
                .Include(at => at.Pet.Species)
                .Select(at => new AdoptionTransactionRequestDto
                {
                    TransactionId = at.AdoptionId,
                    TransactionType = "Adoption",
                    BookingDate = at.BookingDate,
                    Price = at.Pet != null ? at.Pet.Price : 0,
                    Adopter = at.Adopter,
                    Pet = at.Pet,
                })
                .ToListAsync(cancellationToken);

            var transactions = adoptionTransactions
                                .OrderByDescending(t => t.BookingDate)
                                .ToList();

            if (!transactions.Any())
            {
                _logger.LogWarning("No transactions found for OwnerId: {OwnerId}", request.OwnerId);
                throw new ApiException(HttpStatusCode.NotFound, "Adoption transaction request not found", "No transaction request available for the given OwnerId.");
            }

            _logger.LogInformation("Retrieved {Count} transaction request for OwnerId: {OwnerId}", transactions.Count, request.OwnerId);
            return transactions;
        }

    }
}
