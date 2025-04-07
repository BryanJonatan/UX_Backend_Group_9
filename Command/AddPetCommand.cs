using MediatR;
using PetPals_BackEnd_Group_9.Models;

namespace PetPals_BackEnd_Group_9.Command
{
    public class AddPetCommand : IRequest<Pet>
    {
        public string Name { get; set; } = string.Empty;
        public string Breed { get; set; } = string.Empty;
        public decimal Age { get; set; } = decimal.Zero;
        public string Gender { get; set; } = string.Empty;
        public int SpeciesId { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; } = decimal.Zero;
        public string? ImageUrl { get; set; }
        public int OwnerId { get; set; }
    }
}
