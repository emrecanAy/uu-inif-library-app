using MySql.Data.MySqlClient;
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
    public class PublisherDal : IPublisherDal
    {
        MySqlConnection conn = new MySqlConnection(DbConnection.connectionString);

        public void Add(Publisher publisher)
        {
            conn.Open();
            MySqlCommand commandToAdd = new MySqlCommand("INSERT INTO Publisher (id, name) VALUES (@p1, @p2)", conn);
            try
            {
                commandToAdd.Parameters.AddWithValue("@p1", publisher.Id);
                commandToAdd.Parameters.AddWithValue("@p2", publisher.Name);
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

        public void Delete(string id)
        {
            conn.Open();
            try
            {
                MySqlCommand commandToUpdate = new MySqlCommand("UPDATE Publisher SET deleted=1 WHERE id=@p1 ", conn);
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

        List<Publisher> publishers=new List<Publisher>();
        public List<Publisher> getAll()
        {
            conn.Open();
            try
            {
                MySqlCommand commandToGetAll = new MySqlCommand("SELECT * FROM Department WHERE deleted=false", conn);
                MySqlDataReader reader = commandToGetAll.ExecuteReader();
                while (reader.Read())
                {
                    Publisher publisher = new Publisher();
                    publisher.Id = reader[0].ToString();
                    publisher.Name = reader[1].ToString();
                    publisher.CreatedAt = Convert.ToDateTime(reader[2]);
                    publisher.Deleted = Convert.ToBoolean(reader[3]);
                    publishers.Add(publisher);
                }
                conn.Close();
                return publishers;
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }
        }

        public void Update(Publisher publisher)
        {
            conn.Open();
            try
            {
                MySqlCommand commandToUpdate = new MySqlCommand("UPDATE Publisher SET name=@p2 WHERE id=@p1 ", conn);
                commandToUpdate.Parameters.AddWithValue("@p1", publisher.Id);
                commandToUpdate.Parameters.AddWithValue("@p2", publisher.Name);
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
