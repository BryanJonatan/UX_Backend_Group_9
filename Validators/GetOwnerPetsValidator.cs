using FluentValidation;
using PetPals_BackEnd_Group_9.Models;

namespace PetPals_BackEnd_Group_9.Validators
{
    public class GetOwnerPetsValidator : AbstractValidator<GetOwnerPetsQuery>
    {
        public GetOwnerPetsValidator() 
        {
            RuleFor(x => x.ownerId).GreaterThan(0).WithMessage("Owner Id must be greater than 0.");
        }
    }
}
