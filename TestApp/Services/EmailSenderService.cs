using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using TestApp.Core;

namespace TestApp.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly string _host;
        private readonly int _port;
        private readonly string _ownerEmail;
        private readonly string _ownerPassword;

        public EmailSenderService(IConfiguration configuration)
        {           
            _host = configuration.GetSection("EmailSender:HOST").Value;
            _port = int.Parse(configuration.GetSection("EmailSender:PORT").Value);
            _ownerEmail = configuration.GetSection("EmailSender:OWNER_USERNAME").Value;
            _ownerPassword = configuration.GetSection("EmailSender:OWNER_PASSWORD").Value;
        }

        public async Task<bool> SendEmailAsync(EmailMessage message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(MailboxAddress.Parse(_ownerEmail));

            // if recipient host gmail.com, should edit smtp host name
            emailMessage.To.Add(MailboxAddress.Parse(message.Recipient));

            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(TextFormat.Html) { Text = message.Text };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_host, _port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_ownerEmail, _ownerPassword);

            await smtp.SendAsync(emailMessage);
            await smtp.DisconnectAsync(true);

            return true;
        }
    }
}
