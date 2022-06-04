using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.Core.Helpers;
using uu_library_app.DataAccess.Abstract;
using uu_library_app.Entity.Concrete;

namespace uu_library_app.DataAccess.Concrete
{
    public class BookDal : IBookDal
    {
        MySqlConnection conn = new MySqlConnection(DbConnection.connectionString);
        public void Add(Book book)
        {
            try
            {
                conn.Open();

                MySqlCommand commandToAdd = new MySqlCommand("INSERT INTO Book (id, bookName, languageId, authorId, categoryId, publisherId, pageCount, isbnNumber, publishDate, publishCount, stockCount, locationId, status, interpreter, fixtureNo) VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12, @p13, @p14, @p15)", conn);
                try
                {
                    commandToAdd.Parameters.AddWithValue("@p1", book.Id);
                    commandToAdd.Parameters.AddWithValue("@p2", book.BookName);
                    commandToAdd.Parameters.AddWithValue("@p3", book.LanguageId);
                    commandToAdd.Parameters.AddWithValue("@p4", book.AuthorId);
                    commandToAdd.Parameters.AddWithValue("@p5", book.CategoryId);
                    commandToAdd.Parameters.AddWithValue("@p6", book.PublisherId);
                    commandToAdd.Parameters.AddWithValue("@p7", book.PageCount);
                    commandToAdd.Parameters.AddWithValue("@p8", book.IsbnNumber);
                    commandToAdd.Parameters.AddWithValue("@p9", book.PublishDate);
                    commandToAdd.Parameters.AddWithValue("@p10", book.PublishCount);
                    commandToAdd.Parameters.AddWithValue("@p11", book.StockCount);
                    commandToAdd.Parameters.AddWithValue("@p12", book.LocationId);
                    commandToAdd.Parameters.AddWithValue("@p13", book.Status);
                    commandToAdd.Parameters.AddWithValue("@p14", book.Interpreter);
                    commandToAdd.Parameters.AddWithValue("@p15", book.FixtureNo);
                    commandToAdd.ExecuteNonQuery();
                    Console.WriteLine("Successfully added!");
                }
                catch (Exception)
                {
                    Console.WriteLine("Something went wrong!");
                }

                conn.Close();
            }
            catch (Exception)
            {
                throw new Exception("Lütfen internet bağlantınızı kontrol ederek tekrar deneyiniz. Sorunun devam etmesi durumunda bir yetkiliyle iletişime geçiniz.");
            }
            
        }

        public void Update(Book book)
        {
            conn.Open();

            MySqlCommand commandToUpdate = new MySqlCommand("UPDATE Book SET bookName=@p2, languageId=@p3, authorId=@p4, categoryId=@p5, publisherId=@p6, pageCount=@p7, isbnNumber=@p8, publishDate=@p9, publishCount=@p10, stockCount=@p11, locationId=@p12, status=@p13, interpreter=@p14, fixtureNo=@p15 WHERE id=@p1", conn);
            try
            {
                commandToUpdate.Parameters.AddWithValue("@p1", book.Id);
                commandToUpdate.Parameters.AddWithValue("@p2", book.BookName);
                commandToUpdate.Parameters.AddWithValue("@p3", book.LanguageId);
                commandToUpdate.Parameters.AddWithValue("@p4", book.AuthorId);
                commandToUpdate.Parameters.AddWithValue("@p5", book.CategoryId);
                commandToUpdate.Parameters.AddWithValue("@p6", book.PublisherId);
                commandToUpdate.Parameters.AddWithValue("@p7", book.PageCount);
                commandToUpdate.Parameters.AddWithValue("@p8", book.IsbnNumber);
                commandToUpdate.Parameters.AddWithValue("@p9", book.PublishDate);
                commandToUpdate.Parameters.AddWithValue("@p10", book.PublishCount);
                commandToUpdate.Parameters.AddWithValue("@p11", book.StockCount);
                commandToUpdate.Parameters.AddWithValue("@p12", book.LocationId);
                commandToUpdate.Parameters.AddWithValue("@p13", book.Status);
                commandToUpdate.Parameters.AddWithValue("@p14", book.Interpreter);
                commandToUpdate.Parameters.AddWithValue("@p15", book.FixtureNo);
                commandToUpdate.ExecuteNonQuery();
                Console.WriteLine("Successfully added!");
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }

            conn.Close();
        }

