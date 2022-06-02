using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.Business.Abstract;
using uu_library_app.DataAccess.Abstract;
using uu_library_app.DataAccess.Concrete;
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
            if (checkIfExistInBooks(location.Id))
            {
                throw new Exception("Bu konum; kitaplarda bulunan bir kitaba veya kitaplara ait olduğu için öncelikle kitaplara giderek bu konuma ait olan kitabı veya kitapları silmeniz gerekmektedir!");
            }
            else
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

        public bool checkIfExistInBooks(string locationId)
        {
            BookManager bookManager = new BookManager(new BookDal());
            if (bookManager.getByLocationId(locationId) != null)
            {
                return true;
            }
            return false;
        }
    }
}
