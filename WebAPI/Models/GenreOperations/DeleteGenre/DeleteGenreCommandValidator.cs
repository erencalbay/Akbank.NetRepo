using FluentValidation;

namespace WebAPI.Models.GenreOperations.DeleteGenre
{
    public class DeleteGenreCommandValidator : AbstractValidator<DeleteGenreCommand>
    {
        public DeleteGenreCommandValidator()
        {
            RuleFor(command => command.GenreId).NotEmpty().GreaterThan(0);
        }
    }
}
