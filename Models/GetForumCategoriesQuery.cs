using MediatR;

namespace PetPals_BackEnd_Group_9.Models
{
    public record GetForumCategoriesQuery(int? ForumCategoryId) : IRequest<List<ForumCategoryResponse>>;

}
