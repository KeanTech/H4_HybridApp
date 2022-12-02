using BlazorBoard_Api.DataAccess;
using System.Net;
using System.Net.Mail;

namespace BlazorBoard_Api.Services
{
    public class MailService
    {
        private readonly BlazorBoardDB _db;

        public MailService(BlazorBoardDB db) 
        {
            _db = db;
        }

        private SmtpClient GetSmtpClient(User user) 
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(user.Contact, user.Info),
                EnableSsl = true,
            };

            return smtpClient;
        }

        public void SendMail(string message) 
        {
            var user = _db.Users.First();
            var smtp = GetSmtpClient(user);

            var MailMessage = new MailMessage()
            {
                From = new MailAddress(user.Contact),
                Subject = "Notification",
                Body = $"{message}",
                IsBodyHtml= false,
            };

            MailMessage.To.Add(new MailAddress("kenethanders@gmail.com"));
        
            smtp.Send(MailMessage);
        }
    }
}
