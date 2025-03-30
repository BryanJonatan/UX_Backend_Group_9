using MediatR;

namespace PetPals_BackEnd_Group_9.Models
{
    public class GetOwnerPetsQuery : IRequest<List<GetOwnerPetsResponse>>
    {
        public int ownerId { get; set; }
        public GetOwnerPetsQuery(int ownerId)
        {
            this.ownerId = ownerId;
        }
    }
}
