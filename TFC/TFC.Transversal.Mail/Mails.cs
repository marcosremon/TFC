using MailKit.Net.Smtp;
using MimeKit;

public class Mails
{
    private const string SmtpHost = "smtp.gmail.com";
    private const int SmtpPort = 587;
    private const string Email = "marcosremon2@gmail.com"; 
    private const string AppPassword = "lpqa srml fzsb bijh"; 

    public static void SendEmail(string recipientName, string recipientEmail, string newPassword)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Sistema de Recuperación De Contraseñas", Email));
        message.To.Add(new MailboxAddress(recipientName, recipientEmail));
        message.Subject = "Tu nueva contraseña";

        message.Body = new TextPart("plain")
        {
            Text = "Hola " + recipientName + "\n\nTu nueva contraseña es: " + newPassword
        };

        using (var client = new SmtpClient())
        {
            client.Timeout = 10000; 
            client.Connect(SmtpHost, SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
            client.Authenticate(Email, AppPassword);
            client.Send(message);
            client.Disconnect(true);
        }
    }
}