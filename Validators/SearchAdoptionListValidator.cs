using FluentValidation;
using PetPals_BackEnd_Group_9.Models;

public class SearchAdoptionListValidator : AbstractValidator<SearchAdoptionListQuery>
{
    public SearchAdoptionListValidator()
    {
        // Age
        RuleFor(q => q.MinAge)
            .Must(x => !x.HasValue || x.Value >= 0)
            .WithMessage("MinAge must be greater than or equal to 0.");

        RuleFor(q => q.MaxAge)
            .Must(x => !x.HasValue || x.Value >= 0)
            .WithMessage("MaxAge must be greater than or equal to 0.");

        RuleFor(q => q.MinAge)
            .Custom((minAge, context) =>
            {
                var instance = context.InstanceToValidate;
                if (minAge.HasValue && instance.MaxAge.HasValue && minAge > instance.MaxAge)
                {
                    context.AddFailure("MinAge", "MinAge must be less than or equal to MaxAge.");
                }
            });

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
