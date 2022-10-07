using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uu_library_app.Business.Concrete;
using uu_library_app.DataAccess.Concrete;
using uu_library_app.Entity.Concrete;
using uu_library_app.Entity.Concrete.DTO;

namespace uu_library_app.Core.Helpers
{
    public static class ExportFileDataHelper
    {
        static MySqlConnection conn = new MySqlConnection(DbConnection.connectionString);
        static DepositBookManager depositBookManager = new DepositBookManager(new DepositBookDal());
        static SettingsManager settingsManager = new SettingsManager(new SettingsDal());

        public static DataTable listInnerJoinAllBooksDataToTable()
        {
            conn.Open();
            DataTable dt = new DataTable();
            MySqlCommand command = new MySqlCommand("SELECT Book.id, Book.bookName, CONCAT( Author.firstName, ' ', Author.lastName ) AS authorFullName, Publisher.name'publisherName', Language.language, Category.name'categoryName', Book.pageCount, Book.isbnNumber, Book.publishDate, Book.stockCount, Location.shelf, Book.interpreter'interpreterName', Book.createdAt FROM Book INNER JOIN Language ON Book.languageId = Language.id INNER JOIN Author ON Book.authorId = Author.id INNER JOIN Category ON Book.categoryId = Category.id INNER JOIN Publisher ON Book.publisherId = Publisher.id INNER JOIN Location ON Book.locationId = Location.id WHERE Book.deleted=0", conn);
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            da.Fill(dt);
            conn.Close();
            return dt;          
        }

        public static DataTable listInnerJoinAllBooksDataToTableWithTrNames()
        {
            conn.Open();
            DataTable dt = new DataTable();
            MySqlCommand command = new MySqlCommand("SELECT Book.id'Id', Book.bookName'Kitap', CONCAT( Author.firstName, ' ', Author.lastName ) AS 'Yazar', Publisher.name'Yayınevi', Language.language'Dil', Category.name'Kategori', Location.shelf'Konum', Book.pageCount'Sayfa Sayısı', Book.fixtureNo'Demirbaş No', Book.isbnNumber'ISBN', Book.publishDate'Yayın Tarihi', Book.stockCount'Stok Sayısı', Book.interpreter'Çevirmen', Book.createdAt'Oluşturulma Tarihi' FROM Book INNER JOIN Language ON Book.languageId = Language.id INNER JOIN Author ON Book.authorId = Author.id INNER JOIN Category ON Book.categoryId = Category.id INNER JOIN Publisher ON Book.publisherId = Publisher.id INNER JOIN Location ON Book.locationId = Location.id WHERE Book.deleted=0", conn);
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            da.Fill(dt);
            conn.Close();
            return dt;
        }
        public static DataTable listInnerJoinAllDeletedBooksDataToTable()
        {
            conn.Open();
            DataTable dt = new DataTable();
            MySqlCommand command = new MySqlCommand("SELECT Book.id'Id', Book.bookName'Kitap', CONCAT( Author.firstName, ' ', Author.lastName ) AS 'Yazar', Publisher.name'Yayınevi', Language.language'Dil', Category.name'Kategori', Location.shelf'Konum', Book.pageCount'Sayfa Sayısı', Book.fixtureNo'Demirbaş No', Book.isbnNumber'ISBN', Book.publishDate'Yayın Tarihi', Book.stockCount'Stok Sayısı', Book.interpreter'Çevirmen', Book.createdAt'Oluşturulma Tarihi' FROM Book INNER JOIN Language ON Book.languageId = Language.id INNER JOIN Author ON Book.authorId = Author.id INNER JOIN Category ON Book.categoryId = Category.id INNER JOIN Publisher ON Book.publisherId = Publisher.id INNER JOIN Location ON Book.locationId = Location.id WHERE Book.deleted=1", conn);
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            da.Fill(dt);
            conn.Close();
            return dt;

        }

        public static DataTable listInnerJoinMostFrequentTenBooks()
        {
            conn.Open();
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT bookId'Id',count(*)'Sayı', Book.bookName'Kitap' FROM DepositBook INNER JOIN Book ON DepositBook.bookId=Book.id GROUP BY DepositBook.bookId ORDER BY count(*) DESC LIMIT 10", conn);
            da.Fill(dt);
            conn.Close();
            return dt;
        }

