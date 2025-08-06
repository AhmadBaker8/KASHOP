using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace KASHOP.PL.Utils
{
    public class EmailSetting : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("ahmadheeba10@gmail.com", "qimo chxt kqdj cxek")
            };
            return client.SendMailAsync(
                new MailMessage(
                from: "ahmadheeba10@gmail.com",
                to: email,
                subject,
                htmlMessage
                )
                { IsBodyHtml = true});
        }
    }
}
