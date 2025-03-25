using MediatR;
using Microsoft.EntityFrameworkCore;
using PetPals_BackEnd_Group_9.Command;
using PetPals_BackEnd_Group_9.Models;

namespace PetPals_BackEnd_Group_9.Handlers
{
    public class ServiceTransactionHandler : IRequestHandler<ServiceTransactionCommand, ServiceTransactionResponse>
    {
        private readonly PetPalsDbContext _context;
        private readonly ILogger<ServiceTransactionHandler> _logger;

        public ServiceTransactionHandler(PetPalsDbContext context, ILogger<ServiceTransactionHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ServiceTransactionResponse> Handle(ServiceTransactionCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Processing service transaction for ServiceId: {ServiceId}, AdopterId: {AdopterId}", command.ServiceId, command.AdopterId);

            // ✅ Check if Adopter (User) exists
            var adopterExists = await _context.Users.AnyAsync(u => u.UserId == command.AdopterId, cancellationToken);
            if (!adopterExists)
            {
                _logger.LogWarning("Adopter with ID {AdopterId} not found.", command.AdopterId);
                throw new CustomException2("Adopter not found.", "adopter_not_found", 404);
            }

            // ✅ Check if Service exists
            var service = await _context.Services.FirstOrDefaultAsync(s => s.ServiceId == command.ServiceId, cancellationToken);
            if (service == null)
            {
                _logger.LogWarning("Service with ID {ServiceId} not found.", command.ServiceId);
                throw new CustomException2("Service not found.", "service_not_found", 404);
            }

            // ✅ Create Service Transaction
            var transaction = new ServiceTransaction
            {
                AdopterId = command.AdopterId,
                ServiceId = command.ServiceId,
                BookingDate = command.BookingDate,  // ✅ Allow user-specified BookingDate
                Price = service.Price,  // ✅ Fetch price from DB, preventing modification
                Status = "Completed",
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = command.AdopterId.ToString(),
                UpdatedAt = DateTimeOffset.UtcNow,
                UpdatedBy = command.AdopterId.ToString(),
            };

            // ✅ Save transaction to DB
            _context.ServiceTransactions.Add(transaction);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Service transaction {TransactionId} successfully created for AdopterId {AdopterId}.", transaction.TransactionId, command.AdopterId);

            return new ServiceTransactionResponse
            {
                Success = true,
                Message = "Service transaction successful.",
                TransactionId = transaction.TransactionId,
                
            };
        }

    }
}
