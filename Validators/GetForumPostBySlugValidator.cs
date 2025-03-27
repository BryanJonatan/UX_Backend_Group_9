using FluentValidation;
using PetPals_BackEnd_Group_9.Models;

namespace PetPals_BackEnd_Group_9.Validators
{
    public class GetForumPostBySlugValidator : AbstractValidator<GetForumPostBySlugQuery>
    {
        public GetForumPostBySlugValidator()
        {
            RuleFor(x => x.Slug)
                .NotEmpty().WithMessage("Slug is required.")
                .Matches("^[a-z0-9-]+$").WithMessage("Slug must only contain lowercase letters, numbers, and hyphens.");
        }
    }
}
