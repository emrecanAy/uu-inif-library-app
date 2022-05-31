using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql;
using MySql.Data.MySqlClient;
using uu_library_app.Core.Helpers;
using uu_library_app.DataAccess.Abstract;
using uu_library_app.Entity.Concrete;

namespace uu_library_app.DataAccess.Concrete
{

    public class DepositBookDal : IDepositBookDal
    {
        MySqlConnection conn = new MySqlConnection(DbConnection.connectionString);
        public void Add(DepositBook depositBook)
        {
            conn.Open();
            MySqlCommand commandToAdd = new MySqlCommand("INSERT INTO DepositBook (id,depositDate,studentId,bookId) VALUES (@p1, @p2, @p3, @p4)", conn);
            try
            {
                commandToAdd.Parameters.AddWithValue("@p1", depositBook.Id);
                commandToAdd.Parameters.AddWithValue("@p2", depositBook.DepositDate);
                commandToAdd.Parameters.AddWithValue("@p3", depositBook.StudentId);
                commandToAdd.Parameters.AddWithValue("@p4", depositBook.BookId);
                commandToAdd.ExecuteNonQuery();
                Console.WriteLine("Başarıyla eklendi!");
            }
            catch (Exception)
            {
                Console.WriteLine("Hatalı ekleme!");
                throw;
            }

            conn.Close();
        }

        public void Delete(DepositBook depositBook)
        {
            conn.Open();
            try
            {
                MySqlCommand commandToUpdate = new MySqlCommand("UPDATE DepositBook SET deleted=1 WHERE id=@p1 ", conn);
                commandToUpdate.Parameters.AddWithValue("@p1", depositBook.Id);
                commandToUpdate.ExecuteNonQuery();

            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }

            conn.Close();
        }
        
        List<DepositBook> depositBooks = new List<DepositBook>();
        
        public List<DepositBook> getAll()
        {
            conn.Open();
            try
            {
                MySqlCommand commandToGetAll = new MySqlCommand("SELECT * FROM DepositBook WHERE deleted=false", conn);
                MySqlDataReader reader = commandToGetAll.ExecuteReader();
                while (reader.Read())
                {
                    DepositBook depositBook = new DepositBook();
                    depositBook.Id = reader[0].ToString();
                    depositBook.DepositDate = Convert.ToDateTime(reader[1]);
                    depositBook.StudentId = reader[2].ToString();
                    depositBook.BookId = reader[3].ToString();   
                    depositBooks.Add(depositBook);
                }
                conn.Close();
                return depositBooks;
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }
        }

        List<DepositBook> undepositedBooks = new List<DepositBook>();
        public List<DepositBook> getAllUndeposited()
        {
            conn.Open();
            try
            {
                MySqlCommand commandToGetAll = new MySqlCommand("SELECT * FROM DepositBook WHERE deleted=false AND status=0", conn);
                MySqlDataReader reader = commandToGetAll.ExecuteReader();
                while (reader.Read())
                {
                    DepositBook depositBook = new DepositBook();
                    depositBook.Id = reader[0].ToString();
                    depositBook.DepositDate = Convert.ToDateTime(reader[1]);
                    depositBook.StudentId = reader[2].ToString();
                    depositBook.BookId = reader[3].ToString();
                    undepositedBooks.Add(depositBook);
                }
                conn.Close();
                return undepositedBooks;
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }
        }

        List<DepositBook> depositedBooks = new List<DepositBook>();
        public List<DepositBook> getAllDeposited()
        {
            conn.Open();
            try
            {
                MySqlCommand commandToGetAll = new MySqlCommand("SELECT * FROM DepositBook WHERE deleted=false AND status=1", conn);
                MySqlDataReader reader = commandToGetAll.ExecuteReader();
                while (reader.Read())
                {
                    DepositBook depositBook = new DepositBook();
                    depositBook.Id = reader[0].ToString();
                    depositBook.DepositDate = Convert.ToDateTime(reader[1]);
                    depositBook.StudentId = reader[2].ToString();
                    depositBook.BookId = reader[3].ToString();
                    depositedBooks.Add(depositBook);
                }
                conn.Close();
                return depositedBooks;
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }
        }

        BindingList<DepositBook> deposits = new BindingList<DepositBook>();
        public List<DepositBook> findAllByStudentId(string studentId)
        {
            conn.Open();
            try
            {
                MySqlCommand commandToGetAll = new MySqlCommand("SELECT * FROM DepositBook WHERE deleted=false AND studentId=@p1", conn);
                commandToGetAll.Parameters.AddWithValue("@p1", studentId);
                MySqlDataReader reader = commandToGetAll.ExecuteReader();
                while (reader.Read())
                {
                    DepositBook depositBook = new DepositBook();
                    depositBook.Id = reader[0].ToString();
                    depositBook.DepositDate = Convert.ToDateTime(reader[1]);
                    depositBook.StudentId = reader[2].ToString();
                    depositBook.BookId = reader[3].ToString();
                    depositBooks.Add(depositBook);
                }
                conn.Close();
                return depositBooks;
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }
        }

        public DepositBook findById(string id)
        {

            conn.Open();
            try
            {
                DepositBook depositBook = new DepositBook();
                MySqlCommand commandToGetAll = new MySqlCommand("SELECT * FROM DepositBook WHERE deleted=false AND id=@p1", conn);
                commandToGetAll.Parameters.AddWithValue("@p1", id);
                MySqlDataReader reader = commandToGetAll.ExecuteReader();
                while (reader.Read())
                {
                    depositBook.Id = reader[0].ToString();
                    depositBook.DepositDate = Convert.ToDateTime(reader[1]);
                    depositBook.StudentId = reader[2].ToString();
                    depositBook.BookId = reader[3].ToString();
                }
                conn.Close();
                return depositBook;
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }
        }



        public void Update(DepositBook depositBook)
        {
            conn.Open();
            try
            {
                MySqlCommand commandToUpdate = new MySqlCommand("UPDATE DepositBook SET depositDate=@p2, studentId=@p3, bookId=@p4 WHERE id=@p1 ", conn);
                commandToUpdate.Parameters.AddWithValue("@p1", depositBook.Id);
                commandToUpdate.Parameters.AddWithValue("@p2", depositBook.DepositDate);
                commandToUpdate.Parameters.AddWithValue("@p3", depositBook.StudentId);
                commandToUpdate.Parameters.AddWithValue("@p4", depositBook.BookId);
                commandToUpdate.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }

            conn.Close();


        }

        public void depositBook(string id)
        {
            conn.Open();
            try
            {
                MySqlCommand commandToUpdate = new MySqlCommand("UPDATE DepositBook SET status=1 WHERE id=@p1 ", conn);
                commandToUpdate.Parameters.AddWithValue("@p1", id);
                commandToUpdate.ExecuteNonQuery();

            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }

            conn.Close();
        }
    }
}

