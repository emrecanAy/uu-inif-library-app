using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.Core.Helpers;
using uu_library_app.Core.Logger.FileLogger.Abstract;

namespace uu_library_app.Core.Logger.FileLogger
{
    public class FileLoggerDal : IFileLoggerDal
    {
        MySqlConnection conn = new MySqlConnection(DbConnection.connectionString);

        List<FileLogger> logs = new List<FileLogger>();
        public List<FileLogger> GetAll()
        {
            conn.Open();
            try
            {
                MySqlCommand commandToGetAll = new MySqlCommand("SELECT * FROM FileLog", conn);
                MySqlDataReader reader = commandToGetAll.ExecuteReader();
                while (reader.Read())
                {
                    FileLogger log = new FileLogger();
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

        public void Log(FileLogger fileLogger)
        {
            conn.Open();
            MySqlCommand commandToAdd = new MySqlCommand("INSERT INTO FileLog (id, adminId, logMessage) VALUES (@p1, @p2, @p3)", conn);
            try
            {
                commandToAdd.Parameters.AddWithValue("@p1", fileLogger.Id);
                commandToAdd.Parameters.AddWithValue("@p2", fileLogger.AdminId);
                commandToAdd.Parameters.AddWithValue("@p3", fileLogger.LogMessage);
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
