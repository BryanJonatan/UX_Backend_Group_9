using MediatR;
using PetPals_BackEnd_Group_9.Models;

namespace PetPals_BackEnd_Group_9.Command
{
    public class AdoptionTransactionCommand : IRequest<AdoptionTransactionResponse>
    {
        public int PetId { get; set; }
        public int UserId { get; set; }

        public AdoptionTransactionCommand(int petId, int userId)
        {
            PetId = petId;
            UserId = userId;
        }
    }
}
