using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uu_library_app.Entity.Concrete
{
    public class Language
    {
        private string id;
        private string languageName;
        private DateTime createdAt;
        private bool deleted;

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
