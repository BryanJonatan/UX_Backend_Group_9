using FluentValidation;
using PetPals_BackEnd_Group_9.Models;

namespace PetPals_BackEnd_Group_9.Validators
{
    public class GetAllForumCommentsQueryValidator : AbstractValidator<GetAllForumCommentsQuery>
    {
        public GetAllForumCommentsQueryValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0)
                .When(x => x.UserId.HasValue)
                .WithMessage("UserId harus lebih dari 0.");

            RuleFor(x => x.CommentId)
                .GreaterThan(0)
                .When(x => x.CommentId.HasValue)
                .WithMessage("CommentId harus lebih dari 0.");
        }
    }
}
