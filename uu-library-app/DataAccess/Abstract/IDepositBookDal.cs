using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.Entity.Concrete;

namespace uu_library_app.DataAccess.Abstract
{
    public interface IDepositBookDal
    {
        List<DepositBook> getAll();
        void Add(DepositBook depositBook);
        void Update(DepositBook depositBook);
        void Delete(DepositBook depositBook);
       
    }
}
