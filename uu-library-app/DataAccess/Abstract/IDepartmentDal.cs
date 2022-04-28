using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.Entity.Concrete;

namespace uu_library_app.DataAccess.Abstract
{
    interface IDepartmentDal
    {
        List<Department> getAll();
        void Add(Department book);
        void Update(Department book);
        void Delete(Department book);
        Book getById(string id);
    }
}
