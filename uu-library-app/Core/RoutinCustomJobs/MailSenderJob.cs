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
                    if (daysPast == -3) //bu günü helperda static olarak tutalım. oradan düzenleriz sadece. veya gecikme süresi kısmı ui'da da seçtirebiliriz.
                    {
                        MailSender.SendMailReminding(student, book, depositBook);
                        Console.WriteLine("Hatırlatma maili " + student.FirstName + " kişisine gönderildi!");
                    }
                    if (daysPast < -6) //bu günü helperda static olarak tutalım. oradan düzenleriz sadece. veya gecikme süresi kısmı ui'da da seçtirebiliriz.
                    {
                        MailSender.SendMailForExpired(student, book, depositBook);
                        Console.WriteLine("Gecikme maili " + student.FirstName + " kişisine gönderildi!");
                    }

                }

            }
        }

    }
}
