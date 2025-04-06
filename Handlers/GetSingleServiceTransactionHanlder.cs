using MediatR;
using Microsoft.EntityFrameworkCore;
using PetPals_BackEnd_Group_9.Models;
using Serilog;

namespace PetPals_BackEnd_Group_9.Handlers
{
    public class GetSingleServiceTransactionHanlder : IRequestHandler<GetSingleServiceTransactionQuery, GetSingleServiceTransactionResponse>
    {
        private readonly PetPalsDbContext _context;

        public GetSingleServiceTransactionHanlder(PetPalsDbContext context)
        {
            _context = context;
        }

        public async Task<GetSingleServiceTransactionResponse> Handle(GetSingleServiceTransactionQuery request, CancellationToken cancellationToken)
        {
            var serviceTransaction = await _context.ServiceTransactions
                .Include(st => st.Adopter)
                .Include(st => st.Provider)
                   .Include(at => at.Adopter.Role)
                 .Include(at => at.Provider.Role)
                .Include(st => st.Service)
                .Include(st => st.Service.Category)
                .Where(st => st.TransactionId == request.TransactionId)
                .Select(st => new GetSingleServiceTransactionResponse
                {
                    TransactionId = request.TransactionId,
                    BookingDate = st.BookingDate.ToString(),
                    Status = st.Status,
                    Price = st.Price,
                    Adopter = st.Adopter,
                    Provider = st.Provider,
                    Service = st.Service,
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (serviceTransaction == null)
            {
                Log.Information("Service transaction not found: {TransactionId}", request.TransactionId);
                throw new NotFoundException($"Service transaction with id '{request.TransactionId}' not found");
            }

            return serviceTransaction;
        }
    }
}
