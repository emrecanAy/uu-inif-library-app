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
    public class StudentManager : IStudentService
    {
        IStudentDal _service;

        public StudentManager(IStudentDal service)
        {
            _service = service;
        }

        public void Add(Student student)
        {
            if (student.FirstName != null || student.LastName != null || student.Email != null || student.Number != null || student.Card != null || student.Id != null)
            {
                _service.Add(student);
            }

        }

        public void Delete(Student student)
        {
            _service.Delete(student);
        }

        public List<Student> getAll()
        {
            return _service.getAll();
        }

        public Student getById(string id)
        {
            return _service.getById(id);
        }

        public void Update(Student student)
        {
            _service.Update(student);
        }
    }
}
