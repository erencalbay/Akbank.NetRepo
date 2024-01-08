using FluentValidation;

namespace WebAPI.Models.AuthorOperations.UpdateAuthor
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(command => command.AuthorId).NotEmpty().GreaterThan(0);
            RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(2).MaximumLength(20);
            RuleFor(command => command.Model.Surname).NotEmpty().MinimumLength(2).MaximumLength(20);
        }
    }
}
