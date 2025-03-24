using FluentValidation;
using PetPals_BackEnd_Group_9.Models;

namespace PetPals_BackEnd_Group_9.Validators
{
    public class GetAllServiceCategoriesValidator : AbstractValidator<GetAllServiceCategoriesQuery>
    {
        public GetAllServiceCategoriesValidator()
        {
            RuleFor(x => x.CategoryId).GreaterThan(0).When(x => x.CategoryId.HasValue)
                .WithMessage("Category ID harus lebih besar dari 0.");

            RuleFor(x => x.Name).MaximumLength(100)
                .WithMessage("Nama kategori tidak boleh lebih dari 100 karakter.");
        }
    }
}
