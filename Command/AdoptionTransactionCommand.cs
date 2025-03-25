using MediatR;
using PetPals_BackEnd_Group_9.Models;

namespace PetPals_BackEnd_Group_9.Command
{
    public class AdoptionTransactionCommand : IRequest<AdoptionTransactionResponse>
    {
        public int PetId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Breed { get; set; } = string.Empty;
        public int UserId { get; set; }
        public DateTimeOffset? BookingDate { get; set; }  // ✅ Fix: Add BookingDate

        public AdoptionTransactionCommand(int petId, string name, string breed, int userId, DateTimeOffset? bookingDate = null)
        {
            PetId = petId;
            Name = name;
            Breed = breed;
            UserId = userId;
            BookingDate = bookingDate;
        }
    }
}
