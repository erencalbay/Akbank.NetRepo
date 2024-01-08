using FluentValidation;

namespace WebAPI.Models.AuthorOperations.GetOneAuthor
{
    public class GetOneAuthorCommandValidator : AbstractValidator<GetOneAuthorCommand>
    {
        public GetOneAuthorCommandValidator()
        {
            RuleFor(command => command.AuthorID).NotEmpty().GreaterThan(0);
        }
    }
}
