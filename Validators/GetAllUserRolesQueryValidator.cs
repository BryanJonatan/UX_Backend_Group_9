using FluentValidation;
using PetPals_BackEnd_Group_9.Models;

namespace PetPals_BackEnd_Group_9.Validators
{
    public class GetAllUserRolesQueryValidator : AbstractValidator<GetAllUserRolesQuery>
    {
        public GetAllUserRolesQueryValidator()
        {
            RuleFor(x => x.RoleId).GreaterThan(0).When(x => x.RoleId.HasValue);
            RuleFor(x => x.Name).MaximumLength(100);
        }
    }

}
