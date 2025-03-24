using MediatR;

namespace PetPals_BackEnd_Group_9.Models
{
    public class GetAllSpeciesQuery : IRequest<List<SpeciesDto>>
    {
        public int? SpeciesId { get; set; }
        public string? Name { get; set; }
    }
}
