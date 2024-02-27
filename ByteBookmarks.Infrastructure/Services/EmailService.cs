#region

using System.Net;
using System.Net.Mail;

#endregion

namespace ByteBookmarks.Infrastructure.Services;

public class EmailService(IConfiguration configuration) : IEmailService
{
    public async Task SendEmailAsync(string email, string subject, string message)
    {
        var smtpClient = new SmtpClient(configuration["SMTP:Host"])
        {
            Port = int.Parse(configuration["SMTP:Port"]),
            Credentials = new NetworkCredential(configuration["SMTP:Username"], configuration["SMTP:Password"]),
            EnableSsl = bool.Parse(configuration["SMTP:UseSSL"])
        };

        await smtpClient.SendMailAsync(configuration["SMTP:Sender"], email, subject, message);
    }

    public Task SendEmailConfirmationAsync(string email, string link)
    {
        throw new NotImplementedException();
    }

    public Task SendPasswordResetAsync(string email, string link)
    {
        throw new NotImplementedException();
    }

    public Task EmailShareBookmarkAsync(string email, string link)
    {
        throw new NotImplementedException();
    }
}