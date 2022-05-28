using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public bool isValidRegex(string email) //Utils'e koy.
        {
            //Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            //Regex regex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.+-]+\.edu$");
            Regex regex = new Regex(@"/\.(?:uludag|edu|tr)|@ogr/");
            Match match = regex.Match(email);
            if (match.Success)
                return true;
            else
                return false;
        }

        public bool isValidSchoolMail(string email) //Utils'e koy.
        {
            string ending = email.Substring(email.IndexOf("@") + 1);
            if (ending != "ogr.uludag.edu.tr")
            {
                return false;
            }
            return true;

        }

        public bool checkIfEmailExist(string email) //email'lere regex eklenecek.
        {
            if (_service.findByEmail(email) != null)
            {
                return true;
            }
            return false;
        }

        public Student findByEmail(string email)
        {
            return _service.findByEmail(email);
        }
    }
}
