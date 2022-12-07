using FluentValidation;
using Walks.API.Models.DTO;

namespace Walks.API.Validators
{
    public class UpdateWalkValidator : AbstractValidator<UpdateWalkDto>
    {
        public UpdateWalkValidator() 
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Length).GreaterThan(0);
        }

    }
}
