using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.AspNetCore.Http;
using Publi4.Models;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Publi4.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailMessageSender : IEmailSender
    {
        public EmailMessageSender(IOptions<EmailMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public EmailMessageSenderOptions Options { get; } //set only via Secret Manager
        public Task SendEmailAsync(string email, string subject, string message)
        {
            // Plug in your email service here to send an email.
            return Execute(Options.SendGridKey, subject, message, email);
        }

        public Task SendEmailAsync(string email, string subject, string message, List<Models.AttachmentDto> attachments)
        {
            // Plug in your email service here to send an email.
            return Execute(Options.SendGridKey, subject, message, email, attachments);
        }

        public Task SendSmsAsync(string number, string message)
        {
            throw new NotImplementedException();
        }

        private Task Execute(string apiKey, string subject, string message, string email, List<Models.AttachmentDto> attachments = null)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(Options.EmailAdmin, Options.NomeAdmin),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message                
            };



            if (attachments != null)
            {
                foreach (var attachment in attachments)
                {
                    //msg.AddAttachment("test.pdf", attachment.Content.ToString(), "pdf");
                    msg.AddAttachment(attachment.FileName, Convert.ToBase64String(attachment.Content), attachment.ContentType);
                }
            }

            msg.AddTo(new EmailAddress(email));

            return client.SendEmailAsync(msg);
        }

    }


    public class AuthMessageSenderDummy : IEmailSender
    {
        public AuthMessageSenderDummy(IOptions<EmailMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public EmailMessageSenderOptions Options { get; } //set only via Secret Manager
        public Task SendEmailAsync(string email, string subject, string message)
        {
            // Plug in your email service here to send an email.
            return Execute(Options.SendGridKey, subject, message, email);
        }

        public Task Execute(string apiKey, string subject, string message, string email, List<Models.AttachmentDto> attachments = null)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(Options.EmailAdmin, Options.NomeAdmin),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));

            if (attachments != null)
            {
                foreach (var attachment in attachments)
                {
                    msg.AddAttachment(attachment.FileName, attachment.ToString(), attachment.ContentType);
                }
            }

            return Task.Run(() =>
            {
                Debug.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(msg, Formatting.Indented));
                Debug.WriteLine($"\r\nEMAIL RESET LINK FOR {email}\r\ndata:text/html;charset=utf-8," + message + "\r\n");
            });
        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }

        public Task SendEmailAsync(string email, string subject, string message, List<Models.AttachmentDto> attachments)
        {
            return Execute(Options.SendGridKey, subject, message, email, attachments);
        }

        public void SendEmail(string email, string subject, string message, List<AttachmentDto> attachments)
        {
            throw new NotImplementedException();
        }
    }

}
