using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uu_library_app.Business.Concrete;
using uu_library_app.DataAccess.Concrete;
using uu_library_app.Entity.Concrete;

namespace uu_library_app.Core.Helpers
{
    public class DataListerHelper
    {
        public static void listBookDataToTable(DataGridView dataGrid, MySqlConnection conn)
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM Book WHERE deleted=false", conn);
            da.Fill(dt);
            dataGrid.DataSource = dt;
            dataGrid.Columns[0].Visible = false;
            dataGrid.Columns[6].Visible = false;
            dataGrid.Columns[3].HeaderText = "Yazar";
            dataGrid.Columns[2].Visible = false;
            dataGrid.Columns[5].HeaderText = "Yayınevi";
            dataGrid.Columns[4].Visible = false;
            dataGrid.Columns[1].HeaderText = "İsim";
            dataGrid.Columns[7].Visible = false;
            dataGrid.Columns[8].Visible = false;
            dataGrid.Columns[9].Visible = false;
            dataGrid.Columns[10].Visible = false;
            dataGrid.Columns[11].Visible = false;
            dataGrid.Columns[12].Visible = false;
            dataGrid.Columns[13].Visible = false;
            dataGrid.Columns[14].Visible = false;
            dataGrid.Columns[15].Visible = false;
            dataGrid.RowHeadersVisible = false;
            dataGrid.DefaultCellStyle.Font = new Font("Nirmala UI", 13);
        }

