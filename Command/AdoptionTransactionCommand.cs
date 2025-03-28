using MediatR;
using PetPals_BackEnd_Group_9.Models;

namespace PetPals_BackEnd_Group_9.Command
{
    public class AdoptionTransactionCommand : IRequest<AdoptionTransactionResponse>
    {
        public int AdopterId { get; set; }
        public int OwnerId { get; set; }
        public int PetId { get; set; }
        public AdoptionTransactionCommand(int petId, int adopterId, int ownerId)
        {
            PetId = petId;
            AdopterId = adopterId;
            OwnerId = ownerId;
        }
    }
}
