﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.Entity.Concrete;

namespace uu_library_app.DataAccess.Abstract
{
    public interface IDepositBookDal
    {
        List<DepositBook> getAll();
        List<DepositBook> getAllUndeposited();
        List<DepositBook> getAllUndepositedByStudentId(string studentId);
        List<DepositBook> getAllDeposited();
        List<DepositBook> getAllByBookId(string bookId);
        void Add(DepositBook depositBook);
        void Update(DepositBook depositBook);
        void Delete(DepositBook depositBook);
        void depositBook(string id);
        List<DepositBook> findAllByStudentId(string studentId);
        DepositBook findById(string id);
        DepositBook getByBookId(string bookId);
        DepositBook getByStudentId(string studentId);
        DepositBook getByStudentIdAndBookId(DepositBook depositBook);
    }
}
