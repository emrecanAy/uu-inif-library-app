using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.Entity.Concrete;

namespace uu_library_app.Business.Abstract
{
    public interface IDepartmentService
    {
        List<Department> getAll();
        void Add(Department department);
        void Update(Department department);
        void Delete(Department department);
    }
}
