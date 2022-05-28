using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.Entity.Concrete;

namespace uu_library_app.DataAccess.Abstract
{
    public interface IStudentDal
    {
        List<Student> getAll();
        List<Student> getAllSortedByName();
        List<Student> getAllSortedByAddedDate();
        void Add(Student student);
        void Update(Student student);
        void Delete(Student student);
        Student getById(string id);
        Student findByName(string name);
        Student findByNumber(string number);
        Student findByEmail(string email);
    }
}
