using FluentValidation;
using PetPals_BackEnd_Group_9.Command;

namespace PetPals_BackEnd_Group_9.Validators
{
    public class EditPetCommandValidator : AbstractValidator<EditPetsCommand>
    {
        public EditPetCommandValidator() 
        {
            // Name
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Matches(@"^[\p{L} ]+$").WithMessage("Name can only contain letters and spaces."); // bisa mengandung aksen (misal: "José", "Chloë")

            // Breed
            RuleFor(p => p.Breed)
                .NotEmpty().WithMessage("Breed is required.");

            // Species
            RuleFor(p => p.SpeciesId)
                .GreaterThan(0).WithMessage("Species is required.");

            // Age
            RuleFor(p => p.Age)
                .NotEmpty().WithMessage("Age is required.")
                .GreaterThan(0).WithMessage("Age must be a non-negative number.");

            // Gender
            RuleFor(p => p.Gender)
                .NotEmpty().WithMessage("Gender is required.");

            // Price
            RuleFor(p => p.Price)
                .NotEmpty().WithMessage("Price is required.")
                .GreaterThan(0).WithMessage("Price must be a non-negative number.");
        }
    }
}
