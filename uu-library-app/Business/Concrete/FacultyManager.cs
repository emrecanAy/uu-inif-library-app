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
    public class FacultyManager : IFacultyService
    {
        IFacultyDal _service;
        public FacultyManager(IFacultyDal service)
        {
            _service = service;
        }

        public void Add(Faculty faculty)
        {
            _service.Add(faculty);
        }

        public void Delete(Faculty faculty)
        {
            if (checkIfExistInStudents(faculty.Id))
            {
                throw new Exception("Bu fakülte; öğrencilerde bulunan bir öğrenciye veya öğrencilere ait olduğu için öncelikle öğrencilere giderek bu fakülteye ait olan öğrenciyi veya öğrencileri silmeniz gerekmektedir!");
            }
            else
            {
                _service.Delete(faculty);
            }

  
        }

        public Faculty FindById(string id)
        {
            return _service.FindById(id);
        }

        public List<Faculty> GetAll()
        {
            return this._service.GetAll();
        }

        public void Update(Faculty faculty)
        {
            _service.Update(faculty);
        }

        bool checkIfExistInStudents(string facultyId)
        {
            StudentManager studentManager = new StudentManager(new StudentDal());
            if (studentManager.GetByFacultyId(facultyId) != null)
            {
                return true;
            }
            return false;
        }

       
     
       

        
    }
}
