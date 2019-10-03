using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Services
{
    public class EmailSender:IEmailSender
    {
        Secrets _appsecrets;
        public EmailSender(Secrets AppSecret)
        {
            _appsecrets = AppSecret;
        }


        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(_appsecrets.SendGridKey, subject, message, email);
        }

        public Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                // should be a domain other than yahoo.com, outlook.com, hotmail.com, gmail.com
                From = new EmailAddress("donotreply@somewhere.com", "IndieGamesLab"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));
            return client.SendEmailAsync(msg);
        }




    }
}
