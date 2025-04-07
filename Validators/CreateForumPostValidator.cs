using FluentValidation;
using PetPals_BackEnd_Group_9.Command;

namespace PetPals_BackEnd_Group_9.Validators
{
    public class CreateForumPostValidator : AbstractValidator<CreateForumPostCommand>
    {
        public CreateForumPostValidator()
        {
            // User Id
            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("Invalid User Id.");

            // Forum Category Id
            RuleFor(x => x.ForumCategoryId)
                .GreaterThan(0).WithMessage("Forum category is required.");

            // Title
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(255).WithMessage("Title can't be more than 255 characters.");

            // Content
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required.")
                .MaximumLength(255).WithMessage("Content can't be more than 255 characters.");
        }
    }

}
