using FluentValidation;
using Walks.API.Models.DTO;

namespace Walks.API.Validators
{
    public class AddWalkDifficultyValidator : AbstractValidator<AddWalkDifficultyDto>
    {
        public AddWalkDifficultyValidator() 
        {
            RuleFor(x => x.Code).NotEmpty().MinimumLength(2);
        }
    }
}
