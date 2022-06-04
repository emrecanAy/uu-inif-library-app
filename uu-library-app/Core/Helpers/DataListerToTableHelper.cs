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
using uu_library_app.Entity.Concrete;
using uu_library_app.Entity.Concrete.DTO;

namespace uu_library_app.Core.Helpers
{
    public static class DataListerToTableHelper
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

        public static void GetStudentsWhoHasThatBook(DepositBookManager depositBookManager, SettingsManager settingsManager, MySqlConnection conn, DataGridView dataGridView ,string bookId)
        {
            List<StudentDto> studentList = new List<StudentDto>();
            studentList.Clear();

            List<DepositBook> depositBooksList = depositBookManager.getAllByBookId(bookId);
            foreach (DepositBook depositBook in depositBooksList)
            {
                DateTime dt = depositBook.DepositDate.AddDays(settingsManager.getSettings().DepositDay);
                studentList.Add(DataListerHelper.listInnerJoinStudentDataToTableByStudentId(conn, depositBook.StudentId, dt));
            }

            dataGridView.DataSource = studentList;
            dataGridView.Columns[0].Visible = false;
            dataGridView.Columns[1].HeaderText = "Öğrenci";
            dataGridView.Columns[2].HeaderText = "Numara";
            dataGridView.Columns[3].HeaderText = "Bölüm";
            dataGridView.Columns[4].HeaderText = "G.Tarih";
            depositBooksList.Clear();

        }

