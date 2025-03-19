using System.ComponentModel.DataAnnotations;

namespace PetPals_BackEnd_Group_9.Models
{
    public record Pet
    {
        public int PetId { get; set; }
        public required string Name { get; set; }
        public required string Breed { get; set; }
        public int Age { get; set; }
        public int SpeciesId { get; set; }
        public required Species Species { get; set; }  
        public decimal Price { get; set; }
        public required string Status { get; set; }

        public int OwnerId { get; set; } 
        public required User Owner { get; set; }  
    }


}
