using MediatR;

namespace PetPals_BackEnd_Group_9.Models
{
    public class AdoptionTransactionRequestQuery : IRequest<List<AdoptionTransactionRequestDto>>
    {
        public int OwnerId { get; set; }
    }
}
