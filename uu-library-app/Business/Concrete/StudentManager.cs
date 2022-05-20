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

        public void Delete(string id)
        {
            if(id != null)
            {
                _service.Delete(id);
            }
        }

        public Student findByName(string name)
        {
            if(name == null)
            {
                return null;
            }
            return _service.findByName(name);
        }

        public Student findByNumber(string number)
        {
            if(number == null)
            {
                return null;
            }
            return _service.findByNumber(number);
        }

        public List<Student> getAll()
        {
            return _service.getAll();
        }

        public List<Student> getAllSortedByAddedDate()
        {
            return _service.getAllSortedByAddedDate();
        }

        public List<Student> getAllSortedByName()
        {
            return _service.getAllSortedByName();
        }

        public Student getById(string id)
        {
            if(id == null)
            {
                return null;
            }
            return _service.getById(id);
        }

        public void Update(Student student)
        {
            if (student.FirstName != null || student.LastName != null || student.Email != null || student.Number != null || student.Card != null || student.Id != null)
            {
                _service.Update(student);
            }
        }
    }
}
