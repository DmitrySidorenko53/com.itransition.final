using System.Net.Mail;
using com.itransition.final.Models;
using MimeKit;

namespace com.itransition.final.Services.Impl;

public class EmailSenderService : IEmailSenderService
{
    public async Task SendEmailAsync(EmailConfirmationMessage emailConfirmationMessage)
    {
        var message = new MimeMessage
        {
            From = { new MailboxAddress("Administration", "admin@gmail.com") },
            To = { new MailboxAddress("", emailConfirmationMessage.EmailTo) },
            Subject = emailConfirmationMessage.Subject,
            Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = emailConfirmationMessage.Message
            }
        };
    }
}