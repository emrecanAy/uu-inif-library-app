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

namespace uu_library_app.DataAccess.Concrete
{
    public class CategoryDal : ICategoryDal
    {
        MySqlConnection conn = new MySqlConnection(DbConnection.connectionString);
        public void Add(Category category)
        {
            conn.Open();
            MySqlCommand commandToAdd = new MySqlCommand("INSERT INTO Category (id,name) VALUES (@p1, @p2)", conn);
            try
            {
                commandToAdd.Parameters.AddWithValue("@p1",category.Id  );
                commandToAdd.Parameters.AddWithValue("@p2",category.Name );
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
                MySqlCommand commandToUpdate = new MySqlCommand("UPDATE Category SET deleted=1 WHERE id=@p1 ", conn);
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
        List<Category> categories=new List<Category>();
        public List<Category> getAll()
        {
            conn.Open();
            try
            {
                MySqlCommand commandToGetAll = new MySqlCommand("SELECT * FROM Category WHERE deleted=false", conn);
                MySqlDataReader reader = commandToGetAll.ExecuteReader();
                while (reader.Read())
                {
                    Category category = new Category();
                    category.Id = reader[0].ToString();
                    category.Name = reader[1].ToString();
                    category.CreatedAt = Convert.ToDateTime(reader[2]);
                    category.Deleted = Convert.ToBoolean(reader[3]);
                    categories.Add(category);
                }
                conn.Close();
                return categories;
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }
        }

        public void Update(Category category)
        {
            conn.Open();
            try
            {
                MySqlCommand commandToUpdate = new MySqlCommand("UPDATE Category SET name=@p2 WHERE id=@p1 ", conn);
                commandToUpdate.Parameters.AddWithValue("@p1", category.Id);
                commandToUpdate.Parameters.AddWithValue("@p2", category.Name);
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
