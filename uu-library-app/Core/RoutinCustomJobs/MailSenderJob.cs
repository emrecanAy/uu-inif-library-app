using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.Business.Concrete;
using uu_library_app.Core.Utils;
using uu_library_app.DataAccess.Concrete;
using uu_library_app.Entity.Concrete;

namespace uu_library_app.Core.RoutinCustomJobs
{
    public class MailSenderJob
    {
        DepositBookManager depositBookManager = new DepositBookManager(new DepositBookDal());
        BookManager bookManager = new BookManager(new BookDal());
        StudentManager studentManager = new StudentManager(new StudentDal());
        AuthorManager authorManager = new AuthorManager(new AuthorDal());
        SettingsManager settingsManager = new SettingsManager(new SettingsDal());
        public void everyDayCheckAndSendMail()
        {
            List<DepositBook> depositBooksList = depositBookManager.getAll();
            foreach (DepositBook depositBook in depositBooksList)
            {
                TimeSpan ts = depositBook.DepositDate - DateTime.Now;
                string howManyDaysPast = ts.Days.ToString();
                if (howManyDaysPast.StartsWith("-"))
                {
                    int daysPast = Convert.ToInt32(howManyDaysPast);
                    Student student = studentManager.getById(depositBook.StudentId);
                    Book book = bookManager.getById(depositBook.BookId);
                    Author author = authorManager.getById(book.AuthorId);
                    string pastDays = daysPast.ToString();

                    if (daysPast == -settingsManager.getSettings().RemindingDay)
                    {
                        pastDays = pastDays.Substring(1);
                        MailSender.SendMailReminding(student, book, author, depositBook, pastDays);
                        Console.WriteLine("Hatırlatma maili " + student.FirstName + " kişisine gönderildi!");
                    }
                    if (daysPast < -settingsManager.getSettings().DepositDay)
                    {
                        MailSender.SendMailForExpired(student, book, author, depositBook, pastDays);
                        Console.WriteLine("Gecikme maili " + student.FirstName + " kişisine gönderildi!");
                    }

                }

            }
        }

    }
}
