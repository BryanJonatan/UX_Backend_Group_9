using FluentValidation;
using PetPals_BackEnd_Group_9.Models;

namespace PetPals_BackEnd_Group_9.Models
{
    public class GetProviderServicesValidator : AbstractValidator<GetProviderServicesQuery> 
    {
        public GetProviderServicesValidator() 
        {
            RuleFor(x => x.providerId).GreaterThan(0).WithMessage("Provider Id must be greater than 0.");
        }
    }
}
