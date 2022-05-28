using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.Business.Abstract;
using uu_library_app.Core.Utils;
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

        public AdminManager()
        {
        }

        public bool checkIfEmailEqualsToPassword(string eMail, string password)
        {
            Admin admin = _admin.getByEmail(eMail);
            if (StringEncoder.Decrypt(admin.Password) != password)
            {
                return false;
            }
            return true;

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

        public Admin getbyEmail(string eMail)
        {
            
            return _admin.getByEmail(eMail);
        }

        public void Update(Admin admin)
        {
            if (admin.FirstName != null || admin.LastName != null || admin.EMail != null || admin.Password != null || admin.Id != null)
            {
                _admin.Update(admin);
            }
        }

        public Admin GetById(string id)
        {
            return _admin.GetById(id);
        }
    }
}
