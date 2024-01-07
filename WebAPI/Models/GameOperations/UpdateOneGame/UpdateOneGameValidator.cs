using FluentValidation;
using WebAPI.Models.BookOperations.UpdateOneGame;

namespace WebAPI.Models.GameOperations.UpdateOneGame
{
    public class UpdateOneGameValidator : AbstractValidator<UpdateOneGameCommand>
    {
        public UpdateOneGameValidator()
        {
            RuleFor(x => x.Model.Price).NotEmpty().GreaterThan(0).WithMessage("Price must be bigger than 0");
            RuleFor(x => x.Model.Title).NotEmpty().MinimumLength(10).MaximumLength(100).WithMessage("Title length must be between 10 and 100");
        }
    }
}