        public static void listStudentDataToTable(DataGridView dataGrid, MySqlConnection conn)
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("Select * From Student WHERE deleted=false", conn);
            da.Fill(dt);
            dataGrid.DataSource = dt;
            dataGrid.Columns[1].HeaderText = "";
            dataGrid.Columns[0].Visible = false;
            dataGrid.Columns[2].HeaderText = "";
            dataGrid.Columns[3].Visible = false;
            dataGrid.Columns[4].Visible = false;
            dataGrid.Columns[5].HeaderText = "";
            dataGrid.Columns[6].Visible = false;
            dataGrid.Columns[7].Visible = false;
            dataGrid.Columns[8].Visible = false;
            dataGrid.ColumnHeadersVisible = false;
            dataGrid.RowHeadersVisible = false;
            dataGrid.DefaultCellStyle.Font = new Font("Nirmala UI", 13);

        }

        public static void listBorrowingBookStudentDataToTable(DataGridView dataGrid, MySqlConnection conn)
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT Student.id, CONCAT( Student.firstName, ' ', Student.lastName ) AS studentFullName, Department.name, Student.number FROM Student INNER JOIN Department ON Student.departmentId = Department.id WHERE Student.deleted=0", conn);
            da.Fill(dt);
            dataGrid.DataSource = dt;
            dataGrid.Columns[1].HeaderText = "Öğrenci";
            dataGrid.Columns[0].Visible = false;
            dataGrid.Columns[2].HeaderText = "";
            dataGrid.Columns[3].Visible = false;
            dataGrid.ColumnHeadersVisible = false;
            dataGrid.RowHeadersVisible = false;
            dataGrid.DefaultCellStyle.Font = new Font("Nirmala UI", 13);

        }

        public static void listAllTakenBooksDataToTable(DataGridView dataGrid, MySqlConnection conn, string studentId)
        {
            DataTable dt = new DataTable();

            MySqlCommand command = new MySqlCommand("SELECT DepositBook.id, Student.firstName, Student.lastName, Book.id, Book.bookName, Author.firstName'authorFirstName', Author.lastName'authorLastName' FROM DepositBook INNER JOIN Student ON DepositBook.studentId = Student.id INNER JOIN Book ON DepositBook.bookId = Book.id INNER JOIN Author ON Book.authorId = Author.id WHERE Student.id=@p1", conn);
            command.Parameters.AddWithValue("@p1", studentId);
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            da.Fill(dt);
            dataGrid.DataSource = dt;
            dataGrid.Columns[1].Visible = false;
            dataGrid.Columns[0].Visible = false;
            dataGrid.Columns[2].Visible = false;
            dataGrid.Columns[3].Visible = false;
            dataGrid.Columns[4].HeaderText = "Kitap Adı";
            dataGrid.Columns[5].HeaderText = "Yazar Ad";
            dataGrid.Columns[6].HeaderText = "Yazar Soyad";
            dataGrid.ColumnHeadersVisible = false;
            dataGrid.RowHeadersVisible = false;
            dataGrid.DefaultCellStyle.Font = new Font("Nirmala UI", 13);

        }

        public static void listUndepositBooksDataToTable(DataGridView dataGrid, MySqlConnection conn, string studentId)
        {
            DataTable dt = new DataTable();

            MySqlCommand command = new MySqlCommand("SELECT DepositBook.id, Student.firstName, Student.lastName, Book.id, Book.bookName, Author.firstName'authorFirstName', Author.lastName'authorLastName' FROM DepositBook INNER JOIN Student ON DepositBook.studentId = Student.id INNER JOIN Book ON DepositBook.bookId = Book.id INNER JOIN Author ON Book.authorId = Author.id WHERE Student.id=@p1 AND DepositBook.status=0", conn);
            command.Parameters.AddWithValue("@p1", studentId);
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            da.Fill(dt);
            dataGrid.DataSource = dt;
            dataGrid.Columns[1].Visible = false;
            dataGrid.Columns[0].Visible = false;
            dataGrid.Columns[2].Visible = false;
            dataGrid.Columns[3].Visible = false;
            dataGrid.Columns[4].HeaderText = "Kitap Adı";
            dataGrid.Columns[5].HeaderText = "Yazar Ad";
            dataGrid.Columns[6].HeaderText = "Yazar Soyad";
            dataGrid.ColumnHeadersVisible = false;
            dataGrid.RowHeadersVisible = false;
            dataGrid.DefaultCellStyle.Font = new Font("Nirmala UI", 13);

        }

        public static void listDepositBooksDataToTable(DataGridView dataGrid, MySqlConnection conn, string studentId)
        {
            DataTable dt = new DataTable();

            MySqlCommand command = new MySqlCommand("SELECT DepositBook.id, Student.firstName, Student.lastName, Book.id, Book.bookName, Author.firstName'authorFirstName', Author.lastName'authorLastName' FROM DepositBook INNER JOIN Student ON DepositBook.studentId = Student.id INNER JOIN Book ON DepositBook.bookId = Book.id INNER JOIN Author ON Book.authorId = Author.id WHERE Student.id=@p1 AND DepositBook.status=1", conn);
            command.Parameters.AddWithValue("@p1", studentId);
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            da.Fill(dt);
            dataGrid.DataSource = dt;
            dataGrid.Columns[1].Visible = false;
            dataGrid.Columns[0].Visible = false;
            dataGrid.Columns[2].Visible = false;
            dataGrid.Columns[3].Visible = false;
            dataGrid.Columns[4].HeaderText = "Kitap Adı";
            dataGrid.Columns[5].HeaderText = "Yazar Ad";
            dataGrid.Columns[6].HeaderText = "Yazar Soyad";
            dataGrid.ColumnHeadersVisible = false;
            dataGrid.RowHeadersVisible = false;
            dataGrid.DefaultCellStyle.Font = new Font("Nirmala UI", 13);

        }

        public static void listInnerJoinAllBooksDataToTable(DataGridView dataGrid, MySqlConnection conn)
        {
            DataTable dt = new DataTable();

            MySqlCommand command = new MySqlCommand("SELECT Book.id, Book.bookName, CONCAT( Author.firstName, ' ', Author.lastName ) AS authorFullName, Publisher.name, Language.language, Category.name, Book.pageCount, Book.isbnNumber, Book.publishDate, Book.stockCount, Location.shelf, Book.interpreter, Book.createdAt FROM Book INNER JOIN Language ON Book.languageId = Language.id INNER JOIN Author ON Book.authorId = Author.id INNER JOIN Category ON Book.categoryId = Category.id INNER JOIN Publisher ON Book.publisherId = Publisher.id INNER JOIN Location ON Book.locationId = Location.id WHERE Book.deleted=0", conn);
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            da.Fill(dt);
            dataGrid.DataSource = dt;
            dataGrid.Columns[0].Visible = false;
            dataGrid.Columns[1].HeaderText = "Kitap Adı";
            dataGrid.Columns[2].HeaderText = "Yazar";
            dataGrid.Columns[3].HeaderText = "Yayınevi";
            dataGrid.Columns[3].Width = 150;
            dataGrid.Columns[4].HeaderText = "Dil";
            dataGrid.Columns[5].HeaderText = "Kategori";
            dataGrid.Columns[6].HeaderText = "Sayfa Sayısı";
            dataGrid.Columns[6].Width = 50;
            dataGrid.Columns[7].HeaderText = "ISBN";
            dataGrid.Columns[8].HeaderText = "Yayın Tarihi";
            dataGrid.Columns[8].Width = 100;
            dataGrid.Columns[9].HeaderText = "Stok";
            dataGrid.Columns[9].Width = 50;
            dataGrid.Columns[10].HeaderText = "Konum";
            dataGrid.Columns[11].HeaderText = "Çevirmen";
            dataGrid.Columns[12].HeaderText = "Oluşturulma Tarihi";
            dataGrid.RowHeadersVisible = false;
            dataGrid.DefaultCellStyle.Font = new Font("Nirmala UI", 13);

        }

        public static void listInnerJoinAllStudentsDataToTable(DataGridView dataGrid, MySqlConnection conn)
        {
            DataTable dt = new DataTable();

            MySqlCommand command = new MySqlCommand("SELECT Student.id, CONCAT( Student.firstName, ' ', Student.lastName ) AS studentFullName, Student.number, Student.eMail, Department.name, Student.createdAt FROM Student INNER JOIN Department ON Student.departmentId = Department.id WHERE Student.deleted=0", conn);
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            da.Fill(dt);
            dataGrid.DataSource = dt;
            dataGrid.Columns[0].Visible = false;
            dataGrid.Columns[1].HeaderText = "Öğrenci";
            dataGrid.Columns[2].HeaderText = "Okul No";
            dataGrid.Columns[3].HeaderText = "Eposta";
            dataGrid.Columns[4].HeaderText = "Bölüm";
            dataGrid.Columns[5].HeaderText = "Kayıt Tarihi";
            dataGrid.RowHeadersVisible = false;
            dataGrid.DefaultCellStyle.Font = new Font("Nirmala UI", 13);

        }

        public static void listInnerJoinSomeBookDataToTable(DataGridView dataGrid, MySqlConnection conn)
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT Book.id, Book.bookName, CONCAT( Author.firstName, ' ', Author.lastName ) AS authorFullName, Publisher.name, Language.language, Category.name, Book.pageCount, Book.isbnNumber, Book.publishDate, Book.stockCount, Location.shelf, Book.interpreter, Book.createdAt FROM Book INNER JOIN Language ON Book.languageId = Language.id INNER JOIN Author ON Book.authorId = Author.id INNER JOIN Category ON Book.categoryId = Category.id INNER JOIN Publisher ON Book.publisherId = Publisher.id INNER JOIN Location ON Book.locationId = Location.id WHERE Book.deleted=0", conn);
            da.Fill(dt);
            dataGrid.DataSource = dt;
            dataGrid.Columns[0].Visible = false;
            dataGrid.Columns[6].Visible = false;
            dataGrid.Columns[2].HeaderText = "Yazar";
            dataGrid.Columns[5].Visible = false;
            dataGrid.Columns[3].HeaderText = "Yayınevi";
            dataGrid.Columns[4].Visible = false;
            dataGrid.Columns[1].HeaderText = "İsim";
            dataGrid.Columns[7].Visible = false;
            dataGrid.Columns[8].Visible = false;
            dataGrid.Columns[9].Visible = false;
            dataGrid.Columns[10].Visible = false;
            dataGrid.Columns[11].Visible = false;
            dataGrid.Columns[12].Visible = false;
            dataGrid.RowHeadersVisible = false;
            dataGrid.DefaultCellStyle.Font = new Font("Nirmala UI", 13);
        }

        public static void listInnerJoinBorrowingBookDataToTable(DataGridView dataGrid, MySqlConnection conn)
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT Book.id, Book.bookName, CONCAT( Author.firstName, ' ', Author.lastName ) AS authorFullName, Publisher.name, Book.isbnNumber FROM Book INNER JOIN Author ON Book.authorId = Author.id INNER JOIN Publisher ON Book.publisherId = Publisher.id WHERE Book.deleted=0", conn);
            da.Fill(dt);
            dataGrid.DataSource = dt;
            dataGrid.Columns[0].Visible = false;
            dataGrid.Columns[1].HeaderText = "Kitap";
            dataGrid.Columns[2].HeaderText = "Yazar";
            dataGrid.Columns[3].Visible = false;
            dataGrid.Columns[4].Visible = false;
            dataGrid.RowHeadersVisible = false;
            dataGrid.DefaultCellStyle.Font = new Font("Nirmala UI", 13);
        }


    }
}
