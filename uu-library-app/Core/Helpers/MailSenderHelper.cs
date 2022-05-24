using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace uu_library_app.Core.Helpers
{
    public static class MailSenderHelper
    {
        public static void SendMail(string receiverMailAddress)
        {

            string senderMailAddress = "inif.assemsoft@gmail.com";

            try
            {
                Console.WriteLine("Method call at" + DateTime.Now);
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress(senderMailAddress);
                mail.To.Add(receiverMailAddress);
                mail.Subject = "INIF-AssemSoft Kitap Teslim Süresi";
                mail.Body = "1 Aralık 2022 tarihinde teslim almış olduğunuz Taaşşuk-u Talat ve Fitnat(Şemsettin Sami) kitabını teslim etmeniz gereken 13 Aralık 2022 tarihinde teslim etmediğiniz için bu bilgilendirme postasını alıyorsunuz.";
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(senderMailAddress, "PassWord_123");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
                Console.WriteLine("Mail sent at" + DateTime.Now);
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