        public void Delete(Book book)
        {
            conn.Open();
            try
            {
                MySqlCommand commandToSetDeleted = new MySqlCommand("UPDATE Book SET deleted=1 WHERE id=@p1 ", conn);
                commandToSetDeleted.Parameters.AddWithValue("@p1", book.id);
                commandToSetDeleted.ExecuteNonQuery();

            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }

            conn.Close();
        }

        List<Book> books=new List<Book>();
        
        public List<Book> getAll()
        {
            conn.Open();
            try
            {
                MySqlCommand commandToGetAll = new MySqlCommand("SELECT * FROM Book WHERE deleted=0", conn);
                MySqlDataReader reader = commandToGetAll.ExecuteReader();
                while (reader.Read())
                {
                    Book book = new Book();
                    book.Id = reader[0].ToString();
                    book.LanguageId = reader[2].ToString();
                    book.AuthorId = reader[3].ToString();
                    book.CategoryId = reader[4].ToString();
                    book.PublisherId = reader[5].ToString();
                    book.PageCount = Convert.ToInt32(reader[6]);
                    book.IsbnNumber = reader[7].ToString();
                    book.PublishDate = Convert.ToDateTime(reader[8]);
                    book.PublishCount = Convert.ToInt32(reader[9]);
                    book.StockCount = Convert.ToInt32(reader[10]);
                    book.LocationId = reader[11].ToString();
                    book.BookName = reader[1].ToString();
                    book.Status = Convert.ToBoolean(reader[12]);
                    book.CreatedAt = Convert.ToDateTime(reader[13]);
                    book.Interpreter = reader[14].ToString();
                    book.Deleted = Convert.ToBoolean(reader[15]);
                    book.FixtureNo = reader[16].ToString();
                    books.Add(book);
                }
                conn.Close();
                return books;
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }  
        }

        public List<Book> getAllByCategory(string categoryId)
        {
            conn.Open();
            try
            {
                MySqlCommand commandToGetAllByCategory = new MySqlCommand("SELECT * FROM Book WHERE deleted=0 AND categoryId=@p1", conn);
                commandToGetAllByCategory.Parameters.AddWithValue("@p1", categoryId);
                MySqlDataReader reader = commandToGetAllByCategory.ExecuteReader();
                while (reader.Read())
                {
                    Book book = new Book();
                    book.Id = reader[0].ToString();
                    book.LanguageId = reader[1].ToString();
                    book.AuthorId = reader[2].ToString();
                    book.CategoryId = reader[3].ToString();
                    book.PublisherId = reader[4].ToString();
                    book.PageCount = Convert.ToInt32(reader[5]);
                    book.IsbnNumber = reader[6].ToString();
                    book.PublishDate = Convert.ToDateTime(reader[7]);
                    book.PublishCount = Convert.ToInt32(reader[8]);
                    book.StockCount = Convert.ToInt32(reader[9]);
                    book.LocationId = reader[10].ToString();
                    book.BookName = reader[11].ToString();
                    book.Status = Convert.ToBoolean(reader[12]);
                    book.CreatedAt = Convert.ToDateTime(reader[13]);
                    book.Interpreter = reader[14].ToString();
                    book.Deleted = Convert.ToBoolean(reader[15]);
                    book.FixtureNo = reader[16].ToString();
                    books.Add(book);
                }
                conn.Close();
                return books;
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }   
        }

