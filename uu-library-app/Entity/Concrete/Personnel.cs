using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uu_library_app.Entity.Concrete
{
    public class Personnel
    {
        private string id;
        private string facultyId;
        private string departmentId;
        private string firstName;
        private string lastName;
        private string email;
        private DateTime createdAt;
        private bool deleted;

        public Personnel() { }
        public Personnel(string id, string facultyId, string departmentId, string firstName, string lastName, string email)
        {
            this.Id = id;
            this.FacultyId = facultyId;
            this.DepartmentId = departmentId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
        }

        public string Id { get => id; set => id = value; }
        public string FacultyId { get => facultyId; set => facultyId = value; }
        public string DepartmentId { get => departmentId; set => departmentId = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Email { get => email; set => email = value; }
        public DateTime CreatedAt { get => createdAt; set => createdAt = value; }
        public bool Deleted { get => deleted; set => deleted = value; }
    }
}
