using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.Entity.Concrete;

namespace uu_library_app.DataAccess.Abstract
{
    public interface ILocationDal
    {
        List<Location> getAll();
        void Add(Location location);
        void Update(Location location);
        void Delete(string id);
    }
}
