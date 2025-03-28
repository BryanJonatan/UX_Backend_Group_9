using MediatR;
using PetPals_BackEnd_Group_9.Models;

namespace PetPals_BackEnd_Group_9.Command
{
    public class ServiceTransactionCommand : IRequest<ServiceTransactionResponse>
    {
        public int AdopterId { get; set; }  
        public int ProviderId { get; set; }
        public int ServiceId { get; set; }
        public DateTimeOffset BookingDate { get; set; }

        public ServiceTransactionCommand (int adopterId, int serviceId, DateTimeOffset bookingDate)
        {
            AdopterId = adopterId;
            ServiceId = serviceId;
            BookingDate = bookingDate;
        }
    }
}
