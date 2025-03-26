using FluentValidation;
using PetPals_BackEnd_Group_9.Models;

namespace PetPals_BackEnd_Group_9.Validators
{
    public class RegisterValidator : AbstractValidator<RegisterRequestDto>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(255);
            RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(100);
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8).MaximumLength(255);
            RuleFor(x => x.Phone).NotEmpty().MaximumLength(20);
            RuleFor(x => x.Address).NotEmpty().MaximumLength(255);
            RuleFor(x => x.City).NotEmpty().MaximumLength(255);
            RuleFor(x => x.RoleId).GreaterThan(0);
        }
    }
}
