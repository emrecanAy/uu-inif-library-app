using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql;
using MySql.Data.MySqlClient;
using uu_library_app.DataAccess.Abstract;
using uu_library_app.Entity.Concrete;


namespace uu_library_app.DataAccess.Concrete.EntityFramework
{
    public class DepartmentDal : IDepartmentDal
    {

        MySqlConnection conn = new MySqlConnection("Server=172.21.54.3;uid=ASSEMSoft;pwd=Assemsoft1320..!;database=ASSEMSoft");

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

        public void Delete(Department book)
        {
            throw new NotImplementedException();
        }

        public List<Department> getAll()
        {
            throw new NotImplementedException();
        }

        public Book getById(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(Department book)
        {
            throw new NotImplementedException();
        }
    }
}
