using MediatR;
using System.ComponentModel.DataAnnotations;

namespace PetPals_BackEnd_Group_9.Models
{
    public class AdoptionListDto : IRequest<List<Pet>>
    {
        public int PetId { get; set; }
        public required string Name { get; set; } 
        public required string Slug { get; set; }
        public required string Breed { get; set; }
        public decimal Age { get; set; }
        public string Gender { get; set; }
        public decimal Price { get; set; }
        public required string Status { get; set; }
        public required Species Species { get; set; }
    }

}
