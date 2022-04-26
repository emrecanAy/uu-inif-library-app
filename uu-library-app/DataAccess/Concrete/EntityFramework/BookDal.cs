using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.DataAccess.Abstract;
using uu_library_app.Entity.Concrete;

namespace uu_library_app.DataAccess.Concrete
{
    public class BookDal : IBookDal
    {
        public void Add(Book book)
        {
            throw new NotImplementedException();
        }

        public void Delete(Book book)
        {
            throw new NotImplementedException();
        }

        public List<Book> getAll()
        {
            throw new NotImplementedException();
        }

        public List<Book> getAllByCategory(string categoryId)
        {
            throw new NotImplementedException();
        }

        public List<Book> getAllSortedByAddedDate()
        {
            throw new NotImplementedException();
        }

        public List<Book> getAllSortedByName()
        {
            throw new NotImplementedException();
        }

        public Book getById(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(Book book)
        {
            throw new NotImplementedException();
        }
    }
}
