using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uu_library_app.Entity.Concrete.DTO
{
    public class StudentDto
    {
        private string id;
        private string fullName;
        private string number;
        private string departmentName;
        private DateTime dateNeedToGet;

        public StudentDto() { }
        public StudentDto(string id, string fullName, string number, string departmentName, DateTime dateNeedToGet)
        {
            this.Id = id;
            this.FullName = fullName;
            this.Number = number;
            this.DepartmentName = departmentName;
            this.DateNeedToGet = dateNeedToGet;
        }

        public string Id { get => id; set => id = value; }
        public string FullName { get => fullName; set => fullName = value; }
        public string Number { get => number; set => number = value; }
        public string DepartmentName { get => departmentName; set => departmentName = value; }
        public DateTime DateNeedToGet { get => dateNeedToGet; set => dateNeedToGet = value; }
    }
}
