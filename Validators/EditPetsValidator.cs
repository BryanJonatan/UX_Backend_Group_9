using FluentValidation;
using PetPals_BackEnd_Group_9.Command;

namespace PetPals_BackEnd_Group_9.Validators
{
    public class EditPetsValidator : AbstractValidator<EditPetsCommand>
    {
        public EditPetsValidator()
        {
            RuleFor(x => x.PetId).GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Breed).NotEmpty();
            RuleFor(x => x.Age).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
        }
    }
}
