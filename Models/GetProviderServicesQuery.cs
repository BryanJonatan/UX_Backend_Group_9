using MediatR;

namespace PetPals_BackEnd_Group_9.Models
{
    public class GetProviderServicesQuery : IRequest<List<GetProviderServicesResponse>>
    {
        public int providerId { get; set; }
        public GetProviderServicesQuery(int providerId)
        {
            this.providerId = providerId;
        }
    }
}
