using MediatR;

namespace PetPals_BackEnd_Group_9.Models
{
    public class DeletePetQuery: IRequest<DeletePetResult>
    {
        public int PetId {  get; set; }
        public DeletePetQuery(int PetId)
        {
            this.PetId = PetId;
        }
    }
}
