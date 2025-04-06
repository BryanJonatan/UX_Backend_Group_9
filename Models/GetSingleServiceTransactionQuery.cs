using MediatR;

namespace PetPals_BackEnd_Group_9.Models
{
    public class GetSingleServiceTransactionQuery : IRequest<GetSingleServiceTransactionResponse>
    {
        public int TransactionId { get; set; }
        public GetSingleServiceTransactionQuery(int TransactionId)
        {
            this.TransactionId = TransactionId;
        }
    }
}
