using FluentValidation;
using PetPals_BackEnd_Group_9.Command;

namespace PetPals_BackEnd_Group_9.Validators
{
    public class ServiceTransactionValidator : AbstractValidator<ServiceTransactionCommand>
    {
        public ServiceTransactionValidator()
        {
            RuleFor(x => x.ServiceId)
                .NotEmpty().WithMessage("ServiceId is required.");

            RuleFor(x => x.AdopterId)  
                .NotEmpty().WithMessage("AdopterId is required.");

            RuleFor(x => x.BookingDate)
           .Must(date => date.Date >= DateTimeOffset.UtcNow.Date)
           .WithMessage("Booking date cannot be in the past.");
        }
    }
}
