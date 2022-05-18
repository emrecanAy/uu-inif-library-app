using System;
using System.Collections.Generic;
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
        IDepositBookService _depositBook;
        public DepositBookManager(IDepositBookService depositBook)
        {
            this._depositBook = depositBook;
        }



        public void Add(DepositBook depositBook)
        {
            _depositBook.Add(depositBook);
        }

        public void Delete(DepositBook depositBook)
        {
            _depositBook.Delete(depositBook);
        }

        public List<DepositBook> getAll()
        {
            return _depositBook.getAll();
        }

        public DepositBook getById(string id)
        {
            return _depositBook.getById(id);
        }

        public void Update(DepositBook depositBook)
        {
            _depositBook.Update(depositBook);
        }
    }
}
