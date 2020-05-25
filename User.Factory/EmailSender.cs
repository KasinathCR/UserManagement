namespace User.Helpers
{
    using System;
    using System.Net;
    using System.Net.Mail;

    public static class EmailSender
    {
        public static void SendEmail(string fromAddress, string toAddress, string password, string message)
        {
            try
            {
                var mailMessage = new MailMessage();
                var smtp = new SmtpClient();
                mailMessage.From = new MailAddress(fromAddress);
                mailMessage.To.Add(new MailAddress(toAddress));
                mailMessage.Subject = "Test";
                mailMessage.IsBodyHtml = true; //to make message body as html  
                mailMessage.Body = message;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(fromAddress, password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(mailMessage);
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
