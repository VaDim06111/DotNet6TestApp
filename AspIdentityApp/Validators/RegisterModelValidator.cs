using FluentValidation;

namespace AspIdentityApp.Validators
{
    public class RegisterModelValidator : AbstractValidator<RegisterModel>
    {
        public RegisterModelValidator()
        {
            RuleFor(c => c.Email).NotEmpty()
                .EmailAddress()
                .WithMessage("Incorrect e-mail");
            RuleFor(c => c.Password).NotEmpty()
                .MinimumLength(6)
                .WithMessage("Password is required. By default, passwords must contain an uppercase character, " +
                "lowercase character, a digit, and a non-alphanumeric character. " +
                "Passwords must be at least six characters long.");
            RuleFor(c => c.Username).NotEmpty()
                .WithMessage("Username is required");
        }
    }
}
