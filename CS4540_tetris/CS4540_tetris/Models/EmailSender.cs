using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CS4540_tetris.Models
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var task = new Task(() =>
            {
                SmtpClient smtpClient = new SmtpClient("smtp.utah.edu",
               25);
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("u0829734@eng.utah.edu", "PLAY TETRIS");
                mail.To.Add(new MailAddress(email));
                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                smtpClient.Send(mail);
            });
            task.Start();
            return task;
        }
    }
}
