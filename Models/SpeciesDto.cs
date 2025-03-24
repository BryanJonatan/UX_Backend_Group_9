namespace PetPals_BackEnd_Group_9.Models
{
    public class SpeciesDto
    {
        public int SpeciesId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
