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
    public class FacultyDal : IFacultyDal
    {
        MySqlConnection conn = new MySqlConnection(DbConnection.connectionString);
        public void Add(Faculty faculty)
        {
            conn.Open();
            MySqlCommand commandToAdd = new MySqlCommand("INSERT INTO Faculty (id, name) VALUES (@p1, @p2)", conn);
            try
            {
                commandToAdd.Parameters.AddWithValue("@p1", faculty.Id);
                commandToAdd.Parameters.AddWithValue("@p2", faculty.Name);
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

        public void Delete(Faculty faculty)
        {
            conn.Open();
            try
            {
                MySqlCommand commandToUpdate = new MySqlCommand("UPDATE Faculty SET deleted=1 WHERE id=@p1 ", conn);
                commandToUpdate.Parameters.AddWithValue("@p1", faculty.Id);
                commandToUpdate.ExecuteNonQuery();

            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }

            conn.Close();
        }

       
        public List<Faculty> GetAll()
        {
            List<Faculty> faculties = new List<Faculty>();
            conn.Open();
            try
            {
                MySqlCommand commandToGetAll = new MySqlCommand("SELECT * FROM Faculty WHERE deleted=false", conn);
                MySqlDataReader reader = commandToGetAll.ExecuteReader();
                while (reader.Read())
                {
                    Faculty faculty = new Faculty();
                    faculty.Id = reader[0].ToString();
                    faculty.Name = reader[1].ToString();
                    faculty.CreatedAt = Convert.ToDateTime(reader[2]);
                    faculty.Deleted = Convert.ToBoolean(reader[3]);
                    faculties.Add(faculty);
                }
                conn.Close();
                return faculties;
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }
        }

        public void Update(Faculty faculty)
        {
            conn.Open();
            try
            {
                MySqlCommand commandToUpdate = new MySqlCommand("UPDATE Faculty SET name=@p2 WHERE id=@p1 ", conn);
                commandToUpdate.Parameters.AddWithValue("@p1", faculty.Id);
                commandToUpdate.Parameters.AddWithValue("@p2", faculty.Name);
                commandToUpdate.ExecuteNonQuery();

            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }

            conn.Close();
        }

        public Faculty FindById(string id)
        {
            Faculty faculty = new Faculty();
            conn.Open();
            try
            {
                MySqlCommand commandToGetAll = new MySqlCommand("SELECT * FROM Faculty WHERE id=@p1", conn);
                commandToGetAll.Parameters.AddWithValue("@p1", id);
                MySqlDataReader reader = commandToGetAll.ExecuteReader();
                while (reader.Read())
                {
                    faculty.Id = reader[0].ToString();
                    faculty.Name = reader[1].ToString();
                }
                conn.Close();
                return faculty;
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }
        }
    }
}
