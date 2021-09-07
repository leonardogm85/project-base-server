using Microsoft.Extensions.Options;
using ProjetoBase.Infrastructure.CrossCutting.Common.Interfaces;
using ProjetoBase.Infrastructure.CrossCutting.Common.Settings;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ProjetoBase.Infrastructure.CrossCutting.Common.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSenderSettings _emailSenderSettings;

        public EmailService(IOptions<EmailSenderSettings> options) => _emailSenderSettings = options.Value;

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            using var smtpClient = new SmtpClient(_emailSenderSettings.Host, _emailSenderSettings.Port)
            {
                EnableSsl = _emailSenderSettings.EnableSsl,
                Credentials = new NetworkCredential(_emailSenderSettings.UserName, _emailSenderSettings.Password)
            };
            using var mailMessage = new MailMessage(new MailAddress(_emailSenderSettings.UserName, _emailSenderSettings.Name), new MailAddress(email))
            {
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };
            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
