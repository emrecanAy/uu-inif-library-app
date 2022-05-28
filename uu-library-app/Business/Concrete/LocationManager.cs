using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.Business.Abstract;
using uu_library_app.DataAccess.Abstract;
using uu_library_app.Entity.Concrete;

namespace uu_library_app.Business.Concrete
{
    public class LocationManager : ILocationService
    {
        ILocationDal _location;
        public LocationManager(ILocationDal location)
        {
            this._location = location;
        }

        public void Add(Location location)
        {
            if (location.Shelf != null)
            {
                _location.Add(location);
            }
        }

        public void Delete(Location location)
        {
            if (location != null)
            {
                _location.Delete(location);
            }
        }

        public List<Location> getAll()
        {
            return _location.getAll();
        }

        public void Update(Location location)
        {
            if (location.Shelf != null || location.Shelf.Length > 0)
            {
                _location.Update(location);
            }
        }
    }
}
