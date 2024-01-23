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
        List<Book> getAllByName(string name);
        Book getById(string id);
        Book getByCategoryId(string categoryId);
        Book getByAuthorId(string authorId);
        Book getByLanguageId(string languageId);
        Book getByLocationId(string locationId);
        Book getByPublisherId(string publisherId);

        void Add(Book book);
        void Update(Book book);
        void Delete(Book book);
        
    }
}