        public List<Book> getAllSortedByAddedDate()
        {
            conn.Open();
            try
            {
                MySqlCommand commandToGetAll = new MySqlCommand("SELECT * FROM Book WHERE deleted=0 ORDER BY createdAt ASC", conn);
                MySqlDataReader reader = commandToGetAll.ExecuteReader();
                while (reader.Read())
                {
                    Book book = new Book();
                    book.Id = reader[0].ToString();
                    book.LanguageId = reader[1].ToString();
                    book.AuthorId = reader[2].ToString();
                    book.CategoryId = reader[3].ToString();
                    book.PublisherId = reader[4].ToString();
                    book.PageCount = Convert.ToInt32(reader[5]);
                    book.IsbnNumber = reader[6].ToString();
                    book.PublishDate = Convert.ToDateTime(reader[7]);
                    book.PublishCount = Convert.ToInt32(reader[8]);
                    book.StockCount = Convert.ToInt32(reader[9]);
                    book.LocationId = reader[10].ToString();
                    book.BookName = reader[11].ToString();
                    book.Status = Convert.ToBoolean(reader[12]);
                    book.CreatedAt = Convert.ToDateTime(reader[13]);
                    book.Interpreter = reader[14].ToString();
                    book.Deleted = Convert.ToBoolean(reader[15]);
                    book.FixtureNo = reader[16].ToString();
                    books.Add(book);
                }
                conn.Close();
                return books;
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }
        }

        public List<Book> getAllSortedByName()
        {
            conn.Open();
            try
            {
                MySqlCommand commandToGetAll = new MySqlCommand("SELECT * FROM Book WHERE deleted=0 ORDER BY bookName ASC", conn);
                MySqlDataReader reader = commandToGetAll.ExecuteReader();
                while (reader.Read())
                {
                    Book book = new Book();
                    book.Id = reader[0].ToString();
                    book.LanguageId = reader[1].ToString();
                    book.AuthorId = reader[2].ToString();
                    book.CategoryId = reader[3].ToString();
                    book.PublisherId = reader[4].ToString();
                    book.PageCount = Convert.ToInt32(reader[5]);
                    book.IsbnNumber = reader[6].ToString();
                    book.PublishDate = Convert.ToDateTime(reader[7]);
                    book.PublishCount = Convert.ToInt32(reader[8]);
                    book.StockCount = Convert.ToInt32(reader[9]);
                    book.LocationId = reader[10].ToString();
                    book.BookName = reader[11].ToString();
                    book.Status = Convert.ToBoolean(reader[12]);
                    book.CreatedAt = Convert.ToDateTime(reader[13]);
                    book.Interpreter = reader[14].ToString();
                    book.Deleted = Convert.ToBoolean(reader[15]);
                    book.FixtureNo = reader[16].ToString();
                    books.Add(book);
                }
                conn.Close();
                return books;
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }
        }

        
        public Book getById(string id)
        {
            
            conn.Open();
            try
            {
                Book book = new Book();
                MySqlCommand commandToGetAllByCategory = new MySqlCommand("SELECT * FROM Book WHERE id=@p1 AND deleted=0", conn);
                commandToGetAllByCategory.Parameters.AddWithValue("@p1", id);
                MySqlDataReader reader = commandToGetAllByCategory.ExecuteReader();
                while (reader.Read())
                {               
                    book.Id = reader[0].ToString();
                    book.LanguageId = reader[2].ToString();
                    book.AuthorId = reader[3].ToString();
                    book.CategoryId = reader[4].ToString();
                    book.PublisherId = reader[5].ToString();
                    book.PageCount = Convert.ToInt32(reader[6]);
                    book.IsbnNumber = reader[7].ToString();
                    book.PublishDate = Convert.ToDateTime(reader[8]);
                    book.PublishCount = Convert.ToInt32(reader[9]);
                    book.StockCount = Convert.ToInt32(reader[10]);
                    book.LocationId = reader[11].ToString();
                    book.BookName = reader[1].ToString();
                    book.Status = Convert.ToBoolean(reader[12]);
                    book.CreatedAt = Convert.ToDateTime(reader[13]);
                    book.Interpreter = reader[14].ToString();
                    book.Deleted = Convert.ToBoolean(reader[15]);
                    book.FixtureNo = reader[16].ToString();
                }
                conn.Close();
                return book;
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }

            
        }

