namespace ByteBookmarks.Core.Interfaces;

public interface IEmailService
{
    Task SendEmailAsync(string email, string subject, string message);
    Task SendEmailConfirmationAsync(string email, string link);
    Task SendPasswordResetAsync(string email, string link);
    Task EmailShareBookmarkAsync(string email, string link);
    
}