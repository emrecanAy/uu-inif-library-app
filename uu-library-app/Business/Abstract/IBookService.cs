using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.Entity.Concrete;

namespace uu_library_app.Business.Abstract
{
    public interface IBookService
    {
        List<Book> getAll();
        List<Book> getAllByCategory(string categoryId);
        List<Book> getAllSortedByName();
        List<Book> getAllSortedByAddedDate();
        void Add(Book book);
        void Update(Book book);
        void Delete(Book book);
        Book getById(string id);
    }
}
