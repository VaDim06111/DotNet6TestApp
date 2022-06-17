namespace TestApp.Core
{
    public interface IEmailSenderService
    {
        Task<bool> SendEmailAsync(EmailMessage message);
    }
}
