using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.DataAccess.Abstract;
using uu_library_app.Entity.Concrete;
using MySql.Data.MySqlClient;
using uu_library_app.Core.Helpers;

namespace uu_library_app.DataAccess.Concrete
{
    internal class LocationDal : ILocationDal
    {
        MySqlConnection conn = new MySqlConnection(DbConnection.connectionString);
        public void Add(Location location)
        {
            conn.Open();
            MySqlCommand commandToAdd = new MySqlCommand("INSERT INTO Location (id, shelf, categoryId) VALUES (@p1, @p2, @p3)", conn);
            try
            {
                commandToAdd.Parameters.AddWithValue("@p1", location.Id);
                commandToAdd.Parameters.AddWithValue("@p2", location.Shelf);
                commandToAdd.Parameters.AddWithValue("@p3", location.CategoryId);
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
                MySqlCommand commandToUpdate = new MySqlCommand("UPDATE Location SET deleted=1 WHERE id=@p1 ", conn);
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

        List<Location> locations=new List<Location>();
        public List<Location> getAll()
        {
            conn.Open();
            try
            {
                MySqlCommand commandToGetAll = new MySqlCommand("SELECT * FROM Location WHERE deleted=false", conn);
                MySqlDataReader reader = commandToGetAll.ExecuteReader();
                while (reader.Read())
                {
                    Location location = new Location();
                    location.Id = reader[0].ToString();
                    location.Shelf = reader[1].ToString();
                    location.CreatedAt = Convert.ToDateTime(reader[2]);
                    location.Deleted = Convert.ToBoolean(reader[3]);
                    locations.Add(location);
                }
                conn.Close();
                return locations;
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }
        }
            public void Update(Location location)
            {
            conn.Open();
            try
            {
                MySqlCommand commandToUpdate = new MySqlCommand("UPDATE Location SET shelf=@p2, categoryId=@p3 WHERE id=@p1 ", conn);
                commandToUpdate.Parameters.AddWithValue("@p1", location.Id);
                commandToUpdate.Parameters.AddWithValue("@p2", location.Shelf);
                commandToUpdate.Parameters.AddWithValue("@p3", location.CategoryId);
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
