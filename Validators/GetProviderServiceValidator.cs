using FluentValidation;
using PetPals_BackEnd_Group_9.Models;

namespace PetPals_BackEnd_Group_9.Validators
{
    public class GetProviderServiceValidator : AbstractValidator<GetProviderServiceQuery>
    {
        public GetProviderServiceValidator()
        {
            RuleFor(x => x.ProviderId).GreaterThan(0).WithMessage("ProviderId must be greater than zero.");
        }
    }

}
