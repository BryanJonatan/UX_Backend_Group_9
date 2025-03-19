using MediatR;

namespace PetPals_BackEnd_Group_9.Models
{
    public record SearchAdoptionListQuery : IRequest<List<AdoptionListDto>>
    {
        public string? Name { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        public string? Breed { get; set; }
        public int? SpeciesId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}
