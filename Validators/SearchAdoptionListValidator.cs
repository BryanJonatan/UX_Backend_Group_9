using FluentValidation;
using PetPals_BackEnd_Group_9.Models;

namespace PetPals_BackEnd_Group_9.Validators
{
    public class SearchAdoptionListValidator : AbstractValidator<SearchAdoptionListQuery>
    {
        public SearchAdoptionListValidator()
        {
            RuleFor(q => q.MinAge).GreaterThanOrEqualTo(0).When(q => q.MinAge.HasValue);
            RuleFor(q => q.MaxAge).GreaterThanOrEqualTo(q => q.MinAge).When(q => q.MaxAge.HasValue);
            RuleFor(q => q.MinPrice).GreaterThanOrEqualTo(0).When(q => q.MinPrice.HasValue);
            RuleFor(q => q.MaxPrice).GreaterThanOrEqualTo(q => q.MinPrice).When(q => q.MaxPrice.HasValue);
        }
    }
}
