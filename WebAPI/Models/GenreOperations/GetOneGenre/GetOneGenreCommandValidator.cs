using FluentValidation;

namespace WebAPI.Models.GenreOperations.GetOneGenre
{
    public class GetOneGenreCommandValidator : AbstractValidator<GetOneGenreCommand>
    {
        public GetOneGenreCommandValidator()
        {
            RuleFor(query => query.GenreID).GreaterThan(0);
        }
    }
}
