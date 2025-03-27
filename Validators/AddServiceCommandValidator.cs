using FluentValidation;
using PetPals_BackEnd_Group_9.Command;

namespace PetPals_BackEnd_Group_9.Validators
{
    public class AddServiceCommandValidator : AbstractValidator<AddServiceCommand>
    {
        public AddServiceCommandValidator()
        {
            RuleFor(x => x.ProviderId).GreaterThan(0).WithMessage("Invalid provider ID");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.CategoryId).GreaterThan(0).WithMessage("Invalid category ID");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero");
            RuleFor(x => x.CreatedBy).NotEmpty().WithMessage("CreatedBy is required");
        }
    }
}
