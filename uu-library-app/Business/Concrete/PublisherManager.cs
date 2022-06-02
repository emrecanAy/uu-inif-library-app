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
    public class PublisherManager : IPublisherService
    {
        IPublisherDal _publisher;
        public PublisherManager(IPublisherDal publisher)
        {
            _publisher = publisher;
        }

        public void Add(Publisher publisher)
        {
            if (publisher.Name != null)
            {
                _publisher.Add(publisher);
            }
        }

        public void Update(Publisher publisher)
        {
            if (publisher.Name != null)
            {
                _publisher.Update(publisher);
            }
        }

        public void Delete(Publisher publisher)
        {
            if(checkIfExistInBooks(publisher.id))
            {
                throw new Exception("Bu yayınevi; kitaplarda bulunan bir kitaba veya kitaplara ait olduğu için öncelikle kitaplara giderek bu yayınevine ait olan kitabı veya kitapları silmeniz gerekmektedir!");
            }
            else
            {
                _publisher.Delete(publisher);
            }
        }

        public List<Publisher> getAll()
        {
            return _publisher.getAll();
        }
        public bool checkIfExistInBooks(string publisherId)
        {
            BookManager bookManager = new BookManager(new BookDal());
            if (bookManager.getByPublisherId(publisherId) != null)
            {
                return true;
            }
            return false;
        }


    }
}
