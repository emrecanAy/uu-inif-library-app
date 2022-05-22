﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.Business.Abstract;
using uu_library_app.DataAccess.Abstract;
using uu_library_app.Entity.Concrete;
namespace uu_library_app.Business.Concrete
{
    internal class DepositBookManager : IDepositBookService
    {
        IDepositBookDal _depositBook;
        public DepositBookManager(IDepositBookDal depositBook)
        {
            this._depositBook = depositBook;
        }
        public void Add(DepositBook depositBook)
        {
            if (depositBook.DepositDate != null || depositBook.StudentId != null || depositBook.BookId != null)
            {
                _depositBook.Add(depositBook);
            }
        }

        public void Delete(string id)
        {
            if(id != "")
            {
                _depositBook.Delete(id);
            }
            
        }

        public void depositBook(string id)
        {
            if (id != "")
            {
                _depositBook.depositBook(id);
            }
        }

        public List<DepositBook> findAllByStudentId(string studentId)
        {
            return _depositBook.findAllByStudentId(studentId);
        }

        public List<DepositBook> getAll()
        {
            return _depositBook.getAll();
        }

        public void Update(DepositBook depositBook)
        {
            _depositBook.Update(depositBook);
        }
    }
}
