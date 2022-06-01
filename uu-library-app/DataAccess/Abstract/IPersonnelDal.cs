using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.Entity.Concrete;

namespace uu_library_app.DataAccess.Abstract
{
    public interface IPersonnelDal
    {
        List<Personnel> getAll();
        void Add(Personnel personnel);
        void Update(Personnel personnel);
        void Delete(Personnel personnel);
        Personnel getById(string id);
        Personnel findByName(string name);
        Personnel findByEmail(string email);
    }
}
