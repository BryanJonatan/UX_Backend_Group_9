using FluentValidation;
using PetPals_BackEnd_Group_9.Models;

namespace PetPals_BackEnd_Group_9.Validators
{
    public class GetServiceListQueryValidator : AbstractValidator<GetServiceListQuery>
    {
        public GetServiceListQueryValidator()
        {
            // Price
            RuleFor(q => q.MinPrice)
                .Must(x => !x.HasValue || x.Value >= 0)
                .WithMessage("MinPrice must be greater than or equal to 0.");

            RuleFor(q => q.MaxPrice)
                .Must(x => !x.HasValue || x.Value >= 0)
                .WithMessage("MaxPrice must be greater than or equal to 0.");

            RuleFor(q => q.MinPrice)
                .Custom((minPrice, context) =>
                {
                    var instance = context.InstanceToValidate;
                    if (minPrice.HasValue && instance.MaxPrice.HasValue && minPrice > instance.MaxPrice)
                    {
                        context.AddFailure("MinPrice", "MinPrice must be less than or equal to MaxPrice.");
                    }
                });
        }
    }
}
