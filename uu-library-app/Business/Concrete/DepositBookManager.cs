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
            throw new NotImplementedException();
        }

        public void Delete(DepositBook depositBook)
        {
            throw new NotImplementedException();
        }

        public List<DepositBook> getAll()
        {
            throw new NotImplementedException();
        }

        public DepositBook getById(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(DepositBook depositBook)
        {
            throw new NotImplementedException();
        }
    }
}
