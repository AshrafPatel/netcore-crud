using FluentValidation;
using Walks.API.Models.DTO;

namespace Walks.API.Validators
{
    public class UpdateWalkDifficultyValidator : AbstractValidator<UpdateWalkDifficultyDto>
    {
        public UpdateWalkDifficultyValidator() 
        {
            RuleFor(x => x.Code).NotEmpty().MinimumLength(2);
        }
    }
}
