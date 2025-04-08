using FluentValidation;
using PetPals_BackEnd_Group_9.Models;

namespace PetPals_BackEnd_Group_9.Validators
{
    public class RegisterValidator : AbstractValidator<RegisterRequestDto>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Matches(@"^[\p{L}\s]+$").WithMessage("Name can only contain letters and spaces.") // bisa mengandung aksen (misal: "José", "Chloë")
                .MaximumLength(255).WithMessage("Name can't be more than 255 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email format is not valid.")
                .MaximumLength(100).WithMessage("Email can't be more than 100 characters");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must contain at least 8 characters.")
                .MaximumLength(255).WithMessage("Password can't be more than 255 characters.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^[0-9]+$").WithMessage("Phone number can only be numerics.")
                .MaximumLength(20).WithMessage("Phone number can't be more than 20 characters.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(255).WithMessage("Address can't be more than 255 characters.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City is required.")
                .MaximumLength(255).WithMessage("City can't be more than 255 characters.");

            RuleFor(x => x.RoleId)
                .GreaterThan(0).WithMessage("Role is required.");
        }
    }
}
