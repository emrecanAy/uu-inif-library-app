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
    public class PersonnelDal : IPersonnelDal
    {
        MySqlConnection conn = new MySqlConnection(DbConnection.connectionString);

        public void Add(Personnel personnel)
        {
            conn.Open();
            MySqlCommand commandToAdd = new MySqlCommand("INSERT INTO Personnel (id, firstName, lastName, eMail, registrationNumber, facultyId) VALUES (@p1, @p2, @p3, @p4, @p5, @p6)", conn);
            try
            {
                commandToAdd.Parameters.AddWithValue("@p1", personnel.Id);
                commandToAdd.Parameters.AddWithValue("@p2", personnel.FirstName);
                commandToAdd.Parameters.AddWithValue("@p3", personnel.LastName);
                commandToAdd.Parameters.AddWithValue("@p4", personnel.Email);
                commandToAdd.Parameters.AddWithValue("@p5", personnel.RegistrationNumber);
                commandToAdd.Parameters.AddWithValue("@p6", personnel.FacultyId);
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

        public void Update(Personnel personnel)
        {
            conn.Open();
            try
            {
                MySqlCommand commandToUpdate = new MySqlCommand("UPDATE Personnel SET firstName=@p2, lastName=@p3, eMail=@p4, registrationNumber=@p5, facultyId=@p6 WHERE id=@p1 ", conn);
                commandToUpdate.Parameters.AddWithValue("@p1", personnel.Id);
                commandToUpdate.Parameters.AddWithValue("@p2", personnel.FirstName);
                commandToUpdate.Parameters.AddWithValue("@p3", personnel.LastName);
                commandToUpdate.Parameters.AddWithValue("@p4", personnel.Email);
                commandToUpdate.Parameters.AddWithValue("@p5", personnel.RegistrationNumber);
                commandToUpdate.Parameters.AddWithValue("@p6", personnel.FacultyId);
                commandToUpdate.ExecuteNonQuery();

            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }

            conn.Close();
        }

        public void Delete(Personnel personnel)
        {
            conn.Open();
            try
            {
                MySqlCommand commandToUpdate = new MySqlCommand("UPDATE Personnel SET deleted=1 WHERE id=@p1 ", conn);
                commandToUpdate.Parameters.AddWithValue("@p1", personnel.Id);
                commandToUpdate.ExecuteNonQuery();

            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }

            conn.Close();
        }

        public Personnel findByEmail(string email)
        {
            Personnel personnel = new Personnel();
            conn.Open();
            try
            {
                MySqlCommand commandToGetAll = new MySqlCommand("SELECT * FROM Personnel WHERE eMail=@p1", conn);
                commandToGetAll.Parameters.AddWithValue("@p1", email);
                MySqlDataReader reader = commandToGetAll.ExecuteReader();
                while (reader.Read())
                {
                    personnel.Id = reader[0].ToString();
                    personnel.FirstName = reader[1].ToString();
                    personnel.LastName = reader[2].ToString();
                    personnel.Email = reader[3].ToString();
                    personnel.RegistrationNumber = reader[4].ToString();
                    personnel.FacultyId = reader[5].ToString();
                    personnel.CreatedAt = Convert.ToDateTime(reader[6]);
                    personnel.Deleted = Convert.ToBoolean(reader[7]);
                }
                conn.Close();
                return personnel;
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }
        }

        public Personnel findByName(string name)
        {
            Personnel personnel = new Personnel();
            conn.Open();
            try
            {
                MySqlCommand commandToGetAll = new MySqlCommand("SELECT * FROM Personnel WHERE firstName=@p1", conn);
                commandToGetAll.Parameters.AddWithValue("@p1", name);
                MySqlDataReader reader = commandToGetAll.ExecuteReader();
                while (reader.Read())
                {
                    personnel.Id = reader[0].ToString();
                    personnel.FirstName = reader[1].ToString();
                    personnel.LastName = reader[2].ToString();
                    personnel.Email = reader[3].ToString();
                    personnel.RegistrationNumber = reader[4].ToString();
                    personnel.FacultyId = reader[5].ToString();
                    personnel.CreatedAt = Convert.ToDateTime(reader[6]);
                    personnel.Deleted = Convert.ToBoolean(reader[7]);
                }
                conn.Close();
                return personnel;
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }
        }

        
        public List<Personnel> getAll()
        {
            List<Personnel> personnels = new List<Personnel>();

            conn.Open();
            try
            {
                MySqlCommand commandToGetAll = new MySqlCommand("SELECT * FROM Personnel WHERE deleted=0", conn);
                MySqlDataReader reader = commandToGetAll.ExecuteReader();
                while (reader.Read())
                {
                    Personnel personnel = new Personnel();
                    personnel.Id = reader[0].ToString();
                    personnel.FirstName = reader[1].ToString();
                    personnel.LastName = reader[2].ToString();
                    personnel.Email = reader[3].ToString();
                    personnel.RegistrationNumber = reader[4].ToString();
                    personnel.FacultyId = reader[5].ToString();
                    personnel.CreatedAt = Convert.ToDateTime(reader[6]);
                    personnel.Deleted = Convert.ToBoolean(reader[7]);
                    personnels.Add(personnel);
                }
                conn.Close();
                return personnels;
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }
        }

        public Personnel getById(string id)
        {
            Personnel personnel = new Personnel();
            conn.Open();
            try
            {
                MySqlCommand commandToGetAll = new MySqlCommand("SELECT * FROM Personnel WHERE id=@p1", conn);
                commandToGetAll.Parameters.AddWithValue("@p1", id);
                MySqlDataReader reader = commandToGetAll.ExecuteReader();
                while (reader.Read())
                {
                    personnel.Id = reader[0].ToString();
                    personnel.FirstName = reader[1].ToString();
                    personnel.LastName = reader[2].ToString();
                    personnel.Email = reader[3].ToString();
                    personnel.RegistrationNumber = reader[4].ToString();
                    personnel.FacultyId = reader[5].ToString();
                    personnel.CreatedAt = Convert.ToDateTime(reader[6]);
                    personnel.Deleted = Convert.ToBoolean(reader[7]);
                }
                conn.Close();
                return personnel;
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }
        }

        
    }
}
