using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.Entity.Abstract;

namespace uu_library_app.Entity.Concrete
{
    public class Book : IBaseModelWithIdAndDate
    {
        private string bookName;
        private string languageId;
        private string authorId;
        private string categoryId;
        private string publisherId;
        private string locationId;
        private int pageCount;
        private string isbnNumber;
        private DateTime publishDate;
        private int publishCount;
        private int stockCount;
        private bool status;
        private string interpreter;

        public Book() { }

        public Book(string id, string bookName, string languageId, string authorId, string categoryId, string publisherId, string locationId, int pageCount, string isbnNumber, DateTime publishDate, int publishCount, int stockCount, bool status, string interpreter)
        {
            this.Id = id;
            this.BookName = bookName;
            this.LanguageId = languageId;
            this.AuthorId = authorId;
            this.CategoryId = categoryId;
            this.PublisherId = publisherId;
            this.LocationId = locationId;
            this.PageCount = pageCount;
            this.IsbnNumber = isbnNumber;
            this.PublishDate = publishDate;
            this.PublishCount = publishCount;
            this.StockCount = stockCount;
            this.Status = status;
            this.Interpreter = interpreter;
        }

        public string Id { get => id; set => id = value; }
        public string BookName { get => bookName; set => bookName = value; }
        public string LanguageId { get => languageId; set => languageId = value; }
        public string AuthorId { get => authorId; set => authorId = value; }
        public string CategoryId { get => categoryId; set => categoryId = value; }
        public string PublisherId { get => publisherId; set => publisherId = value; }
        public string LocationId { get => locationId; set => locationId = value; }
        public int PageCount { get => pageCount; set => pageCount = value; }
        public string IsbnNumber { get => isbnNumber; set => isbnNumber = value; }
        public DateTime PublishDate { get => publishDate; set => publishDate = value; }
        public int PublishCount { get => publishCount; set => publishCount = value; }
        public int StockCount { get => stockCount; set => stockCount = value; }
        public bool Status { get => status; set => status = value; }
        public string Interpreter { get => interpreter; set => interpreter = value; }
        public DateTime CreatedAt { get => createdAt; set => createdAt = value; }
        public bool Deleted { get => deleted; set => deleted = value; }


    }
}
