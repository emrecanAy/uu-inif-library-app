using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.Entity.Abstract;

namespace uu_library_app.Entity.Concrete
{
    public class Faculty : IBaseModelWithIdAndDate
    {
        private string name;
        public Faculty() { }
        public Faculty(string id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public DateTime CreatedAt { get => createdAt; set => createdAt = value; }
        public bool Deleted { get => deleted; set => deleted = value; }
    }
}
