using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uu_library_app.Entity.Concrete
{
    public class Department
    {
        private string id;
        private string name;
        private DateTime createdAt;
        private bool deleted;
        public Department() { }
        public Department(string id, string name)
        {
            this.id = id;
            this.name = name;
        }
        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public DateTime CreatedAt { get => createdAt; set => createdAt = value; }
        public bool Deleted { get => deleted; set => deleted = value; }
    }
}
