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
        IDepartmentService _department;
        public DepartmentManager(IDepartmentService department)
        {
            this._department = department;
        }

        public void Add(Department book)
        {
            _department.Add(book);
        }

        public void Delete(Department book)
        {
            _department.Delete(book);
        }

        public List<Department> getAll()
        {
            return _department.getAll();
        }

        public Book getById(string id)
        {
            return _department.getById(id);
        }

        public void Update(Department book)
        {
            _department.Update(book);
        }
    }
}
