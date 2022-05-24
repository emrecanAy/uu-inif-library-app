using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.Entity.Abstract;

namespace uu_library_app.Entity.Concrete
{
    public class Location : IBaseModelWithIdAndDate
    {
        private string shelf;
        private string categoryId;

        public Location()
        {
        }

        public Location(string id, string shelf, string categoryId)
        {
            this.id = id;
            this.shelf = shelf;
            this.CategoryId = categoryId;

        }

        public string Id { get => id; set => id = value; }
        public string Shelf { get => shelf; set => shelf = value; }
        public DateTime CreatedAt { get => createdAt; set => createdAt = value; }
        public bool Deleted { get => deleted; set => deleted = value; }
        public string CategoryId { get => categoryId; set => categoryId = value; }
    }
}
