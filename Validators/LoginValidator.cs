using FluentValidation;
using PetPals_BackEnd_Group_9.Command;

namespace PetPals_BackEnd_Group_9.Validators
{
    public class LoginValidator : AbstractValidator<LoginCommand>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email tidak boleh kosong.")
                .EmailAddress().WithMessage("Format email tidak valid.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password tidak boleh kosong.");
        }
    }
}
