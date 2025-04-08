using FluentValidation;
using PetPals_BackEnd_Group_9.Command;

namespace PetPals_BackEnd_Group_9.Validators
{
    public class EditServiceCommandValidator : AbstractValidator<EditServiceCommand>
    {
        public EditServiceCommandValidator() 
        {
            // Name
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.");

            // Category ID
            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Category is required.");

            // Price
            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Price is required.")
                .GreaterThan(0).WithMessage("Price must be greater than zero.");

            // Address
            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.");

            // City
            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City is required.")
                .Matches(@"^[\p{L}\s]+$").WithMessage("City can only contain letters and spaces.");
        }
    }
}
