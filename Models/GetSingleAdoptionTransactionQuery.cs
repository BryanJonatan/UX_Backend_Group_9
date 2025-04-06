using MediatR;

namespace PetPals_BackEnd_Group_9.Models
{
    public class GetSingleAdoptionTransactionQuery : IRequest<GetSingleAdoptionTransactionResponse>
    {
        public int AdoptionId { get; set; }
        public GetSingleAdoptionTransactionQuery(int AdoptionId)
        {
            this.AdoptionId = AdoptionId;
        }
    }
}
