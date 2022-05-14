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
    public class StudentDal : IStudentDal
    {
        MySqlConnection conn = new MySqlConnection("Server=172.21.54.3;uid=ASSEMSoft;pwd=Assemsoft1320..!;database=ASSEMSoft");

        public void Add(Student student)
        {
            conn.Open();
            MySqlCommand commandToAdd = new MySqlCommand("INSERT INTO Student (id, firstName, lastName, number, card, eMail, departmentId) VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7)", conn);
            try
            {
                commandToAdd.Parameters.AddWithValue("@p1", student.Id);
                commandToAdd.Parameters.AddWithValue("@p2", student.FirstName);
                commandToAdd.Parameters.AddWithValue("@p3", student.LastName);
                commandToAdd.Parameters.AddWithValue("@p4", student.Number);
                commandToAdd.Parameters.AddWithValue("@p5", student.Card);
                commandToAdd.Parameters.AddWithValue("@p6", student.Email);
                commandToAdd.Parameters.AddWithValue("@p7", student.DepartmentId);
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

        public void Delete(Student student)
        {
            conn.Open();
            MySqlCommand commandToDelete = new MySqlCommand("SELECT * FROM Student WHERE id=@p1", conn);
            commandToDelete.Parameters.AddWithValue("@p1", student.Id);
            commandToDelete.ExecuteReader();
        }

        public List<Student> getAll()
        {
            throw new NotImplementedException();
        }

        public Student getById(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(Student student)
        {
            throw new NotImplementedException();
        }
    }
}
