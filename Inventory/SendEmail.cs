using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
//using System.Web.Mail;
using System.Text;
using System.Threading.Tasks;


namespace Inventory
{
    public class SendEmail
    {
        public void EmailAvtivation(string txtto, string txtmessage, string subj)
        {
            MailMessage Msg = new MailMessage();
            Msg.From = new MailAddress("ankinventorytest@gmail.com");
            Msg.To.Add(txtto);
            //ExbDetails ex = new ExbDetails();
            Msg.Body = txtmessage;
            Msg.Subject = subj;
            Msg.IsBodyHtml = true;
            // your remote SMTP server IP.
            SmtpClient smtp = new SmtpClient();
            smtp.Host = ("SMTP.GMAIL.com").ToString();
            System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
            NetworkCred.UserName = ("ankinventorytest@gmail.com").ToString();
            NetworkCred.Password = ("ankinventorytest123").ToString();
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Send(Msg);
        }
    }
}