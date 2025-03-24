using FluentValidation;
using PetPals_BackEnd_Group_9.Models;

namespace PetPals_BackEnd_Group_9.Validators
{
    public class GetAllSpeciesValidator : AbstractValidator<GetAllSpeciesQuery>
    {
        public GetAllSpeciesValidator()
        {
            RuleFor(x => x.SpeciesId)
                .GreaterThan(0).When(x => x.SpeciesId.HasValue)
                .WithMessage("Species ID harus lebih besar dari 0.");

            RuleFor(x => x.Name)
                .MaximumLength(100)
                .WithMessage("Nama spesies tidak boleh lebih dari 100 karakter.");
        }
    }
}
