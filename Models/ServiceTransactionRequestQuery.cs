using MediatR;

namespace PetPals_BackEnd_Group_9.Models
{
    public class ServiceTransactionRequestQuery : IRequest<List<ServiceTransactionRequestDto>>
    {
        public int ProviderId { get; set; }
    }
}
