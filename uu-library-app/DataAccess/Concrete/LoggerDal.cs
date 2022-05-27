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
    public class LoggerDal : ILoggerDal
    {
        MySqlConnection conn = new MySqlConnection(DbConnection.connectionString);

        List<Logger> logs = new List<Logger>();
        public List<Logger> GetAll()
        {
            conn.Open();
            try
            {
                MySqlCommand commandToGetAll = new MySqlCommand("SELECT * FROM Log", conn);
                MySqlDataReader reader = commandToGetAll.ExecuteReader();
                while (reader.Read())
                {
                    Logger log = new Logger();
                    log.Id = reader[0].ToString();
                    log.AdminId = reader[1].ToString();
                    log.LogMessage = reader[2].ToString();
                    log.CreatedAt = Convert.ToDateTime(reader[3]);
                    logs.Add(log);
                }
                conn.Close();
                return logs;
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw new Exception("Something went wrong when getting all!");
            }
        }

        public void Log(Logger logger)
        {
            conn.Open();
            MySqlCommand commandToAdd = new MySqlCommand("INSERT INTO Log (id, adminId, logMessage) VALUES (@p1, @p2, @p3)", conn);
            try
            {
                commandToAdd.Parameters.AddWithValue("@p1", logger.Id);
                commandToAdd.Parameters.AddWithValue("@p2", logger.AdminId);
                commandToAdd.Parameters.AddWithValue("@p3", logger.LogMessage);
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
    }
}