        public static DataTable listInnerJoinAllStudentsDataToTable(DataGridView dataGrid, MySqlConnection conn)
        {
            conn.Open();
            DataTable dt = new DataTable();
            MySqlCommand command = new MySqlCommand("SELECT Student.id, CONCAT( Student.firstName, ' ', Student.lastName ) AS AdSoyad, Student.number'OkulNo', Student.eMail'E-Posta', Department.name'Bolum', Student.createdAt'OlusturulmaTarihi', Faculty.name'Fakulte' FROM Student INNER JOIN Department ON Student.departmentId = Department.id INNER JOIN Faculty ON Student.facultyId=Faculty.id WHERE Student.deleted=0", conn);
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            da.Fill(dt);
            conn.Close();
            return dt;
            
        }

        public static DataTable listInnerJoinAllDeletedStudentsDataToTable(DataGridView dataGrid, MySqlConnection conn)
        {
            conn.Open();
            DataTable dt = new DataTable();
            MySqlCommand command = new MySqlCommand("SELECT Student.id, CONCAT( Student.firstName, ' ', Student.lastName ) AS AdSoyad, Student.number'OkulNo', Student.eMail'E-Posta', Department.name'Bolum', Student.createdAt'OlusturulmaTarihi', Faculty.name'Fakulte' FROM Student INNER JOIN Department ON Student.departmentId = Department.id INNER JOIN Faculty ON Student.facultyId=Faculty.id WHERE Student.deleted=1", conn);
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            da.Fill(dt);
            conn.Close();
            return dt;
        }

        public static void listUndepositBooksConcatAuthorNameDataToTable(DataGridView dataGrid, MySqlConnection conn)
        {
            conn.Open();
            DataTable dt = new DataTable();

            MySqlCommand command = new MySqlCommand("SELECT DepositBook.id, CONCAT(Student.firstName, Student.lastName) as Ogrenci, Book.id, Book.bookName, CONCAT( Author.firstName, ' ', Author.lastName ) AS authorFullName FROM DepositBook INNER JOIN Student ON DepositBook.studentId = Student.id INNER JOIN Book ON DepositBook.bookId = Book.id INNER JOIN Author ON Book.authorId = Author.id WHERE DepositBook.status=0", conn);
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            da.Fill(dt);
            conn.Close();

        }
        public static List<DepositBook> getExpiredBooks()
        {
            List<DepositBook> expiredBooks = new List<DepositBook>();
            List<DepositBook> depositBooksList = depositBookManager.getAllUndeposited();
            foreach (DepositBook depositBook in depositBooksList)
            {
                TimeSpan ts = depositBook.DepositDate - DateTime.Now;
                string howManyDaysPast = ts.Days.ToString();
                if (howManyDaysPast.StartsWith("-"))
                {
                    int daysPast = Convert.ToInt32(howManyDaysPast);
                    if (daysPast <= -settingsManager.getSettings().DepositDay)
                    {
                        Console.WriteLine(depositBook.Id + " Gecikme Gunu Sayısı: " + daysPast);
                        expiredBooks.Add(depositBook);
                    }
                }
            }
            return expiredBooks;
        }

        public static List<DepositBook> getExpiredBooksByStudentId(string studentId)
        {
            List<DepositBook> expiredBooks = new List<DepositBook>();
            List<DepositBook> depositBooksList = depositBookManager.getAllUndepositedByStudentId(studentId);
            foreach (DepositBook depositBook in depositBooksList)
            {
                TimeSpan ts = depositBook.DepositDate - DateTime.Now;
                string howManyDaysPast = ts.Days.ToString();
                if (howManyDaysPast.StartsWith("-"))
                {
                    int daysPast = Convert.ToInt32(howManyDaysPast);
                    if (daysPast <= -settingsManager.getSettings().DepositDay)
                    {
                        Console.WriteLine(depositBook.Id + " Gecikme Gunu Sayısı: " + daysPast);
                        expiredBooks.Add(depositBook);
                    }
                }
            }
            return expiredBooks;
        }

