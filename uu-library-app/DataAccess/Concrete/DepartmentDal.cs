using MySql.Data.MySqlClient;
using MySql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.DataAccess.Abstract;
using uu_library_app.Entity.Concrete;
using uu_library_app.Core.Helpers;

namespace uu_library_app.DataAccess.Concrete.EntityFramework
{
    public class DepartmentDal : IDepartmentDal
    {

        MySqlConnection conn = new MySqlConnection(DbConnection.connectionString);

        public void Add(Department department)
        {
            conn.Open();
            MySqlCommand commandToAdd = new MySqlCommand("INSERT INTO Department (id, name) VALUES (@p1, @p2)", conn);
            try
            {
                commandToAdd.Parameters.AddWithValue("@p1", department.Id);
                commandToAdd.Parameters.AddWithValue("@p2", department.Name);
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

        public void Delete(Department department)
        {
            conn.Open();
            try
            {
                MySqlCommand commandToUpdate = new MySqlCommand("UPDATE Department SET deleted=1 WHERE id=@p1 ", conn);
                commandToUpdate.Parameters.AddWithValue("@p1", department.Id);
                commandToUpdate.ExecuteNonQuery();

            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }

            conn.Close();
        }

        List<Department> departments=new List<Department>();
        public List<Department> getAll()
        {
            conn.Open();
            try
            {
                MySqlCommand commandToGetAll = new MySqlCommand("SELECT * FROM Department WHERE deleted=false", conn);
                MySqlDataReader reader = commandToGetAll.ExecuteReader();
                while (reader.Read())
                {
                    Department department = new Department();
                    department.Id = reader[0].ToString();
                    department.Name = reader[1].ToString();
                    department.CreatedAt = Convert.ToDateTime(reader[2]);
                    department.Deleted = Convert.ToBoolean(reader[3]);
                    departments.Add(department);
                }
                conn.Close();
                return departments;
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }    
        }

        public void Update(Department department)
        {
            conn.Open();
            try
            {
                MySqlCommand commandToUpdate = new MySqlCommand("UPDATE Department SET name=@p2 WHERE id=@p1 ", conn);
                commandToUpdate.Parameters.AddWithValue("@p1", department.Id);
                commandToUpdate.Parameters.AddWithValue("@p2", department.Name);
                commandToUpdate.ExecuteNonQuery();

            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }

            conn.Close();
        }

        public Department FindById(string id)
        {
            Department department = new Department();
            conn.Open();
            try
            {
                MySqlCommand commandToGetAll = new MySqlCommand("SELECT * FROM Department WHERE id=@p1", conn);
                commandToGetAll.Parameters.AddWithValue("@p1", id);
                MySqlDataReader reader = commandToGetAll.ExecuteReader();
                while (reader.Read())
                {
                    department.Id = reader[0].ToString();
                    department.Name = reader[1].ToString();
                }
                conn.Close();
                return department;
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }
        }
    }
}
