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
    public class BookManager : IBookService
    {
        IBookDal _service;
        public BookManager(IBookDal service)
        {
            _service = service;
        }

        public void Add(Book book)
        {
            if(book.AuthorId != null && book.BookName != null && book.CategoryId != null && book.IsbnNumber != null && book.LanguageId != null && book.PageCount != null && book.PublisherId != null && book.StockCount != null)
            {
                _service.Add(book);
            }
        }

        public void Update(Book book)
        {
            if (book.AuthorId != null && book.BookName != null && book.CategoryId != null && book.IsbnNumber != null && book.LanguageId != null && book.PageCount != null && book.PublisherId != null && book.StockCount != null)
            {
                _service.Update(book);
            }
        }

        public void Delete(string id)
        {
            if(id != null && id.Length > 0)
            {
                _service.Delete(id);
            }
        }

        public List<Book> getAll()
        {
            return _service.getAll();
        }

        public List<Book> getAllByCategory(string categoryId)
        {
            return _service.getAllByCategory(categoryId);
        }

        public List<Book> getAllSortedByAddedDate()
        {
            return _service.getAllSortedByAddedDate();
        }

        public List<Book> getAllSortedByName()
        {
            return _service.getAllSortedByName();
        }

        public Book getById(string id)
        {
            return _service.getById(id);
        }

        
    }
}
