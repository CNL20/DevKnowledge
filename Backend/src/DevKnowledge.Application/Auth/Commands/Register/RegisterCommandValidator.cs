using FluentValidation;

namespace DevKnowledge.Application.Auth.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email is invalid.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters.");

        RuleFor(x => x.DisplayName)
            .NotEmpty().WithMessage("Display Name is required.")
            .MaximumLength(100).WithMessage("Display Name must not exceed 100 characters.");
    }
}
