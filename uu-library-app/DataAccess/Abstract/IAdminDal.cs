using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.Entity.Concrete;

namespace uu_library_app.DataAccess.Abstract
{
    public interface IAdminDal
    {
        List<Admin> getAll();
        void Add(Admin department);
        void Update(Admin department);
        void Delete(string id);
    }
}
