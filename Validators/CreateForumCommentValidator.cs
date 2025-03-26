using FluentValidation;
using PetPals_BackEnd_Group_9.Command;

namespace PetPals_BackEnd_Group_9.Validators
{
    public class CreateForumCommentValidator : AbstractValidator<CreateForumCommentCommand>
    {
        public CreateForumCommentValidator()
        {
            RuleFor(x => x.PostId).GreaterThan(0);
            RuleFor(x => x.UserId).GreaterThan(0);
            RuleFor(x => x.Comment).NotEmpty().MaximumLength(255);
        }
    }
}
