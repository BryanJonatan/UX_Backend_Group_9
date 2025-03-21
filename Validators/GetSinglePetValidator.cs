using FluentValidation;
using PetPals_BackEnd_Group_9.Models;

namespace PetPals_BackEnd_Group_9.Validators
{
    public class GetSinglePetValidator : AbstractValidator<GetSinglePetQuery>
    {
        public GetSinglePetValidator()
        {
            RuleFor(x => x.Slug).NotEmpty().WithMessage("Slug cannot be empty.");
        }
    }

}
