using FluentValidation;
using PetPals_BackEnd_Group_9.Models;

namespace PetPals_BackEnd_Group_9.Validators
{
    public class TransactionHistoryValidator : AbstractValidator<TransactionHistoryQuery>
    {
        public TransactionHistoryValidator()
        {
            RuleFor(x => x.AdopterId)
                .GreaterThan(0)
                .WithMessage("AdopterId must be greater than 0.");
        }
    }
}
