using MediatR;
using PetPals_BackEnd_Group_9.Models;

namespace PetPals_BackEnd_Group_9.Command
{
    public class AddPetCommand : IRequest<PetResponse>
    {
        public required string Name { get; set; }
        public required string Breed { get; set; }
        public int Age { get; set; }
        public int SpeciesId { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public int OwnerId { get; set; }
    }
}
