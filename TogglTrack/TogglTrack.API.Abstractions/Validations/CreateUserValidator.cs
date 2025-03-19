using FluentValidation;

namespace TogglTrack.API.Abstractions.Validations
{
    public class CreateUserValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("First name is required.")
                .Must(name => name.All(char.IsLetter))
                .WithMessage("First name must contain only letters.")
                .Length(2, 30)
                .WithMessage("First name must be between 2 and 30 characters long.");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Last name is required.")
                .Must(name => name.All(char.IsLetter))
                .WithMessage("Last name must contain only letters.")
                .Length(2, 30)
                .WithMessage("Last name must be between 2 and 30 characters long.");
        }
    }
}
