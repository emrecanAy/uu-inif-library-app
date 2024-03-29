﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.Entity.Abstract;

namespace uu_library_app.Entity.Concrete
{
    public class DepositBook
    {
        private string id;
        private string studentId;
        private string bookId;
        private DateTime depositDate;
        private bool status;
        private DateTime escrowDate;
        private DateTime dateShouldBeEscrow;
        private DateTime createdAt;
        private bool deleted;
        public DepositBook() { }
        public DepositBook(string id, string studentId, string bookId, DateTime depositDate)
        {
            this.Id = id;
            this.StudentId = studentId;
            this.BookId = bookId;
            this.DepositDate = depositDate;
        }

        public string Id { get => id; set => id = value; }
        public string StudentId { get => studentId; set => studentId = value; }
        public string BookId { get => bookId; set => bookId = value; }
        public DateTime DepositDate { get => depositDate; set => depositDate = value; }
        public DateTime CreatedAt { get => createdAt; set => createdAt = value; }
        public bool Deleted { get => deleted; set => deleted = value; }
        public bool Status { get => status; set => status = value; }
        public DateTime EscrowDate { get => escrowDate; set => escrowDate = value; }
        public DateTime DateShouldBeEscrow { get => dateShouldBeEscrow; set => dateShouldBeEscrow = value; }
    }
}
