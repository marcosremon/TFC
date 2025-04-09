using System.Net.Mail;
using System.Net;

namespace TFC.Transversal.Mail
{
    public class Mails
    {
        // To_do: Cambiar el host, port, userName y password por los de producción.


        // To_do: mover al properties
        public static string host = "mail.kintech-engineering.com";
        public static int port = 25;
        public static string userName = "web@kintech-engineering.com";
        public static string password = "nlzedssaJrHo";
        public static string from = "web@kintech-engineering.com";

        public static void SendEmail(string username, string userEmail, string newPassword)
        {
            // No se emplea try/catch ya que esta previsto usar el try/catch de la clase donde se usa.
            MailMessage _mailMessage = new MailMessage();
            _mailMessage.From = new MailAddress(from);
            _mailMessage.To.Add(userEmail);
            _mailMessage.Subject = "Kintech Atlas new password";
            _mailMessage.Body = "Dear Atlas User,\n" +
                                "Here is your new password: " + newPassword + "\n" +
                                "If you need any further help, please contact our technical support.\n" +
                                "Kind regards\n" +
                                "Kintech Engineering";


            SmtpClient smtpClient = new SmtpClient(host, port);
            smtpClient.Credentials = new NetworkCredential(userName, password);
            smtpClient.EnableSsl = false;
            smtpClient.Send(_mailMessage);
        }
    }
}