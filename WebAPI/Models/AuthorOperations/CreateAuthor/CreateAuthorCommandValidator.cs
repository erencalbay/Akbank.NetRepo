using FluentValidation;

namespace WebAPI.Models.AuthorOperations.CreateAuthor
{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(x => x.Model.Name).NotEmpty().MinimumLength(2).MaximumLength(20);
            RuleFor(x => x.Model.Surname).NotEmpty().MinimumLength(2).MaximumLength(20);
            RuleFor(x => x.Model.Birthdate).NotEmpty();
        }
    }
}
