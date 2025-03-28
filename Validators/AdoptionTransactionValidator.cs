using FluentValidation;
using PetPals_BackEnd_Group_9.Command;
using PetPals_BackEnd_Group_9.Models;

namespace PetPals_BackEnd_Group_9.Validators
{
    public class AdoptionTransactionValidator : AbstractValidator<AdoptionTransactionRequest>
    {
        public AdoptionTransactionValidator()
        {
            RuleFor(x => x.PetId).GreaterThan(0).WithMessage("PetId must be greater than 0.");
            RuleFor(x => x.AdopterId).GreaterThan(0).WithMessage("AdopterId must be greater than 0.");
            RuleFor(x => x.OwnerId).GreaterThan(0).WithMessage("OwnerId must be greater than 0.");
        }

       
    }
}
