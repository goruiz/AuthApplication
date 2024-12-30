using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace AuthApplication.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Sends an email using SMTP configuration
        public void SendEmail(string to, string subject, string body)
        {
            var smtpConfig = _configuration.GetSection("Smtp");

            // Create and configure the SMTP client
            var smtpClient = new SmtpClient
            {
                Host = smtpConfig["Host"],
                Port = int.Parse(smtpConfig["Port"]),
                EnableSsl = bool.Parse(smtpConfig["EnableSsl"]),
                Credentials = new NetworkCredential(
                    smtpConfig["Username"],
                    smtpConfig["Password"]
                )
            };

            // Create the email message
            var mailMessage = new MailMessage
            {
                From = new MailAddress(smtpConfig["Username"], "AuthApplication"),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(to);

            smtpClient.Send(mailMessage);
        }
    }
}
