using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.Entity.Concrete;

namespace uu_library_app.Business.Abstract
{
    public interface IAdminService
    {
        List<Admin> getAll();
        void Add(Admin admin, string verificationCode);
        void Update(Admin admin);
        void Delete(Admin admin);
        Admin getbyEmail(string eMail);
        Admin GetById(string id);
    }
}
