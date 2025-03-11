using FluentValidation;
using Library.Api.Contracts;

namespace Library.Api.Validation
{
    public class RegistrationValidator : AbstractValidator<RegistrationContract>
    {
        public RegistrationValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Enter correct email address.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");

            RuleFor(x => x.DisplayName)
                .MaximumLength(20).WithMessage("Username can be maximum 20 characters long.")
                .When(x => x.DisplayName.Trim().Length != 0);
        }

    }
}
