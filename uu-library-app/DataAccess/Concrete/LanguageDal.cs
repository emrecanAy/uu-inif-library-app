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
    public class LanguageDal : ILanguageDal
    {

        MySqlConnection conn = new MySqlConnection(DbConnection.connectionString);
        public void Add(Language language)
        {
            conn.Open();
            MySqlCommand commandToAdd = new MySqlCommand("INSERT INTO Language (id, language) VALUES (@p1, @p2)", conn);
            try
            {
                commandToAdd.Parameters.AddWithValue("@p1", language.Id);
                commandToAdd.Parameters.AddWithValue("@p2", language.LanguageName);
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

        public void Delete(Language language)
        {
            conn.Open();
            try
            {
                MySqlCommand commandToUpdate = new MySqlCommand("UPDATE Language SET deleted=1 WHERE id=@p1 ", conn);
                commandToUpdate.Parameters.AddWithValue("@p1", language.Id);
                commandToUpdate.ExecuteNonQuery();

            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }

            conn.Close();
        }

        
        public List<Language> getAll()
        {
            List<Language> languages = new List<Language>();
            conn.Open();
            try
            {
                MySqlCommand commandToGetAll = new MySqlCommand("SELECT * FROM Author WHERE deleted=false", conn);
                MySqlDataReader reader = commandToGetAll.ExecuteReader();
                while (reader.Read())
                {
                    Language language = new Language();
                    language.Id = reader[0].ToString();
                    language.LanguageName = reader[1].ToString();
                    languages.Add(language);
                }
                conn.Close();
                return languages;
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }
        }

        public void Update(Language language)
        {
            conn.Open();
            try
            {
                MySqlCommand commandToUpdate = new MySqlCommand("UPDATE Language SET language=@p2 WHERE id=@p1 ", conn);
                commandToUpdate.Parameters.AddWithValue("@p1", language.Id);
                commandToUpdate.Parameters.AddWithValue("@p2", language.LanguageName);
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
