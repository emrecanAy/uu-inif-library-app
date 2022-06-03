using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.Business.Abstract;
using uu_library_app.DataAccess.Abstract;
using uu_library_app.Entity.Concrete;
namespace uu_library_app.Business.Concrete
{
    internal class DepositBookManager : IDepositBookService
    {
        IDepositBookDal _depositBook;
        public DepositBookManager(IDepositBookDal depositBook)
        {
            this._depositBook = depositBook;
        }
        public void Add(DepositBook depositBook)
        {
            if (getByStudentIdAndBookId(depositBook).Id != null)
            {
                throw new Exception("Aynı kitap aynı öğrenciye daha önce ödünç verilmiştir. Öncelikle almış olduğu kitabı teslim etmesi gerekmektedir.");
            }
            _depositBook.Add(depositBook);

        }
        public DepositBook findById(string id)
        {
            return _depositBook.findById(id);
        }

        public void Delete(DepositBook depositBook)
        {
            _depositBook.Delete(depositBook);
        }

        public void depositBook(string id)
        {
            if (id != "")
            {
                _depositBook.depositBook(id);
            }
        }

        public List<DepositBook> findAllByStudentId(string studentId)
        {
            return _depositBook.findAllByStudentId(studentId);
        }

        public List<DepositBook> getAll()
        {
            return _depositBook.getAll();
        }

        public List<DepositBook> getAllDeposited()
        {
            return _depositBook.getAllDeposited();
        }

        public List<DepositBook> getAllUndeposited()
        {
            return _depositBook.getAllUndeposited();
        }

        public void Update(DepositBook depositBook)
        {
            _depositBook.Update(depositBook);
        }

        public DepositBook getByBookId(string bookId)
        {
            return _depositBook.getByBookId(bookId);
        }

        public DepositBook getByStudentId(string studentId)
        {
            return _depositBook.getByStudentId(studentId);
        }

        public DepositBook getByStudentIdAndBookId(DepositBook depositBook)
        {
            return _depositBook.getByStudentIdAndBookId(depositBook);
        }
    }
}
