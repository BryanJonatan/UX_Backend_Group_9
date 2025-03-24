using MediatR;

namespace PetPals_BackEnd_Group_9.Models
{
    public class GetAllServiceCategoriesQuery : IRequest<List<ServiceCategoryDto>>
    {
        public int? CategoryId { get; set; }
        public string? Name { get; set; }
    }
}
