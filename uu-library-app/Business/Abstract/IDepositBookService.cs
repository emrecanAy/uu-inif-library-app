using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.Entity.Concrete;

namespace uu_library_app.Business.Abstract
{
    public interface IDepositBookService 
    {
        List<DepositBook> getAll();
        void Add(DepositBook depositBook);
        void Update(DepositBook depositBook);
        void Delete(DepositBook depositBook);

    }
}
