using Application.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Infrastracture.Identity;
 
public class EmailSender : IEmailSender<ApplicationUser>
{
    private readonly MailSettings _mailSettings;
    public EmailSender(IOptions<MailSettings> options)
    {
        _mailSettings = options.Value;
    } 

    public async Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink)
    {
        string subject = "Confirm your TaskFlow Email";
        string message = $"Please confirm your account by clicking here: <a href='{confirmationLink}'>link</a>";

        await SendEmailAsync(email, subject, message);
    }

    public async Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink)
    {
        string subject = "Reset your TaskFlow Password";
        string message = $"Reset your password by clicking here: <a href='{resetLink}'>link</a>";

        await SendEmailAsync(email, subject, message);
    }

    public async Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode)
    {
        string subject = "Your TaskFlow Reset Code";
        string message = $"Your reset code is: {resetCode}";

        await SendEmailAsync(email, subject, message);
    }
     
    private async Task SendEmailAsync(string email, string subject, string message)
    { 
        using var mailMessage = new MailMessage();
        mailMessage.From = new MailAddress(_mailSettings.Mail);
        mailMessage.To.Add(email);
        mailMessage.Subject = subject;
        mailMessage.Body = message;
        mailMessage.IsBodyHtml = true;  
         
        using var client = new SmtpClient(_mailSettings.Host, _mailSettings.Port);
        client.Credentials = new NetworkCredential(_mailSettings.Mail, _mailSettings.Password);
        client.EnableSsl = true;  
         
        await client.SendMailAsync(mailMessage);
    }
}