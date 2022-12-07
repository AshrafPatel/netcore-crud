using FluentValidation;
using Walks.API.Models.DTO;

namespace Walks.API.Validators
{
    public class AddWalkValidator : AbstractValidator<AddWalkDto>
    {
        public AddWalkValidator() 
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Length).GreaterThan(0);
        }
    }
}
