using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.Business.Concrete;
using uu_library_app.DataAccess.Concrete;
using uu_library_app.Entity.Concrete;

namespace uu_library_app.Core.Utils
{
    public static class MailSender
    {
        static SettingsManager settingsManager = new SettingsManager(new SettingsDal());
        static Settings settings = settingsManager.getSettings();

        public static void SendMail(string email, string verificationCode)
        {         
            try
            {
                Console.WriteLine("Method call at" + DateTime.Now);
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress(settings.SenderEmail);
                mail.To.Add(email);
                mail.Subject = "INIF-Assemsoft Kütüphane Yönetim Sistemi Kayıt İşlemi";
                mail.Body = "INIF-Assemsoft Kütüphane Yönetim Sistemi'ne Hoşgeldiniz!\nE-Posta doğrulama kodunuz: " + verificationCode;
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(settings.SenderEmail, StringEncoder.Decrypt(settings.SenderPassword));
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
        public static void SendMailForExpired(Student student, Book book, Author author, DepositBook depositBook, string pastDays)
        {
            DateTime dateShouldBeEscrow = depositBook.DepositDate.AddDays(6);

            try
            {
                Console.WriteLine("Method call at" + DateTime.Now);
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress(settings.SenderEmail);
                mail.To.Add(student.Email);
                mail.Subject = settings.ExpiredMailHeader;       
                mail.Body = "Emanet Bilgileri\n--------------\nKitap: "+book.BookName+ "\nYazar: " + author.FirstName +" "+ author.LastName +"\nEmanet Alma Tarihi: "+depositBook.DepositDate+"\nTeslim Edilmesi Gereken Tarih: "+dateShouldBeEscrow+"\nGeciken Gün: "+pastDays + "\n"+ settings.ExpiredMailText;
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(settings.SenderEmail, StringEncoder.Decrypt(settings.SenderPassword));
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
            Settings settings = settingsManager.getSettings();
            DateTime dateShouldBeEscrow = depositBook.DepositDate.AddDays(6);

            try
            {
                Console.WriteLine("Method call at" + DateTime.Now);
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress(settings.SenderEmail);
                mail.To.Add(student.Email);
                mail.Subject = settings.RemindingMailHeader;
                mail.Body = "Emanet Bilgileri\n--------------\nKitap: " + book.BookName + "\nYazar: " + author.FirstName + " " + author.LastName + "\nEmanet Alma Tarihi: " + depositBook.DepositDate + "\nTeslim Edilmesi Gereken Tarih: " + dateShouldBeEscrow + "\nGeciken Gün: " + pastDays + "\n" + settings.RemindingMailText;
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(settings.SenderEmail, settings.SenderPassword);
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
