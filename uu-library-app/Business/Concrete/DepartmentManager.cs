using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.Business.Abstract;
using uu_library_app.DataAccess.Abstract;
using uu_library_app.DataAccess.Concrete;
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

        public void Delete(Department department)
        {
            if (checkIfExistInStudents(department.Id))
            {
                throw new Exception("Bu bölüm; öğrencilerde bulunan bir öğrenciye veya öğrencilere ait olduğu için öncelikle öğrencilere giderek bu bölüme ait olan öğrenciyi veya öğrencileri silmeniz gerekmektedir!");
            }
            {
                _department.Delete(department);
            }       
        }

        public Department FindById(string id)
        {
            return _department.FindById(id);
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

        public bool checkIfExistInStudents(string departmentId)
        {
            StudentManager studentManager = new StudentManager(new StudentDal());
            if (studentManager.GetByDepartmentId(departmentId) != null)
            {
                return true;
            }
            return false;
        }
    }
}
