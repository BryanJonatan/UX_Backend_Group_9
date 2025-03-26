using FluentValidation;
using PetPals_BackEnd_Group_9.Command;

namespace PetPals_BackEnd_Group_9.Validators
{
    public class CreateForumPostValidator : AbstractValidator<CreateForumPostCommand>
    {
        public CreateForumPostValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0);
            RuleFor(x => x.ForumCategoryId).GreaterThan(0);
            RuleFor(x => x.Title).NotEmpty().MaximumLength(255);
            RuleFor(x => x.Content).NotEmpty().MaximumLength(255);
        }
    }

}
