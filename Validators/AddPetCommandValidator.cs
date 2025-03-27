using FluentValidation;
using PetPals_BackEnd_Group_9.Command;

namespace PetPals_BackEnd_Group_9.Validators
{
    public class AddPetCommandValidator : AbstractValidator<AddPetCommand>
    {
        public AddPetCommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Pet name is required.");
            RuleFor(p => p.Breed).NotEmpty().WithMessage("Breed is required.");
            RuleFor(p => p.Age).GreaterThanOrEqualTo(0).WithMessage("Age must be a non-negative number.");
            RuleFor(p => p.SpeciesId).GreaterThan(0).WithMessage("Invalid species ID.");
            RuleFor(p => p.Description).NotEmpty().WithMessage("Description is required.");
            RuleFor(p => p.Price).GreaterThanOrEqualTo(0).WithMessage("Price must be a non-negative number.");
            RuleFor(p => p.OwnerId).GreaterThan(0).WithMessage("Invalid owner ID.");
        }
    }
}
