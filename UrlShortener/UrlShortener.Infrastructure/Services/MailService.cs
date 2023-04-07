using Microsoft.Extensions.Configuration;
using SendGrid.Helpers.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace UrlShortener.Infrastructure.Services
{
    public class SendGridMailService : IEmailSender
    {
        private IConfiguration _configuration;

        public SendGridMailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string content)
        {
            //var apiKey = _configuration["SendGridAPIKey"];
            //var client = new SendGrid.SendGridClient(apiKey);
            //var from = new EmailAddress("elziadna10@gmail.com", "JWT Auth Demo");
            //var to = new EmailAddress(toEmail);
            //var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
            //var response = await client.SendEmailAsync(msg);

            // Specify the SMTP server and port number
            var smtpServer = "smtp.gmail.com";
            var portNumber = 587;

            // Create a new instance of the SmtpClient class
            var smtpClient = new SmtpClient(smtpServer, portNumber);

            // Specify your email credentials
            //TODO :: Hide the password
            //Remove once the its been tested
            smtpClient.Credentials = new NetworkCredential(_configuration["SMTP_FROM_MAIL"], _configuration["SMTP_PASS"]);

            // Enable SSL encryption
            smtpClient.EnableSsl = true;

            // Create a new instance of the MailMessage class
            var message = new MailMessage();

            // Specify the sender and recipient email addresses
            message.From = new MailAddress(_configuration["SMTP_FROM_MAIL"]);
            message.To.Add(toEmail);

            // Specify the subject and body of the email
            message.Subject = subject;
            message.Body = content;

            // Send the email
            smtpClient.Send(message);
        }
    }
}
