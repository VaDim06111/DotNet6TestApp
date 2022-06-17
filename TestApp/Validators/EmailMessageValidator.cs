namespace TestApp.Validators
{
    public class EmailMessageValidator : AbstractValidator<EmailMessage>
    {
        public EmailMessageValidator()
        {
            RuleFor(m => m.Recipient).NotEmpty();
            RuleFor(m => m.Subject).NotEmpty();
            RuleFor(m => m.Text).NotEmpty();
        }
    }
}
