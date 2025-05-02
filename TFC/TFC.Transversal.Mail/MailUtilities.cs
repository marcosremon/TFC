using System.Net.Mail;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

public static class MailUtilities
{
    private static IConfiguration? _configuration;

    public static void Initialize(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public static void SendEmail(string recipientName, string recipientEmail, string newPassword)
    {
        var emailSettings = _configuration.GetSection("EmailSettings");
        string? smtpHost = emailSettings["SmtpHost"];
        int smtpPort = int.Parse(emailSettings["SmtpPort"]); 
        string? senderEmail = emailSettings["SenderEmail"];
        string? appPassword = emailSettings["AppPassword"];
        string? senderName = emailSettings["SenderName"];

        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(senderName, senderEmail));
        message.To.Add(new MailboxAddress(recipientName, recipientEmail));
        message.Subject = "Tu nueva contraseña";

        message.Body = new TextPart("plain")
        {
            Text = $"Hola {recipientName},\n\nTu nueva contraseña es: {newPassword}\n\n" +
                   "Por seguridad, te recomendamos cambiar esta contraseña después de iniciar sesión."
        };

        using (var client = new SmtpClient())
        {
            client.Timeout = 10000;
            client.Connect(smtpHost, smtpPort, MailKit.Security.SecureSocketOptions.StartTls);
            client.Authenticate(senderEmail, appPassword);
            client.Send(message);
            client.Disconnect(true);
        }
    }

    public static bool IsEmailValid(string email)
    {
        try
        {
            MailAddress m = new MailAddress(email);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
}