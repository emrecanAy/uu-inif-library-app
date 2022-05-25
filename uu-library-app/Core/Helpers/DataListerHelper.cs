using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.Business.Concrete;
using uu_library_app.DataAccess.Concrete;
using uu_library_app.Entity.Concrete;

namespace uu_library_app.Core.Helpers
{
    public class DataListerHelper
    {
        BookManager bookManager = new BookManager(new BookDal());
        StudentManager studentManager = new StudentManager(new StudentDal());
        
        public List<Book> getMostFrequentBookData(string[] arr)
        {
            List<Book> bookList = new List<Book>();

            foreach(string id in arr)
            {
                Console.WriteLine("En çok okunan idler"+ id);
            }

            foreach (string id in arr)
            {
                Book book = bookManager.getById(id);
                Console.WriteLine("En çok okunan kitaplar"+ book.BookName);
                bookList.Add(book);
            }
            return bookList;
        }

        public List<Student> getMostFrequentStudentData(string[] arr, int n)
        {
            List<Student> studentList = new List<Student>();
            string[] mostFrequentBooks = ArrayFindersHelper.mostFrequentMultiple(arr, n);
            foreach (string id in mostFrequentBooks)
            {
                Student student = studentManager.getById(id);
                studentList.Add(student);
            }

            return studentList;
        }

        public string[] getReadBooks(MySqlConnection conn)
        {
            List<string> books = new List<string>();
            MySqlCommand command = new MySqlCommand("SELECT DepositBook.id'depositBookId', DepositBook.studentId'studentId', Book.id'bookId', Book.bookName FROM DepositBook INNER JOIN Book ON DepositBook.bookId=Book.id WHERE DepositBook.deleted=0", conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string id = reader[2].ToString();
                books.Add(id);
            }
            string[] readBooksArr = books.ToArray();

            return readBooksArr;
        }

    }
}
