using FluentValidation;
using WebAPI.Models.BookOperations.CreateOneGame;

namespace WebAPI.Models.GameOperations.CreateOneGame
{
    public class CreateOneGameCommandValidator : AbstractValidator<CreateOneGameCommand>
    {
        public CreateOneGameCommandValidator()
        {
            RuleFor(x => x.Model.Price).NotEmpty().GreaterThan(0).WithMessage("Price must be bigger than 0");
            RuleFor(x => x.Model.Title).NotEmpty().MinimumLength(10).MaximumLength(100).WithMessage("Title length must be between 10 and 100");
        }
    }
}