        public static List<DepositBookDto> getExpiredBookWithNames()
        {
            List<DepositBookDto> depositBookDtoList = new List<DepositBookDto>();
            int depositDay = settingsManager.getSettings().DepositDay;
            foreach (DepositBook depositBook in getExpiredBooks())
            {
                conn.Open();
                TimeSpan ts = depositBook.DepositDate - DateTime.Now;
                if (ts.Days <= depositDay)
                {
                    MySqlCommand command = new MySqlCommand("SELECT DepositBook.id, Student.number'OkulNo', CONCAT(Student.firstName,' ',Student.lastName) as Ogrenci, Book.bookName'Kitap', CONCAT(Author.firstName,' ',Author.lastName) as Yazar, DepositBook.createdAt FROM DepositBook INNER JOIN Student ON DepositBook.studentId = Student.id INNER JOIN Book ON DepositBook.bookId = Book.id INNER JOIN Author ON Book.authorId = Author.id WHERE DepositBook.id=@p1 AND DepositBook.status=0", conn);
                    command.Parameters.AddWithValue("@p1", depositBook.Id);
                    MySqlDataReader reader = command.ExecuteReader();
                    DepositBookDto depositBookDto = new DepositBookDto();
                    while (reader.Read())
                    {
                        depositBookDto.Id = reader[0].ToString();
                        depositBookDto.OgrenciNo = reader[1].ToString();
                        depositBookDto.Ogrenci = reader[2].ToString();
                        depositBookDto.Kitap = reader[3].ToString();
                        depositBookDto.Yazar = reader[4].ToString();
                        depositBookDto.OlusturulmaTarihi = Convert.ToDateTime(reader[5]);
                        depositBookDto.GecikmeGunu = ts.Days;

                    }
                    depositBookDtoList.Add(depositBookDto);
                    conn.Close();
                }
                
            }
            return depositBookDtoList;
        }

        public static List<DepositBookDto> getExpiredBookWithNamesByStudentId(string studentId)
        {
            List<DepositBookDto> depositBookDtoList = new List<DepositBookDto>();
            foreach (DepositBook depositBook in getExpiredBooksByStudentId(studentId))
            {
                conn.Open();
                TimeSpan ts = depositBook.DepositDate - DateTime.Now;
                if (ts.Days <= settingsManager.getSettings().DepositDay)
                {
                    MySqlCommand command = new MySqlCommand("SELECT DepositBook.id, Student.number'OkulNo', CONCAT(Student.firstName,' ',Student.lastName) as Ogrenci, Book.bookName'Kitap', CONCAT(Author.firstName,' ',Author.lastName) as Yazar, DepositBook.createdAt FROM DepositBook INNER JOIN Student ON DepositBook.studentId = Student.id INNER JOIN Book ON DepositBook.bookId = Book.id INNER JOIN Author ON Book.authorId = Author.id WHERE DepositBook.id=@p1 AND DepositBook.status=0", conn);
                    command.Parameters.AddWithValue("@p1", depositBook.Id);
                    MySqlDataReader reader = command.ExecuteReader();
                    DepositBookDto depositBookDto = new DepositBookDto();
                    while (reader.Read())
                    {
                        depositBookDto.Id = reader[0].ToString();
                        depositBookDto.OgrenciNo = reader[1].ToString();
                        depositBookDto.Ogrenci = reader[2].ToString();
                        depositBookDto.Kitap = reader[3].ToString();
                        depositBookDto.Yazar = reader[4].ToString();
                        depositBookDto.OlusturulmaTarihi = Convert.ToDateTime(reader[5]);
                        depositBookDto.GecikmeGunu = ts.Days;

                    }
                    depositBookDtoList.Add(depositBookDto);
                    conn.Close();
                }

            }
            return depositBookDtoList;
        }

        public static DataTable listDepositBooksDataToTable(DataGridView dataGrid, MySqlConnection conn, string depositBookId)
        {
            conn.Open();
            DataTable dt = new DataTable();
            MySqlCommand command = new MySqlCommand("SELECT DepositBook.id, Student.firstName, Student.lastName, Book.id, Book.bookName, Author.firstName'authorFirstName', Author.lastName'authorLastName' FROM DepositBook INNER JOIN Student ON DepositBook.studentId = Student.id INNER JOIN Book ON DepositBook.bookId = Book.id INNER JOIN Author ON Book.authorId = Author.id WHERE DepositBook.id=@p1", conn);
            command.Parameters.AddWithValue("@p1", depositBookId);
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            conn.Close();
            da.Fill(dt);
            return dt;
            
        }

    }
}
