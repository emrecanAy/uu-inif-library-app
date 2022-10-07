using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.Entity.Abstract;

namespace uu_library_app.Entity.Concrete
{
    public class Student : IBaseModelWithIdAndDate
    {
        private string departmentId;
        private string firstName;
        private string lastName;
        private string number;
        private string card;
        private string email;
        private string facultyId;
        // TODO: Add phone number field to call the students.

        public Student() { }
        public Student(string id, string departmentId, string facultyId, string firstName, string lastName, string number, string card, string email)
        {
            this.Id = id;
            this.DepartmentId = departmentId;
            this.FacultyId = facultyId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Number = number;
            this.Card = card;
            this.Email = email;
        }

        public string Id { get => id; set => id = value; }
        public string DepartmentId { get => departmentId; set => departmentId = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Number { get => number; set => number = value; }
        public string Card { get => card; set => card = value; }
        public string Email { get => email; set => email = value; }
        public DateTime CreatedAt { get => createdAt; set => createdAt = value; }
        public bool Deleted { get => deleted; set => deleted = value; }
        public string FacultyId { get => facultyId; set => facultyId = value; }
    }
}
