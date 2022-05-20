using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uu_library_app.Entity.Abstract
{
    public abstract class IBaseModelWithIdAndDate
    {
        public string id;
        public DateTime createdAt;
        public bool deleted;
    }
}
