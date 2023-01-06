using com.itransition.final.Models;

namespace com.itransition.final.Services;

public interface IEmailSenderService
{
    Task SendEmailAsync(EmailConfirmationMessage emailConfirmationMessage);
}