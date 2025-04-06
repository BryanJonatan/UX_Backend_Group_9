using MediatR;
using Microsoft.EntityFrameworkCore;
using PetPals_BackEnd_Group_9.Models;
using Serilog;

namespace PetPals_BackEnd_Group_9.Handlers
{
    public class GetSingleAdoptionTransactionHandler : IRequestHandler<GetSingleAdoptionTransactionQuery, GetSingleAdoptionTransactionResponse>
    {
        private readonly PetPalsDbContext _context;

        public GetSingleAdoptionTransactionHandler(PetPalsDbContext context)
        {
            _context = context;
        }

        public async Task<GetSingleAdoptionTransactionResponse> Handle(GetSingleAdoptionTransactionQuery request, CancellationToken cancellationToken)
        {
            var adoptionTransaction = await _context.Adoptions
                .Include(at => at.Adopter)
                .Include(at => at.Owner)
                .Include(at => at.Adopter.Role)
                .Include(at => at.Owner.Role)
                .Include(at => at.Pet)
                .Include(at => at.Pet.Species)
                .Where(at => at.AdoptionId == request.AdoptionId)
                .Select(at => new GetSingleAdoptionTransactionResponse
                {
                    AdoptionId = request.AdoptionId,
                    BookingDate = at.BookingDate.ToString(),
                    Status = at.Status,
                    Price = at.Price,
                    Adopter = at.Adopter,
                    Owner = at.Owner,
                    Pet = at.Pet,
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (adoptionTransaction == null)
            {
                Log.Information("Adoption transaction not found: {AdoptionId}", request.AdoptionId);
                throw new NotFoundException($"Adoption transaction with id '{request.AdoptionId}' not found");
            }

            return adoptionTransaction;
        }
    }
}