        public static void listStudentDataToTableBookQuery(DataGridView dataGrid, MySqlConnection conn)
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("Select * From Student WHERE deleted=false", conn);
            da.Fill(dt);
            dataGrid.DataSource = dt;

        }

        public static void listBorrowingBookPersonnelDataToTable(DataGridView dataGrid, MySqlConnection conn)
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT Personnel.id, CONCAT( Personnel.firstName, ' ', Personnel.lastName ) AS personnelFullName, Department.name, Personnel.eMail, Faculty.name FROM Personnel INNER JOIN Department ON Personnel.departmentId = Department.id INNER JOIN Faculty ON Personnel.facultyId = Faculty.id WHERE Personnel.deleted=0", conn);
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

        public static DataTable listAllTakenBooksDataToTableByStudentId(MySqlConnection conn, string studentId)
        {
            DataTable dt = new DataTable();
            MySqlCommand command = new MySqlCommand("SELECT DepositBook.id'Id', CONCAT(Student.firstName,' ',Student.lastName) as Ogrenci, Book.bookName'Kitap', CONCAT( Author.firstName, ' ', Author.lastName ) AS Yazar, DepositBook.depositDate'AlinmaTarihi' FROM DepositBook INNER JOIN Student ON DepositBook.studentId = Student.id INNER JOIN Book ON DepositBook.bookId = Book.id INNER JOIN Author ON Book.authorId = Author.id WHERE Student.id=@p1", conn);
            command.Parameters.AddWithValue("@p1", studentId);
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            da.Fill(dt);
            return dt;
        }

        public static void listAllTakenBooksConcatAuthorNameDataToTable(DataGridView dataGrid, MySqlConnection conn, string studentId)
        {
            DataTable dt = new DataTable();

            MySqlCommand command = new MySqlCommand("SELECT DepositBook.id, Student.firstName, Student.lastName, Book.id, Book.bookName, CONCAT( Author.firstName, ' ', Author.lastName ) AS authorFullName FROM DepositBook INNER JOIN Student ON DepositBook.studentId = Student.id INNER JOIN Book ON DepositBook.bookId = Book.id INNER JOIN Author ON Book.authorId = Author.id WHERE Student.id=@p1", conn);
            command.Parameters.AddWithValue("@p1", studentId);
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            da.Fill(dt);
            dataGrid.DataSource = dt;
            dataGrid.Columns[1].Visible = false;
            dataGrid.Columns[0].Visible = false;
            dataGrid.Columns[2].Visible = false;
            dataGrid.Columns[3].Visible = false;
            dataGrid.Columns[4].HeaderText = "Kitap Adı";
            dataGrid.Columns[5].HeaderText = "Yazar";
            dataGrid.ColumnHeadersVisible = false;
            dataGrid.RowHeadersVisible = false;
            dataGrid.DefaultCellStyle.Font = new Font("Nirmala UI", 13);

        }

        public static void listInnerJoinAllPersonnelsNotConcatDataToTable(DataGridView dataGrid, MySqlConnection conn)
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT Personnel.id, Personnel.firstName, Personnel.lastName, Personnel.eMail, Department.name'departmentName', Faculty.name'facultyName' FROM Personnel INNER JOIN Department ON Personnel.departmentId = Department.id INNER JOIN Faculty ON Personnel.facultyId = Faculty.id WHERE Personnel.deleted=0", conn);
            da.Fill(dt);
            dataGrid.DataSource = dt;
            dataGrid.Columns[0].Visible = false;
            dataGrid.Columns[2].HeaderText = "";
            dataGrid.Columns[4].Visible = false;
            dataGrid.Columns[5].Visible = false;
            dataGrid.ColumnHeadersVisible = false;
            dataGrid.RowHeadersVisible = false;
            dataGrid.DefaultCellStyle.Font = new Font("Nirmala UI", 13);
        }

        public static void listInnerJoinAllPersonnelsNotConcatDataToTable2(DataGridView dataGrid, MySqlConnection conn)
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT Personnel.id, Personnel.firstName, Personnel.lastName, Personnel.eMail, Department.name'departmentName', Faculty.name'facultyName' FROM Personnel INNER JOIN Department ON Personnel.departmentId = Department.id INNER JOIN Faculty ON Personnel.facultyId = Faculty.id WHERE Personnel.deleted=0", conn);
            da.Fill(dt);
            dataGrid.DataSource = dt;
            dataGrid.Columns[0].Visible = false;
            dataGrid.Columns[1].HeaderText = "Ad";
            dataGrid.Columns[2].HeaderText = "Soyad";
            dataGrid.Columns[3].HeaderText = "E-Posta";
            dataGrid.Columns[4].HeaderText = "Bölüm";
            dataGrid.Columns[5].HeaderText = "Fakülte";
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

        public static void listUndepositBooksConcatAuthorNameDataToTable(DataGridView dataGrid, MySqlConnection conn, string studentId)
        {
            DataTable dt = new DataTable();

            MySqlCommand command = new MySqlCommand("SELECT DepositBook.id, Student.firstName, Student.lastName, Book.id, Book.bookName, CONCAT( Author.firstName, ' ', Author.lastName ) AS authorFullName FROM DepositBook INNER JOIN Student ON DepositBook.studentId = Student.id INNER JOIN Book ON DepositBook.bookId = Book.id INNER JOIN Author ON Book.authorId = Author.id WHERE Student.id=@p1 AND DepositBook.status=0", conn);
            command.Parameters.AddWithValue("@p1", studentId);
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            da.Fill(dt);
            dataGrid.DataSource = dt;
            dataGrid.Columns[1].Visible = false;
            dataGrid.Columns[0].Visible = false;
            dataGrid.Columns[2].Visible = false;
            dataGrid.Columns[3].Visible = false;
            dataGrid.Columns[4].HeaderText = "Kitap Adı";
            dataGrid.Columns[5].HeaderText = "Yazar";
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

        public static void listDepositBooksConcatAuthorNameDataToTable(DataGridView dataGrid, MySqlConnection conn, string studentId)
        {
            DataTable dt = new DataTable();

            MySqlCommand command = new MySqlCommand("SELECT DepositBook.id, Student.firstName, Student.lastName, Book.id, Book.bookName, CONCAT( Author.firstName, ' ', Author.lastName ) AS authorFullName FROM DepositBook INNER JOIN Student ON DepositBook.studentId = Student.id INNER JOIN Book ON DepositBook.bookId = Book.id INNER JOIN Author ON Book.authorId = Author.id WHERE Student.id=@p1 AND DepositBook.status=1", conn);
            command.Parameters.AddWithValue("@p1", studentId);
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            da.Fill(dt);
            dataGrid.DataSource = dt;
            dataGrid.Columns[1].Visible = false;
            dataGrid.Columns[0].Visible = false;
            dataGrid.Columns[2].Visible = false;
            dataGrid.Columns[3].Visible = false;
            dataGrid.Columns[4].HeaderText = "Kitap Adı";
            dataGrid.Columns[5].HeaderText = "Yazar";
            dataGrid.ColumnHeadersVisible = false;
            dataGrid.RowHeadersVisible = false;
            dataGrid.DefaultCellStyle.Font = new Font("Nirmala UI", 13);

        }

        public static void listInnerJoinAllBooksDataToTable(DataGridView dataGrid, MySqlConnection conn)
        {
            DataTable dt = new DataTable();

            MySqlCommand command = new MySqlCommand("SELECT Book.id, Book.bookName, CONCAT( Author.firstName, ' ', Author.lastName ) AS authorFullName, Publisher.name'publisherName', Language.language, Category.name'categoryName', Book.pageCount, Book.isbnNumber, Book.publishDate, Book.stockCount, Location.shelf, Book.interpreter'interpreterName', Book.createdAt FROM Book INNER JOIN Language ON Book.languageId = Language.id INNER JOIN Author ON Book.authorId = Author.id INNER JOIN Category ON Book.categoryId = Category.id INNER JOIN Publisher ON Book.publisherId = Publisher.id INNER JOIN Location ON Book.locationId = Location.id WHERE Book.deleted=0", conn);
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            da.Fill(dt);
            dataGrid.DataSource = dt;
            dataGrid.Columns[0].Visible = false;
            dataGrid.Columns[1].HeaderText = "Kitap Adı";
            dataGrid.Columns[2].HeaderText = "Yazar";
            dataGrid.Columns[3].HeaderText = "Yayınevi";
            dataGrid.Columns[4].HeaderText = "Dil";
            dataGrid.Columns[5].HeaderText = "Kategori";
            dataGrid.Columns[6].HeaderText = "Sayfa Sayısı";
            dataGrid.Columns[7].HeaderText = "ISBN";
            dataGrid.Columns[8].HeaderText = "Yayın Tarihi";
            dataGrid.Columns[9].HeaderText = "Stok";
            dataGrid.Columns[10].HeaderText = "Konum";
            dataGrid.Columns[11].HeaderText = "Çevirmen";
            dataGrid.Columns[12].HeaderText = "Oluşturulma Tarihi";
            dataGrid.RowHeadersVisible = false;
            dataGrid.DefaultCellStyle.Font = new Font("Nirmala UI", 13);

        }

        //?
        public static void listInnerJoinAllDepositBooksDataToTableByStudentId(DataGridView dataGrid, MySqlConnection conn)
        {
            DataTable dt = new DataTable();

            MySqlCommand command = new MySqlCommand("SELECT Book.id, Book.bookName, CONCAT( Author.firstName, ' ', Author.lastName ) AS authorFullName, Publisher.name'publisherName', Language.language, Category.name'categoryName', Book.pageCount, Book.isbnNumber, Book.publishDate, Book.stockCount, Location.shelf, Book.interpreter'interpreterName', Book.createdAt FROM Book INNER JOIN Language ON Book.languageId = Language.id INNER JOIN Author ON Book.authorId = Author.id INNER JOIN Category ON Book.categoryId = Category.id INNER JOIN Publisher ON Book.publisherId = Publisher.id INNER JOIN Location ON Book.locationId = Location.id WHERE Book.deleted=0 AND DepositBook.studentId", conn);
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            da.Fill(dt);
            dataGrid.DataSource = dt;
            dataGrid.Columns[0].Visible = false;
            dataGrid.Columns[1].HeaderText = "Kitap Adı";
            dataGrid.Columns[2].HeaderText = "Yazar";
            dataGrid.Columns[3].HeaderText = "Yayınevi";
            dataGrid.Columns[4].HeaderText = "Dil";
            dataGrid.Columns[5].HeaderText = "Kategori";
            dataGrid.Columns[6].HeaderText = "Sayfa Sayısı";
            dataGrid.Columns[7].HeaderText = "ISBN";
            dataGrid.Columns[8].HeaderText = "Yayın Tarihi";
            dataGrid.Columns[9].HeaderText = "Stok";
            dataGrid.Columns[10].HeaderText = "Konum";
            dataGrid.Columns[11].HeaderText = "Çevirmen";
            dataGrid.Columns[12].HeaderText = "Oluşturulma Tarihi";
            dataGrid.RowHeadersVisible = false;
            dataGrid.DefaultCellStyle.Font = new Font("Nirmala UI", 13);

        }

        public static void listInnerJoinAllStudentsDataToTable(DataGridView dataGrid, MySqlConnection conn)
        {
            DataTable dt = new DataTable();

            MySqlCommand command = new MySqlCommand("SELECT Student.id, CONCAT( Student.firstName, ' ', Student.lastName ) AS studentFullName, Student.number, Student.eMail, Department.name, Student.createdAt, Faculty.name FROM Student INNER JOIN Department ON Student.departmentId = Department.id INNER JOIN Faculty ON Student.facultyId=Faculty.id WHERE Student.deleted=0", conn);
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            da.Fill(dt);
            dataGrid.DataSource = dt;
            dataGrid.Columns[0].Visible = false;
            dataGrid.Columns[1].HeaderText = "Öğrenci";
            dataGrid.Columns[2].HeaderText = "Okul No";
            dataGrid.Columns[3].HeaderText = "Eposta";
            dataGrid.Columns[4].HeaderText = "Bölüm";
            dataGrid.Columns[6].HeaderText = "Fakülte";
            dataGrid.Columns[5].HeaderText = "Kayıt Tarihi";
            dataGrid.RowHeadersVisible = false;
            dataGrid.DefaultCellStyle.Font = new Font("Nirmala UI", 13);

        }

        //İstenilen tarihler arası
        public static void listInnerJoinAllStudentsDataToTableBetweenDates(DataGridView dataGrid, MySqlConnection conn, DateTime startDate, DateTime endDate)
        {
            DataTable dt = new DataTable();

            MySqlCommand command = new MySqlCommand("SELECT Student.id, CONCAT( Student.firstName, ' ', Student.lastName ) AS studentFullName, Student.number, Student.eMail, Department.name, Student.createdAt, Faculty.name FROM Student INNER JOIN Department ON Student.departmentId = Department.id INNER JOIN Faculty ON Student.facultyId=Faculty.id WHERE Student.deleted=0 AND DATE(Student.createdAt) BETWEEN @p1 AND @p2 ORDER BY Student.createdAt ASC", conn);
            command.Parameters.AddWithValue("@p1", startDate);
            command.Parameters.AddWithValue("@p2", endDate);
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

        public static void listInnerJoinAllPersonnelsDataToTableBetweenDates(DataGridView dataGrid, MySqlConnection conn, DateTime startDate, DateTime endDate)
        {
            DataTable dt = new DataTable();
            MySqlCommand command = new MySqlCommand("SELECT Personnel.id, Personnel.firstName, Personnel.lastName, Personnel.eMail, Department.name'departmentName', Faculty.name'facultyName' FROM Personnel INNER JOIN Department ON Personnel.departmentId = Department.id INNER JOIN Faculty ON Personnel.facultyId = Faculty.id WHERE Personnel.deleted=0 AND DATE(Personnels.createdAt) BETWEEN @p1 AND @p2 ORDER BY Personnels.createdAt ASC", conn);
            command.Parameters.AddWithValue("@p1", startDate);
            command.Parameters.AddWithValue("@p2", endDate);
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            da.Fill(dt);
            dataGrid.DataSource = dt;
            dataGrid.Columns[0].Visible = false;
            dataGrid.Columns[2].HeaderText = "";
            dataGrid.Columns[4].Visible = false;
            dataGrid.Columns[5].Visible = false;
            dataGrid.ColumnHeadersVisible = false;
            dataGrid.RowHeadersVisible = false;
            dataGrid.DefaultCellStyle.Font = new Font("Nirmala UI", 13);
        }

        public static void listInnerJoinAllStudentsNotConcatDataToTable(DataGridView dataGrid, MySqlConnection conn)
        {
            DataTable dt = new DataTable();

            MySqlCommand command = new MySqlCommand("SELECT Student.id, Student.firstName, Student.lastName, Student.number, Student.eMail, Department.name, Faculty.name FROM Student INNER JOIN Department ON Student.departmentId = Department.id INNER JOIN Faculty ON Student.facultyId=Faculty.id WHERE Student.deleted=0", conn);
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            da.Fill(dt);
            dataGrid.DataSource = dt;
            dataGrid.Columns[0].Visible = false;
            dataGrid.Columns[1].HeaderText = "Ad";
            dataGrid.Columns[2].HeaderText = "Soyad";
            dataGrid.Columns[3].HeaderText = "Okul No";
            dataGrid.Columns[4].HeaderText = "Eposta";
            dataGrid.Columns[5].HeaderText = "Bölüm";
            dataGrid.Columns[6].HeaderText = "Fakülte";
            dataGrid.RowHeadersVisible = false;
            dataGrid.DefaultCellStyle.Font = new Font("Nirmala UI", 13);

        }

        public static void listInnerJoinSomeBookDataToTable(DataGridView dataGrid, MySqlConnection conn)
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT Book.id, Book.bookName, CONCAT( Author.firstName, ' ', Author.lastName ) AS authorFullName, Publisher.name, Language.language, Category.name, Book.pageCount, Book.isbnNumber, Book.publishDate, Book.stockCount, Location.shelf, Book.interpreter, Book.createdAt, Book.fixtureNo FROM Book INNER JOIN Language ON Book.languageId = Language.id INNER JOIN Author ON Book.authorId = Author.id INNER JOIN Category ON Book.categoryId = Category.id INNER JOIN Publisher ON Book.publisherId = Publisher.id INNER JOIN Location ON Book.locationId = Location.id WHERE Book.deleted=0", conn);
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
            dataGrid.Columns[13].Visible = false;
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

        //İstenilen tarihler arası
        public static void listInnerJoinAllBooksDataToTableBetweenDates(DataGridView dataGrid, MySqlConnection conn, DateTime startDate, DateTime endDate)
        {
            DataTable dt = new DataTable();

            MySqlCommand command = new MySqlCommand("SELECT Book.id, Book.bookName, CONCAT( Author.firstName, ' ', Author.lastName ) AS authorFullName, Publisher.name'publisherName', Language.language, Category.name'categoryName', Book.pageCount, Book.isbnNumber, Book.publishDate, Book.stockCount, Location.shelf, Book.interpreter'interpreterName', Book.createdAt FROM Book INNER JOIN Language ON Book.languageId = Language.id INNER JOIN Author ON Book.authorId = Author.id INNER JOIN Category ON Book.categoryId = Category.id INNER JOIN Publisher ON Book.publisherId = Publisher.id INNER JOIN Location ON Book.locationId = Location.id WHERE Book.deleted=0 AND DATE(Book.createdAt) BETWEEN @p1 AND @p2 ORDER BY Book.createdAt ASC", conn);
            command.Parameters.AddWithValue("@p1", startDate);
            command.Parameters.AddWithValue("@p2", endDate);
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            da.Fill(dt);
            dataGrid.DataSource = dt;
            dataGrid.Columns[0].Visible = false;
            dataGrid.Columns[1].HeaderText = "Kitap Adı";
            dataGrid.Columns[2].HeaderText = "Yazar";
            dataGrid.Columns[3].HeaderText = "Yayınevi";
            dataGrid.Columns[4].HeaderText = "Dil";
            dataGrid.Columns[5].HeaderText = "Kategori";
            dataGrid.Columns[6].HeaderText = "Sayfa Sayısı";
            dataGrid.Columns[7].HeaderText = "ISBN";
            dataGrid.Columns[8].HeaderText = "Yayın Tarihi";
            dataGrid.Columns[9].HeaderText = "Stok";
            dataGrid.Columns[10].HeaderText = "Konum";
            dataGrid.Columns[11].HeaderText = "Çevirmen";
            dataGrid.Columns[12].HeaderText = "Oluşturulma Tarihi";
            dataGrid.RowHeadersVisible = false;
            dataGrid.DefaultCellStyle.Font = new Font("Nirmala UI", 13);

        }

    }
}
