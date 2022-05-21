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
    public class StudentDal : IStudentDal
    {
        MySqlConnection conn = new MySqlConnection(DbConnection.connectionString);

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

        public void Update(Student student)
        {
            conn.Open();
            try
            {
                MySqlCommand commandToUpdate = new MySqlCommand("UPDATE Student SET firstName=@p2, lastName=@p3, number=@p4, card=@p5, eMail=@p6, departmentId=@p7 WHERE id=@p1 ", conn);
                commandToUpdate.Parameters.AddWithValue("@p1", student.Id);
                commandToUpdate.Parameters.AddWithValue("@p2", student.FirstName);
                commandToUpdate.Parameters.AddWithValue("@p3", student.LastName);
                commandToUpdate.Parameters.AddWithValue("@p4", student.Number);
                commandToUpdate.Parameters.AddWithValue("@p5", student.Card);
                commandToUpdate.Parameters.AddWithValue("@p6", student.Email);
                commandToUpdate.Parameters.AddWithValue("@p7", student.DepartmentId);
                commandToUpdate.ExecuteNonQuery();

              

            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }

            conn.Close();

        }

        public void Delete(string id)
        {
            conn.Open();
            try
            {
                MySqlCommand commandToUpdate = new MySqlCommand("UPDATE Student SET deleted=1 WHERE id=@p1 ", conn);
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

        public Student findByName(string name)
        {
            Student student = new Student();
            conn.Open();
            try
            {
                MySqlCommand commandToGetAll = new MySqlCommand("SELECT * FROM Student WHERE firstName=@p1", conn);
                commandToGetAll.Parameters.AddWithValue("@p1", name);
                MySqlDataReader reader = commandToGetAll.ExecuteReader();
                while (reader.Read())
                {
                    student.Id = reader[0].ToString();
                    student.FirstName = reader[1].ToString();
                    student.LastName = reader[2].ToString();
                    student.Number = reader[3].ToString();
                    student.Card = reader[4].ToString();
                    student.Email = reader[5].ToString();
                    student.DepartmentId = reader[6].ToString();
                }
                conn.Close();
                return student;
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }
        }

        public Student findByNumber(string number)
        {
            Student student = new Student();
            conn.Open();
            try
            {
                MySqlCommand commandToGetAll = new MySqlCommand("SELECT * FROM Student WHERE number=@p1", conn);
                commandToGetAll.Parameters.AddWithValue("@p1", number);
                MySqlDataReader reader = commandToGetAll.ExecuteReader();
                while (reader.Read())
                {
                    student.Id = reader[0].ToString();
                    student.FirstName = reader[1].ToString();
                    student.LastName = reader[2].ToString();
                    student.Number = reader[3].ToString();
                    student.Card = reader[4].ToString();
                    student.Email = reader[5].ToString();
                    student.DepartmentId = reader[6].ToString();
                }
                conn.Close();
                return student;
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }
        }

        List<Student> students=new List<Student>();
        public List<Student> getAll()
        {
            conn.Open();
            try
            {
                MySqlCommand commandToGetAll = new MySqlCommand("SELECT * FROM Student WHERE deleted=false", conn);
                MySqlDataReader reader = commandToGetAll.ExecuteReader();
                while (reader.Read())
                {
                    Student student = new Student();
                    student.Id = reader[0].ToString();
                    student.FirstName = reader[1].ToString();
                    student.LastName = reader[2].ToString();
                    student.Number = reader[3].ToString();
                    student.Card = reader[4].ToString();
                    student.Email = reader[5].ToString();
                    student.DepartmentId = reader[6].ToString();
                    students.Add(student);
                }
                conn.Close();
                return students;
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }
        }

        public List<Student> getAllSortedByAddedDate()
        {
            conn.Open();
            try
            {
                MySqlCommand commandToGetAll = new MySqlCommand("SELECT * FROM Student WHERE deleted=false ORDER BY createdAt ASC", conn);
                MySqlDataReader reader = commandToGetAll.ExecuteReader();
                while (reader.Read())
                {
                    Student student = new Student();
                    student.Id = reader[0].ToString();
                    student.FirstName = reader[1].ToString();
                    student.LastName = reader[2].ToString();
                    student.Number = reader[3].ToString();
                    student.Card = reader[4].ToString();
                    student.Email = reader[5].ToString();
                    student.DepartmentId = reader[6].ToString();
                    students.Add(student);
                }
                conn.Close();
                return students;
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }
        }

        public List<Student> getAllSortedByName()
        {
            conn.Open();
            try
            {
                MySqlCommand commandToGetAll = new MySqlCommand("SELECT * FROM Student WHERE deleted=false ORDER BY firstName ASC", conn);
                MySqlDataReader reader = commandToGetAll.ExecuteReader();
                while (reader.Read())
                {
                    Student student = new Student();
                    student.Id = reader[0].ToString();
                    student.FirstName = reader[1].ToString();
                    student.LastName = reader[2].ToString();
                    student.Number = reader[3].ToString();
                    student.Card = reader[4].ToString();
                    student.Email = reader[5].ToString();
                    student.DepartmentId = reader[6].ToString();
                    students.Add(student);
                }
                conn.Close();
                return students;
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }
        }

        public Student getById(string id)
        {
            Student student = new Student();
            conn.Open();
            try
            {
                MySqlCommand commandToGetAll = new MySqlCommand("SELECT * FROM Student WHERE id=@p1", conn);
                commandToGetAll.Parameters.AddWithValue("@p1", id);
                MySqlDataReader reader = commandToGetAll.ExecuteReader();
                while (reader.Read())
                {
                    student.Id = reader[0].ToString();
                    student.FirstName = reader[1].ToString();
                    student.LastName = reader[2].ToString();
                    student.Number = reader[3].ToString();
                    student.Card = reader[4].ToString();
                    student.Email = reader[5].ToString();
                    student.DepartmentId = reader[6].ToString();
                }
                conn.Close();
                return student;
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }
        }

       
    }
}
