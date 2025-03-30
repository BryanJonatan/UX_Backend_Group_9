using FluentValidation;
using PetPals_BackEnd_Group_9.Command;

namespace PetPals_BackEnd_Group_9.Validators
{
    public class EditServiceValidator : AbstractValidator<EditServiceCommand>
    {
        public EditServiceValidator()
        {
            RuleFor(x => x.ServiceId).GreaterThan(0).WithMessage("ServiceId must be valid.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.CategoryId).GreaterThan(0).WithMessage("CategoryId must be valid.");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");
        }
    }
}
