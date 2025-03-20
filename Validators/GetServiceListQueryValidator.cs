using FluentValidation;
using PetPals_BackEnd_Group_9.Models;

namespace PetPals_BackEnd_Group_9.Validators
{
    public class GetServiceListQueryValidator : AbstractValidator<GetServiceListQuery>
    {
        public GetServiceListQueryValidator()
        {
            RuleFor(x => x.MinPrice)
                .GreaterThanOrEqualTo(0).WithMessage("MinPrice must be at least 0.")
                .When(x => x.MinPrice.HasValue);

            RuleFor(x => x.MaxPrice)
                .GreaterThanOrEqualTo(0).WithMessage("MaxPrice must be at least 0.")
                .When(x => x.MaxPrice.HasValue);

            RuleFor(x => x)
                .Must(x => !x.MinPrice.HasValue || !x.MaxPrice.HasValue || x.MinPrice <= x.MaxPrice)
                .WithMessage("MinPrice cannot be greater than MaxPrice.");
        }
    }
}
