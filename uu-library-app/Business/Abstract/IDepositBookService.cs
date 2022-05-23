using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.Entity.Concrete;

namespace uu_library_app.Business.Abstract
{
    public interface IDepositBookService 
    {
        List<DepositBook> getAll();
        List<DepositBook> getAllUndeposited();
        List<DepositBook> getAllDeposited();
        void Add(DepositBook depositBook);
        void Update(DepositBook depositBook);
        void Delete(string id);
        void depositBook(string id);
        List<DepositBook> findAllByStudentId(string studentId);

    }
}