        public Book getByCategoryId(string categoryId)
        {
            conn.Open();
            try
            {
                Book book = new Book();
                MySqlCommand commandToGetAllByCategory = new MySqlCommand("SELECT * FROM Book WHERE categoryId=@p1 AND deleted=0", conn);
                commandToGetAllByCategory.Parameters.AddWithValue("@p1", categoryId);
                MySqlDataReader reader = commandToGetAllByCategory.ExecuteReader();
                while (reader.Read())
                {
                    book.Id = reader[0].ToString();
                    book.LanguageId = reader[2].ToString();
                    book.AuthorId = reader[3].ToString();
                    book.CategoryId = reader[4].ToString();
                    book.PublisherId = reader[5].ToString();
                    book.PageCount = Convert.ToInt32(reader[6]);
                    book.IsbnNumber = reader[7].ToString();
                    book.PublishDate = Convert.ToDateTime(reader[8]);
                    book.PublishCount = Convert.ToInt32(reader[9]);
                    book.StockCount = Convert.ToInt32(reader[10]);
                    book.LocationId = reader[11].ToString();
                    book.BookName = reader[1].ToString();
                    book.Status = Convert.ToBoolean(reader[12]);
                    book.CreatedAt = Convert.ToDateTime(reader[13]);
                    book.Interpreter = reader[14].ToString();
                    book.Deleted = Convert.ToBoolean(reader[15]);
                    book.FixtureNo = reader[16].ToString();
                }
                conn.Close();
                return book;
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }
        }

        public Book getByAuthorId(string authorId)
        {
            conn.Open();
            try
            {
                Book book = new Book();
                MySqlCommand commandToGetAllByCategory = new MySqlCommand("SELECT * FROM Book WHERE authorId=@p1 AND deleted=0", conn);
                commandToGetAllByCategory.Parameters.AddWithValue("@p1", authorId);
                MySqlDataReader reader = commandToGetAllByCategory.ExecuteReader();
                while (reader.Read())
                {
                    book.Id = reader[0].ToString();
                    book.LanguageId = reader[2].ToString();
                    book.AuthorId = reader[3].ToString();
                    book.CategoryId = reader[4].ToString();
                    book.PublisherId = reader[5].ToString();
                    book.PageCount = Convert.ToInt32(reader[6]);
                    book.IsbnNumber = reader[7].ToString();
                    book.PublishDate = Convert.ToDateTime(reader[8]);
                    book.PublishCount = Convert.ToInt32(reader[9]);
                    book.StockCount = Convert.ToInt32(reader[10]);
                    book.LocationId = reader[11].ToString();
                    book.BookName = reader[1].ToString();
                    book.Status = Convert.ToBoolean(reader[12]);
                    book.CreatedAt = Convert.ToDateTime(reader[13]);
                    book.Interpreter = reader[14].ToString();
                    book.Deleted = Convert.ToBoolean(reader[15]);
                    book.FixtureNo = reader[16].ToString();
                }
                conn.Close();
                return book;
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }
        }

