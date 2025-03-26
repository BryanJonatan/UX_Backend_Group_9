using MediatR;

namespace PetPals_BackEnd_Group_9.Models
{
    public class TransactionHistoryQuery : IRequest<List<TransactionHistoryDto>>
    {
        public int AdopterId { get; set; }
        public string TransactionType { get; set; }
    }
}
