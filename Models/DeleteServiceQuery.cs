using MediatR;

namespace PetPals_BackEnd_Group_9.Models
{
    public class DeleteServiceQuery: IRequest<DeleteServiceResult>
    {
        public int ServiceId {  get; set; }
        public DeleteServiceQuery(int ServiceId)
        {
            this.ServiceId = ServiceId;
        }
    }
}
