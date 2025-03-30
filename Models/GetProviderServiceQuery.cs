using MediatR;

namespace PetPals_BackEnd_Group_9.Models
{
    public class GetProviderServiceQuery : IRequest<List<ServiceResponse>>
    {
        public int ProviderId { get; set; }
    }
}