        public Book getByLanguageId(string languageId)
        {
            conn.Open();
            try
            {
                Book book = new Book();
                MySqlCommand commandToGetAllByCategory = new MySqlCommand("SELECT * FROM Book WHERE languageId=@p1 AND deleted=0", conn);
                commandToGetAllByCategory.Parameters.AddWithValue("@p1", languageId);
                MySqlDataReader reader = commandToGetAllByCategory.ExecuteReader();
                while (reader.Read())
                {
                    book.Id = reader[0].ToString();
                    book.LanguageId = reader[2].ToString();
                    book.AuthorId = reader[3].ToString();
                    book.CategoryId = reader[4].ToString();
                    book.PublisherId = reader[5].ToString();
                    book.PageCount = Convert.ToInt32(reader[6]);
                    book.IsbnNumber = reader[7].ToString();
                    book.PublishDate = Convert.ToDateTime(reader[8]);
                    book.PublishCount = Convert.ToInt32(reader[9]);
                    book.StockCount = Convert.ToInt32(reader[10]);
                    book.LocationId = reader[11].ToString();
                    book.BookName = reader[1].ToString();
                    book.Status = Convert.ToBoolean(reader[12]);
                    book.CreatedAt = Convert.ToDateTime(reader[13]);
                    book.Interpreter = reader[14].ToString();
                    book.Deleted = Convert.ToBoolean(reader[15]);
                    book.FixtureNo = reader[16].ToString();
                }
                conn.Close();
                return book;
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }
        }

        public Book getByLocationId(string locationId)
        {
            conn.Open();
            try
            {
                Book book = new Book();
                MySqlCommand commandToGetAllByCategory = new MySqlCommand("SELECT * FROM Book WHERE locationId=@p1 AND deleted=0", conn);
                commandToGetAllByCategory.Parameters.AddWithValue("@p1", locationId);
                MySqlDataReader reader = commandToGetAllByCategory.ExecuteReader();
                while (reader.Read())
                {
                    book.Id = reader[0].ToString();
                    book.LanguageId = reader[2].ToString();
                    book.AuthorId = reader[3].ToString();
                    book.CategoryId = reader[4].ToString();
                    book.PublisherId = reader[5].ToString();
                    book.PageCount = Convert.ToInt32(reader[6]);
                    book.IsbnNumber = reader[7].ToString();
                    book.PublishDate = Convert.ToDateTime(reader[8]);
                    book.PublishCount = Convert.ToInt32(reader[9]);
                    book.StockCount = Convert.ToInt32(reader[10]);
                    book.LocationId = reader[11].ToString();
                    book.BookName = reader[1].ToString();
                    book.Status = Convert.ToBoolean(reader[12]);
                    book.CreatedAt = Convert.ToDateTime(reader[13]);
                    book.Interpreter = reader[14].ToString();
                    book.Deleted = Convert.ToBoolean(reader[15]);
                    book.FixtureNo = reader[16].ToString();
                }
                conn.Close();
                return book;
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }
        }

        public Book getByPublisherId(string publisherId)
        {
            conn.Open();
            try
            {
                Book book = new Book();
                MySqlCommand commandToGetAllByCategory = new MySqlCommand("SELECT * FROM Book WHERE publisherId=@p1 AND deleted=0", conn);
                commandToGetAllByCategory.Parameters.AddWithValue("@p1", publisherId);
                MySqlDataReader reader = commandToGetAllByCategory.ExecuteReader();
                while (reader.Read())
                {
                    book.Id = reader[0].ToString();
                    book.LanguageId = reader[2].ToString();
                    book.AuthorId = reader[3].ToString();
                    book.CategoryId = reader[4].ToString();
                    book.PublisherId = reader[5].ToString();
                    book.PageCount = Convert.ToInt32(reader[6]);
                    book.IsbnNumber = reader[7].ToString();
                    book.PublishDate = Convert.ToDateTime(reader[8]);
                    book.PublishCount = Convert.ToInt32(reader[9]);
                    book.StockCount = Convert.ToInt32(reader[10]);
                    book.LocationId = reader[11].ToString();
                    book.BookName = reader[1].ToString();
                    book.Status = Convert.ToBoolean(reader[12]);
                    book.CreatedAt = Convert.ToDateTime(reader[13]);
                    book.Interpreter = reader[14].ToString();
                    book.Deleted = Convert.ToBoolean(reader[15]);
                    book.FixtureNo = reader[16].ToString();
                }
                conn.Close();
                return book;
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }
        }

       
    }
}
