using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uu_library_app.Entity.Concrete
{
    public class DepositBook
    {
        private string id;
        private string studentId;
        private string bookId;
        private DateTime depositDate;
        private DateTime createdAt;
        private bool deleted;

        public DepositBook(string id, string studentId, string bookId, DateTime depositDate, DateTime createdAt, bool deleted)
        {
            this.Id = id;
            this.StudentId = studentId;
            this.BookId = bookId;
            this.DepositDate = depositDate;
            this.CreatedAt = createdAt;
            this.Deleted = deleted;
        }

        public string Id { get => id; set => id = value; }
        public string StudentId { get => studentId; set => studentId = value; }
        public string BookId { get => bookId; set => bookId = value; }
        public DateTime DepositDate { get => depositDate; set => depositDate = value; }
        public DateTime CreatedAt { get => createdAt; set => createdAt = value; }
        public bool Deleted { get => deleted; set => deleted = value; }
    }
}
