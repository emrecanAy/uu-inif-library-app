using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using uu_library_app.Business.Concrete;
using uu_library_app.DataAccess.Concrete;
using uu_library_app.Entity.Concrete;

namespace uu_library_app.Core.Helpers
{
    public class DashboardDataHelper
    {
        MySqlConnection conn = new MySqlConnection(DbConnection.connectionString);
        List<Student> topStudents = new List<Student>();
        public List<Student> GetTopFiveReaders()
        {
            conn.Open();
            try
            {
                MySqlCommand commandToGetAll = new MySqlCommand("SELECT studentId,count(*), Student.firstName, Student.lastName FROM DepositBook INNER JOIN Student ON DepositBook.studentId = Student.id GROUP BY studentId ORDER BY count(*) DESC LIMIT 5", conn);
                MySqlDataReader reader = commandToGetAll.ExecuteReader();
                while (reader.Read())
                {
                    Student student = new Student();
                    student.Id = reader[0].ToString();
                    student.FirstName = reader[2].ToString() +" "+ reader[3].ToString();
                    student.Number = reader[1].ToString();
                    topStudents.Add(student);
                }
                conn.Close();
                return topStudents;
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }
        }

        List<Department> topDepartments = new List<Department>();
        public List<Department> GetTopFiveReaderDepartments()
        {
            conn.Open();
            try
            {
                MySqlCommand commandToGetAll = new MySqlCommand("SELECT Student.departmentId, count(*), Department.name FROM DepositBook INNER JOIN Student ON DepositBook.studentId = Student.id INNER JOIN Department ON Student.departmentId= Department.id GROUP BY Department.id ORDER BY count(*) DESC LIMIT 5", conn);
                MySqlDataReader reader = commandToGetAll.ExecuteReader();
                while (reader.Read())
                {
                    Department department = new Department();
                    department.Id = reader[1].ToString();
                    department.Name = reader[2].ToString();
                    topDepartments.Add(department);
                }
                conn.Close();
                return topDepartments;
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }
        }

        public static void listInnerJoinAllDepositBooksBetweenDates(MySqlConnection conn, DateTime startDate)
        {
            DataTable dt = new DataTable();

            MySqlCommand command = new MySqlCommand("SELECT DepositBook.id, Student.firstName, Student.lastName, Book.id, Book.bookName, Author.firstName'authorFirstName', Author.lastName'authorLastName' FROM DepositBook INNER JOIN Student ON DepositBook.studentId = Student.id INNER JOIN Book ON DepositBook.bookId = Book.id INNER JOIN Author ON Book.authorId = Author.id WHERE DATE(DepositBook.depositDate) BETWEEN @p1 AND @p2 ORDER BY depositDate ASC", conn);
            command.Parameters.AddWithValue("@p1", startDate);
            command.Parameters.AddWithValue("@p2", DateTime.Now);
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            da.Fill(dt);

        }

        public static void LastSevenDaysDepositBookData(Chart chart,MySqlConnection conn)
        {
            DateTime startDate = DateTime.Now.AddDays(-7);

        }


    }
}
