using FluentValidation;
using Walks.API.Models.DTO;

namespace Walks.API.Validators
{
    public class UpdateRegionValidator : AbstractValidator<UpdateRegionDto>
    {
        public UpdateRegionValidator() 
        {
            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Area).GreaterThan(0);
            RuleFor(x => x.Population).GreaterThanOrEqualTo(0);
        }
    }
}
