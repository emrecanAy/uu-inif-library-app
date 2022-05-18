using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.DataAccess.Abstract;
using uu_library_app.Entity.Concrete;

namespace uu_library_app.DataAccess.Concrete
{
    public class AuthorDal : IAuthorDal
    {
        MySqlConnection conn = new MySqlConnection("Server=172.21.54.3;uid=ASSEMSoft;pwd=Assemsoft1320..!;database=ASSEMSoft");
        public void Add(Author author)
        {
            conn.Open();
            MySqlCommand commandToAdd = new MySqlCommand("INSERT INTO Author (id, firstName, lastName) VALUES (@p1, @p2, @p3)", conn);
            try
            {
                commandToAdd.Parameters.AddWithValue("@p1", author.Id);
                commandToAdd.Parameters.AddWithValue("@p2", author.FirstName);
                commandToAdd.Parameters.AddWithValue("@p3", author.LastName);
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

        public void Delete(Author author)
        {
            throw new NotImplementedException();
        }

        public void Update(Author author)
        {
            throw new NotImplementedException();
        }
    }
}
