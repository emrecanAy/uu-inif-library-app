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
    public class AdminManager : IAdminService
    {
        IAdminDal _admin;
        public AdminManager(IAdminDal admin)
        {
            _admin = admin;
        }

        public void Add(Admin admin)
        {
            if (admin.FirstName != null || admin.LastName != null || admin.EMail != null || admin.Password != null || admin.Id != null)
            {
                _admin.Add(admin);
            }
        }

        public void Delete(string id)
        {
            if (id != null)
            {
                _admin.Delete(id);
            }
        }

        public List<Admin> getAll()
        {
            return _admin.getAll();
        }

        public void Update(Admin admin)
        {
            if (admin.FirstName != null || admin.LastName != null || admin.EMail != null || admin.Password != null || admin.Id != null)
            {
                _admin.Update(admin);
            }
        }
    }
}
