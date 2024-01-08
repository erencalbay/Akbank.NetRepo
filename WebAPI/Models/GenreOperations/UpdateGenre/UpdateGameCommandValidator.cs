using FluentValidation;

namespace WebAPI.Models.GenreOperations.UpdateGenre
{
    public class UpdateGameCommandValidator : AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGameCommandValidator()
        {
            RuleFor(command => command.Model.Name).MinimumLength(3).When(x => x.Model.Name.Trim() != string.Empty);
        }
    }
}
