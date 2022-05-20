using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.Entity.Abstract;

namespace uu_library_app.Entity.Concrete
{
    public class Language : IBaseModelWithIdAndDate
    {
        private string languageName;
        public Language() { }
        public Language(string id, string languageName)
        {
            this.id = id;
            this.languageName = languageName;
        }

        public string Id { get => id; set => id = value; }
        public DateTime CreatedAt { get => createdAt; set => createdAt = value; }
        public bool Deleted { get => deleted; set => deleted = value; }
        public string LanguageName { get => languageName; set => languageName = value; }
    }
}
