using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uu_library_app.Entity.Concrete
{
    public class Author
    {
        private string id;
        private string firstName;
        private string lastName;
        private DateTime createdAt;
        private bool deleted;

        public Author(string id, string firstName, string lastName, DateTime createdAt, bool deleted)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.createdAt = createdAt;
            this.deleted = deleted;
        }

        public string Id { get => id; set => id = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public DateTime CreatedAt { get => createdAt; set => createdAt = value; }
        public bool Deleted { get => deleted; set => deleted = value; }
    }
}
