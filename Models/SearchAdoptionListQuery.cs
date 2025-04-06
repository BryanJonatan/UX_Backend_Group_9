using MediatR;

namespace PetPals_BackEnd_Group_9.Models
{
    public record SearchAdoptionListQuery : IRequest<List<AdoptionListDto>>
    {
        public string? Name { get; set; }
        public decimal? MinAge { get; set; }
        public decimal? MaxAge { get; set; }
        public string? Breed { get; set; }
        public string? Species { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}
