using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.Entity.Concrete;

namespace uu_library_app.Core.Utils
{
    public static class MailSender
    {
        public static void SendMailForExpired(Student student, Book book, Author author, DepositBook depositBook, string pastDays)
        {

            string senderMailAddress = "inif.assemsoft@gmail.com"; //bunlar globals diye klasör açılıp oradan getirilecek. UI'dan set edilebilecek.
            DateTime dateShouldBeEscrow = depositBook.DepositDate.AddDays(6);

            try
            {
                Console.WriteLine("Method call at" + DateTime.Now);
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress(senderMailAddress);
                mail.To.Add(student.Email);
                mail.Subject = "INIF-AssemSoft Kitap Teslim Süresi";
                //TO-DO: Teslim edilmesi gereken tarih dinamik olarak hesaplanacak.
                //Emanet Bilgileri: Kitap: book.BookName, Yazar: NFK, Emanet , + richre
                //mail.Body = "" + depositBook.DepositDate + " tarihinde teslim almış olduğunuz " + book.BookName + " kitabını teslim etmeniz gereken " + dateShouldBeEscrow + " tarihinde teslim etmediğiniz için bu bilgilendirme postasını alıyorsunuz.";
                mail.Body = "Emanet Bilgileri\n--------------\nKitap: "+book.BookName+ "\nYazar: " + author.FirstName +" "+ author.LastName +"\nEmanet Alma Tarihi: "+depositBook.DepositDate+"\nTeslim Edilmesi Gereken Tarih: "+depositBook.DateShouldBeEscrow+"\nGeciken Gün: ";
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(senderMailAddress, "PassWord_123"); //bunlar globals diye klasör açılıp orada bir class'da tutulup oradan getirilecek.
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

        public static void SendMailReminding(Student student, Book book, Author author, DepositBook depositBook, string pastDays)
        {
            DateTime date1 = new DateTime(0, 0, 0);
            string senderMailAddress = "inif.assemsoft@gmail.com"; //bunlar globals diye klasör açılıp oradan getirilecek. UI'dan set edilebilecek.
            DateTime dateShouldBeEscrow = depositBook.DepositDate.AddDays(6);

            try
            {
                Console.WriteLine("Method call at" + DateTime.Now);
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress(senderMailAddress);
                mail.To.Add(student.Email);
                mail.Subject = "INIF-AssemSoft Kitap Teslim Süresi";
                //TO-DO: Teslim edilmesi gereken tarih dinamik olarak hesaplanacak.
                mail.Body = "" + depositBook.DepositDate + " tarihinde teslim almış olduğunuz " + book.BookName + " kitabını teslim etmeniz gereken " + dateShouldBeEscrow + " tarihinde teslim etmediğiniz için bu bilgilendirme postasını alıyorsunuz.";
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(senderMailAddress, "PassWord_123"); //bunlar globals diye klasör açılıp orada bir class'da tutulup oradan getirilecek.
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
