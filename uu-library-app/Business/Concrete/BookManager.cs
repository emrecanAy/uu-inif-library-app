﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.Business.Abstract;
using uu_library_app.Core.Exceptions;
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
            if(book.AuthorId != null && book.BookName != null && book.CategoryId != null && book.IsbnNumber != null && book.LanguageId != null && book.PageCount != 0 && book.PublisherId != null && book.StockCount != 0 && book.FixtureNo != null && book.PublishDate != null) 
            {
                _service.Add(book);
                return;
            }
            else
            {
                throw new Exception("Lütfen gerekli tüm alanları doldurun!");
            }
        }

        public void Update(Book book)
        {
            if (book!=null)
            {
                _service.Update(book);
            }
            else
            {
                throw new NotNullException("Lütfen gerekli tüm alanları doldurun!");
            }
        }

        public void Delete(Book book)
        {
            if (checkIfExistInDepositBooks(book))
            {
                throw new Exception("Bu kitap; ödünç verilen kitaplarda bulunan bir öğrenciye veya öğrencilere ait olduğu için öncelikle ödünç kitaplara giderek bu kitaba ait olan ödünç kitabı veya kitapları silmeniz veya teslim almanız gerekmektedir!");
            }
            else
            {
                if(book.Id != null && book.BookName != null)
                {
                    _service.Delete(book);
                    return;
                }
                else
                {
                    throw new NotNullException("Lütfen silmek için bir kitap seçin!");
                }
                
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

        public Book getByCategoryId(string categoryId)
        {
            return _service.getByCategoryId(categoryId);
        }

        public Book getByAuthorId(string authorId)
        {
            return _service.getByAuthorId(authorId);
        }

        public Book getByLanguageId(string languageId)
        {
            return _service.getByLanguageId(languageId);
        }

        public Book getByLocationId(string locationId)
        {
            return _service.getByLocationId(locationId);
        }

        public Book getByPublisherId(string publisherId)
        {
            return _service.getByPublisherId(publisherId);
        }

        bool checkIfExistInDepositBooks(Book book)
        {
            DepositBookManager depositBookManager = new DepositBookManager(new DepositBookDal());
            if(depositBookManager.getByBookId(book.Id).Id != null)
            {
                return true;
            }
            return false;
        }

        public List<Book> getAllByName(string name)
        {
           return this._service.getAllByName(name);
        }
    }
}
