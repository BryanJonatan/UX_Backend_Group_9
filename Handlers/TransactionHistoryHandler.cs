using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetPals_BackEnd_Group_9.Models;
using System.Net;

namespace PetPals_BackEnd_Group_9.Handlers
{
    public class TransactionHistoryHandler : IRequestHandler<TransactionHistoryQuery, List<TransactionHistoryDto>>
    {
        private readonly PetPalsDbContext _dbContext;
        private readonly ILogger<TransactionHistoryHandler> _logger;

        public TransactionHistoryHandler(PetPalsDbContext dbContext, ILogger<TransactionHistoryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<List<TransactionHistoryDto>> Handle(TransactionHistoryQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching transaction history for AdopterId: {AdopterId}", request.AdopterId);

            var adoptions = await _dbContext.Adoptions
                .Where(a => a.AdopterId == request.AdopterId)
                .Include(a => a.Pet)
                .Select(a => new TransactionHistoryDto
                {
                    TransactionType = "Adoption",
                    PetOrServiceName = a.Pet != null ? a.Pet.Name : "Unknown Pet",
                    BookingDate = (DateTimeOffset)a.BookingDate,
                    Price = a.Pet != null ? a.Pet.Price : 0,
                    Item = a.Pet
                })
                .ToListAsync(cancellationToken);

            var serviceTransactions = await _dbContext.ServiceTransactions
                .Where(st => st.AdopterId == request.AdopterId)
                .Include(st => st.Service)
                .Select(st => new TransactionHistoryDto
                {
                    TransactionType = "Service",
                    PetOrServiceName = st.Service != null ? st.Service.Name : "Unknown Service",
                    BookingDate = st.BookingDate,
                    Price = st.Service != null ? st.Service.Price : 0,
                    Item = st.Service
                })
                .ToListAsync(cancellationToken);

            List<TransactionHistoryDto> transactions = new List<TransactionHistoryDto>();

            if (request.TransactionType.ToLower().Equals("adoption"))
            {
                transactions = adoptions
                                .OrderByDescending(t => t.BookingDate)
                                .ToList();
            }
            else if (request.TransactionType.ToLower().Equals("service"))
            {
                transactions = serviceTransactions
                                .OrderByDescending(t => t.BookingDate)
                                .ToList();
            }
            else
            {
                transactions = adoptions
                                .Concat(serviceTransactions)
                                .OrderByDescending(t => t.BookingDate)
                                .ToList();
            }

            if (!transactions.Any())
            {
                _logger.LogWarning("No transactions found for AdopterId: {AdopterId}", request.AdopterId);
                throw new ApiException(HttpStatusCode.NotFound, "Transaction history not found", "No transactions available for the given AdopterId.");
            }

            _logger.LogInformation("Retrieved {Count} transactions for AdopterId: {AdopterId}", transactions.Count, request.AdopterId);
            return transactions;
        }

    }
}
