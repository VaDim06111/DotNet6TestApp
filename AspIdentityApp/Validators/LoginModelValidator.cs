using AspIdentityApp.Models;
using FluentValidation;

namespace AspIdentityApp.Validators
{
    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        public LoginModelValidator()
        {
            RuleFor(x => x.Email).EmailAddress()
                .WithMessage("Incorrect e-mail");
            RuleFor(x => x.Password).NotEmpty()
                .WithMessage("Password is required");
        }
    }
}
