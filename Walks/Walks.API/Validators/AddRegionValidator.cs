using FluentValidation;
using Walks.API.Models.DTO;

namespace Walks.API.Validators
{
    public class AddRegionValidator : AbstractValidator<AddRegionDto>
    {
        public AddRegionValidator() 
        { 
            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Area).GreaterThan(0);
            RuleFor(x => x.Population).GreaterThanOrEqualTo(0);
        }
    }
}
