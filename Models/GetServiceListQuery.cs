using MediatR;

namespace PetPals_BackEnd_Group_9.Models
{
    public record GetServiceListQuery : IRequest<List<ServiceDto>>
    {
        public string? Name { get; set; }
        public string? CategoryName { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string? City { get; set; }
    }

}
