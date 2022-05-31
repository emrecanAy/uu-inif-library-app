﻿using MySql.Data.MySqlClient;
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
        public static DataTable listInnerJoinAllStudentsDataToTable(DataGridView dataGrid, MySqlConnection conn)
        {
            DataTable dt = new DataTable();
            MySqlCommand command = new MySqlCommand("SELECT Student.id, CONCAT( Student.firstName, ' ', Student.lastName ) AS AdSoyad, Student.number'OkulNo', Student.eMail'E-Posta', Department.name'Bolum', Student.createdAt'OlusturulmaTarihi', Faculty.name'Fakulte' FROM Student INNER JOIN Department ON Student.departmentId = Department.id INNER JOIN Faculty ON Student.facultyId=Faculty.id WHERE Student.deleted=0", conn);
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            da.Fill(dt);
            return dt;
        }

        public static void listUndepositBooksConcatAuthorNameDataToTable(DataGridView dataGrid, MySqlConnection conn)
        {
            DataTable dt = new DataTable();

            MySqlCommand command = new MySqlCommand("SELECT DepositBook.id, CONCAT(Student.firstName, Student.lastName) as Ogrenci, Book.id, Book.bookName, CONCAT( Author.firstName, ' ', Author.lastName ) AS authorFullName FROM DepositBook INNER JOIN Student ON DepositBook.studentId = Student.id INNER JOIN Book ON DepositBook.bookId = Book.id INNER JOIN Author ON Book.authorId = Author.id WHERE DepositBook.status=0", conn);
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            da.Fill(dt);

        }
        public static List<DepositBook> getExpiredBooks()
        {
            List<DepositBook> expiredBooks = new List<DepositBook>();
            List<DepositBook> depositBooksList = depositBookManager.getAll();
            foreach (DepositBook depositBook in depositBooksList)
            {
                TimeSpan ts = depositBook.DepositDate - DateTime.Now;
                string howManyDaysPast = ts.Days.ToString();
                if (howManyDaysPast.StartsWith("-"))
                {
                    int daysPast = Convert.ToInt32(howManyDaysPast);
                    if (daysPast == -settingsManager.getSettings().DepositDay)
                    {
                        expiredBooks.Add(depositBook);
                    }
                }
            }
            return expiredBooks;
        }

        public static List<DepositBookDto> getExpiredBookWithNames()
        {
            List<DataTable> dataTableList = new List<DataTable>();
            List<DepositBookDto> depositBookDtoList = new List<DepositBookDto>();
            DataTable dt = new DataTable();
            foreach (DepositBook depositBook in getExpiredBooks())
            {
                conn.Open();
                DepositBookDto depositBookDto = new DepositBookDto();
                MySqlCommand command = new MySqlCommand("SELECT DepositBook.id, Student.number'OkulNo', CONCAT(Student.firstName,' ',Student.lastName) as Ogrenci, Book.bookName'Kitap', CONCAT(Author.firstName,' ',Author.lastName) as Yazar, DepositBook.createdAt FROM DepositBook INNER JOIN Student ON DepositBook.studentId = Student.id INNER JOIN Book ON DepositBook.bookId = Book.id INNER JOIN Author ON Book.authorId = Author.id WHERE DepositBook.id=@p1", conn);
                command.Parameters.AddWithValue("@p1", depositBook.Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    depositBookDto.Id = reader[0].ToString();
                    depositBookDto.OgrenciNo = reader[1].ToString();
                    depositBookDto.Ogrenci = reader[2].ToString();
                    depositBookDto.Kitap = reader[3].ToString();
                    depositBookDto.Yazar = reader[4].ToString();
                    depositBookDto.OlusturulmaTarihi = Convert.ToDateTime(reader[5]);
                }
                depositBookDtoList.Add(depositBookDto);
                conn.Close();
            }
            return depositBookDtoList;
        }

        public static DataTable listDepositBooksDataToTable(DataGridView dataGrid, MySqlConnection conn, string depositBookId)
        {
            DataTable dt = new DataTable();

            MySqlCommand command = new MySqlCommand("SELECT DepositBook.id, Student.firstName, Student.lastName, Book.id, Book.bookName, Author.firstName'authorFirstName', Author.lastName'authorLastName' FROM DepositBook INNER JOIN Student ON DepositBook.studentId = Student.id INNER JOIN Book ON DepositBook.bookId = Book.id INNER JOIN Author ON Book.authorId = Author.id WHERE DepositBook.id=@p1", conn);
            command.Parameters.AddWithValue("@p1", depositBookId);
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            da.Fill(dt);
            return dt;
        }

    }
}