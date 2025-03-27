using FluentValidation;
using PetPals_BackEnd_Group_9.Models;

namespace PetPals_BackEnd_Group_9.Validators
{
    public class GetForumCategoriesValidator : AbstractValidator<GetForumCategoriesQuery>
    {
        public GetForumCategoriesValidator()
        {
            RuleFor(x => x.ForumCategoryId)
                .GreaterThan(0).When(x => x.ForumCategoryId.HasValue)
                .WithMessage("ForumCategoryId must be greater than zero.");
        }
    }

}
