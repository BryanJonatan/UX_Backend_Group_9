using FluentValidation;
using PetPals_BackEnd_Group_9.Models;

namespace PetPals_BackEnd_Group_9.Validators
{
    public class GetForumCommentsByPostIdValidator : AbstractValidator<GetForumCommentsByPostIdQuery>
    {
        public GetForumCommentsByPostIdValidator()
        {
            RuleFor(x => x.PostId)
              .GreaterThan(0)
              .When(x => x.PostId.HasValue)
              .WithMessage("PostId must be greater than zero if provided.");
        }
    }
}
