using MediatR;
using System.ComponentModel.DataAnnotations;

namespace PetPals_BackEnd_Group_9.Models
{
    public class AdoptionListDto : IRequest<List<Pet>>
    {
        public int PetId { get; set; }


        public required string Name { get; set; } 

        public required string Breed { get; set; }
        public int Age { get; set; }
        public required string Species { get; set; }
        public decimal Price { get; set; }
        public required string Status { get; set; }
    }

}
