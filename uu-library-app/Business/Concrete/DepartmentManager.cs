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
    public class DepartmentManager:IDepartmentService
    {
        IDepartmentDal _department;
        public DepartmentManager(IDepartmentDal department)
        {
            this._department = department;
        }

        public void Add(Department department)
        {
            if(department.Name != null)
            {
                _department.Add(department);
            }
        }

        public void Delete(string id)
        {
            if(id != null)
            {
                _department.Delete(id);
            }       
        }

        public List<Department> getAll()
        {
            return _department.getAll();
        }

        public void Update(Department department)
        {
            if(department.Name != null || department.Name.Length > 0)
            {
                _department.Update(department);
            }
            
        }
    }
}
