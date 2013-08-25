using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace Utils
{
    public class Emailer
    {
        public static void SendEmail(string username, string emailAddress, string content, string subject)
        {
            MailMessage mail = new MailMessage();
            using (SmtpClient SmtpServer = new SmtpClient())
            {

                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.EnableSsl = true;
                SmtpServer.Host = "smtp.gmail.com";
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new NetworkCredential("spotichelas@gmail.com", "pleasedontstealme");
                // send the email

                mail.From = new MailAddress("spotichelas@gmail.com");
                mail.To.Add(emailAddress);
                mail.Subject = subject;
                mail.Body = content;

                SmtpServer.Send(mail);
            }
        }
    }
}
