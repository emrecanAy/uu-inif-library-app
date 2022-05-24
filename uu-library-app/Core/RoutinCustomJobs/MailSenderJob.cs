using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.Business.Concrete;
using uu_library_app.DataAccess.Concrete;
using uu_library_app.Entity.Concrete;

namespace uu_library_app.Core.RoutinCustomJobs
{
    public class MailSenderJob
    {
        DepositBookManager depositBookManager = new DepositBookManager(new DepositBookDal());
        StudentManager studentManager = new StudentManager(new StudentDal());
        public void everyDayCheckAndSendMail()
        {
            List<DepositBook> depositBooksList = depositBookManager.getAll();
            foreach(DepositBook depositBook in depositBooksList)
            {
                DateTime depositDate = depositBook.DepositDate;

                TimeSpan ts = DateTime.Now - depositDate;
                string howManyDaysPast = ts.Days.ToString();

                if (howManyDaysPast.StartsWith("-"))
                {
                    int daysPast = Convert.ToInt32(howManyDaysPast);
                    Student student = studentManager.getById(depositBook.StudentId);
                    if(daysPast < -15) //bu günü helperda static olarak tutalım. oradan düzenleriz sadece. veya gecikme süresi kısmı ui'da da seçtirebiliriz.
                    {
                        
                    }

                }
             
            }
        }

    }
}
