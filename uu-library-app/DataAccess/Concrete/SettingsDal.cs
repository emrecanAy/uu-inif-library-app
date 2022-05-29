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
    public class SettingsDal : ISettingsDal
    {
        MySqlConnection conn = new MySqlConnection(DbConnection.connectionString);
        
        public void AddOrUpdate(Settings settings)
        {
            /*UPDATED AT SORGUYA EKLENECEK*/
            conn.Open();
            MySqlCommand commandToAdd = new MySqlCommand("INSERT INTO Settings (id, senderEmail, senderPassword, remindingDay, depositDay, remindingMailHeader, remindingMailText, expiredMailHeader, expiredMailText) VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9) ON DUPLICATE KEY UPDATE senderEmail = @p2,senderPassword = @p3,remindingDay = @p4,depositDay = @p5,remindingMailHeader = @p6,remindingMailText = @p7,expiredMailHeader = @p8,expiredMailText = @p9", conn);
            try
            {
                commandToAdd.Parameters.AddWithValue("@p1", settings.Id);
                commandToAdd.Parameters.AddWithValue("@p2", settings.SenderEmail);
                commandToAdd.Parameters.AddWithValue("@p3", settings.SenderPassword);
                commandToAdd.Parameters.AddWithValue("@p4", settings.RemindingDay);
                commandToAdd.Parameters.AddWithValue("@p5", settings.DepositDay);
                commandToAdd.Parameters.AddWithValue("@p6", settings.RemindingMailHeader);
                commandToAdd.Parameters.AddWithValue("@p7", settings.RemindingMailText);
                commandToAdd.Parameters.AddWithValue("@p8", settings.ExpiredMailHeader);
                commandToAdd.Parameters.AddWithValue("@p9", settings.ExpiredMailText);
                commandToAdd.Parameters.AddWithValue("@p10", settings.UpdatedAt);
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

        public Settings getSettings()
        {
            
            try
            {
                conn.Open();
                Settings settings = new Settings();
                MySqlCommand commandToGetAll = new MySqlCommand("SELECT * FROM Settings", conn);
                MySqlDataReader reader = commandToGetAll.ExecuteReader();
                while (reader.Read())
                {
                    settings.Id = reader[0].ToString();
                    settings.SenderEmail = reader[1].ToString();
                    settings.SenderPassword = reader[2].ToString();
                    settings.RemindingDay = Convert.ToInt32(reader[3]);
                    settings.DepositDay = Convert.ToInt32(reader[4]);
                    settings.RemindingMailHeader = reader[5].ToString();
                    settings.RemindingMailText = reader[6].ToString();
                    settings.ExpiredMailHeader = reader[7].ToString();
                    settings.ExpiredMailText = reader[8].ToString();
                    settings.UpdatedAt = Convert.ToDateTime(reader[9].ToString());

                }
                conn.Close();
                return settings;
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }
        }

        public void Update(Settings settings)
        {
            conn.Open();
            try
            {
                MySqlCommand commandToUpdate = new MySqlCommand("UPDATE Settings SET senderEmail=@p2, senderPassword=@p3, remindingDay=@p4, depositDay=@p5, remindingMailHeader=@p6, remindingMailText=@p7, expiredMailHeader=@p8, expiredMailText=@p9, updatedAt=@p10 WHERE id=@p1 ", conn);
                commandToUpdate.Parameters.AddWithValue("@p1", settings.Id);
                commandToUpdate.Parameters.AddWithValue("@p2", settings.SenderEmail);
                commandToUpdate.Parameters.AddWithValue("@p3", settings.SenderPassword);
                commandToUpdate.Parameters.AddWithValue("@p4", settings.RemindingDay);
                commandToUpdate.Parameters.AddWithValue("@p5", settings.DepositDay);
                commandToUpdate.Parameters.AddWithValue("@p6", settings.RemindingMailHeader);
                commandToUpdate.Parameters.AddWithValue("@p7", settings.RemindingMailText);
                commandToUpdate.Parameters.AddWithValue("@p8", settings.ExpiredMailHeader);
                commandToUpdate.Parameters.AddWithValue("@p9", settings.ExpiredMailText);
                commandToUpdate.Parameters.AddWithValue("@p10", settings.UpdatedAt);
                commandToUpdate.ExecuteNonQuery();
                Console.WriteLine("Güncellendi!");

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
