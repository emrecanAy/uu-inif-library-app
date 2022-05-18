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
    public class AuthorManager:IAuthorService
    {
        IAuthorDal _service;

        public AuthorManager(IAuthorDal service)
        {
            _service = service;
        }

        public void Add(Author author)
        {
            _service.Add(author);
        }

        public void Delete(Author author)
        {
            _service.Delete(author);
        }

        public List<Author> getbyAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Author author)
        {
            throw new NotImplementedException();
        }
    }
}
