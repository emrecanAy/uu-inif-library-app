using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.DataAccess.Abstract;
using uu_library_app.Entity.Concrete;
using MySql.Data.MySqlClient;

namespace uu_library_app.DataAccess.Concrete
{
    internal class LocationDal : ILocationDal
    {
        MySqlConnection conn = new MySqlConnection("Server=172.21.54.3;uid=ASSEMSoft;pwd=Assemsoft1320..!;database=ASSEMSoft");
        public void Add(Location location)
        {
            conn.Open();
            MySqlCommand commandToAdd = new MySqlCommand("INSERT INTO Location (id,shelf) VALUES (@p1, @p2)", conn);
            try
            {
                commandToAdd.Parameters.AddWithValue("@p1", location.Id);
                commandToAdd.Parameters.AddWithValue("@p2", location.Shelf);
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

        List<Location> locations;
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
                MySqlCommand commandToUpdate = new MySqlCommand("UPDATE Location SET name=@p2 WHERE id=@p1 ", conn);
                commandToUpdate.Parameters.AddWithValue("@p1", location.Id);
                commandToUpdate.Parameters.AddWithValue("@p2", location.Shelf);
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
