using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.Entity.Concrete;

namespace uu_library_app.Business.Abstract
{
    public interface IStudentService
    {
        List<Student> getAll();
        List<Student> getAllSortedByName();
        List<Student> getAllSortedByAddedDate();
        void Add(Student student);
        void Update(Student student);
        void Delete(string id);
        Student getById(string id);
        Student findByName(string name);
        Student findByNumber(string number);
    }
}
