using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetPals_BackEnd_Group_9.Models;
using System.Net;

namespace PetPals_BackEnd_Group_9.Handlers
{
    public class ServiceTransactionRequestHandler : IRequestHandler<ServiceTransactionRequestQuery, List<ServiceTransactionRequestDto>>
    {
        private readonly PetPalsDbContext _dbContext;
        private readonly ILogger<TransactionHistoryHandler> _logger;

        public ServiceTransactionRequestHandler(PetPalsDbContext dbContext, ILogger<TransactionHistoryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<List<ServiceTransactionRequestDto>> Handle(ServiceTransactionRequestQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching transaction request for ProviderId: {ProviderId}", request.ProviderId);

            var serviceTransactions = await _dbContext.ServiceTransactions
                .Where(st => st.ProviderId == request.ProviderId)
                .Include(st => st.Adopter)
                .Include(st => st.Service)
                .Include(st => st.Service.Category)
                .Select(st => new ServiceTransactionRequestDto
                {
                    TransactionId = st.TransactionId,
                    TransactionType = "Service",
                    BookingDate = st.BookingDate,
                    Price = st.Service != null ? st.Service.Price : 0,
                    Adopter = st.Adopter,
                    Service = st.Service ?? new Service(),
                })
                .ToListAsync(cancellationToken);

            var transactions = serviceTransactions
                                .OrderByDescending(t => t.BookingDate)
                                .ToList();

            if (!transactions.Any())
            {
                _logger.LogWarning("No transactions found for ProviderId: {ProviderId}", request.ProviderId);
                throw new ApiException(HttpStatusCode.NotFound, "Service transaction request not found", "No transaction request available for the given ProviderId.");
            }

            _logger.LogInformation("Retrieved {Count} transaction request for ProviderId: {ProviderId}", transactions.Count, request.ProviderId);
            return transactions;
        }

    }
}
