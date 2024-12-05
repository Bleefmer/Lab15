using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace Lr15.Net.Services.Email
{
    public class EmailSenderService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailSenderService> _logger;

        public EmailSenderService(IConfiguration configuration, ILogger<EmailSenderService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var smtpSettings = _configuration.GetSection("SmtpSettings");

            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Your App", "mishvag2107@gmail.com"));
            emailMessage.To.Add(new MailboxAddress(to, to));
            emailMessage.Subject = subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = body };
            emailMessage.Body = bodyBuilder.ToMessageBody();

            try
            {
                using (var client = new SmtpClient())
                {
                    // Connect to the MailDev SMTP server on port 1025 (no encryption needed)
                    await client.ConnectAsync("127.0.0.1", int.Parse(smtpSettings["Port"]), SecureSocketOptions.None);


                    // Since MailDev doesn't require authentication, no credentials are needed
                    await client.SendAsync(emailMessage);

                    // Disconnect after sending the email
                    await client.DisconnectAsync(true);
                }
                _logger.LogInformation("Email sent successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error sending email: {ex.Message}");
            }
        }
    }
}
