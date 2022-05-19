using MySql.Data.MySqlClient;
using MySql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.DataAccess.Abstract;
using uu_library_app.Entity.Concrete;

namespace uu_library_app.DataAccess.Concrete
{
    public class LanguageDal : ILanguageDal
    {

        MySqlConnection conn = new MySqlConnection("Server=172.21.54.3;uid=ASSEMSoft;pwd=Assemsoft1320..!;database=ASSEMSoft");
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

        public void Delete(string id)
        {
            conn.Open();
            try
            {
                MySqlCommand commandToUpdate = new MySqlCommand("UPDATE Language SET deleted=1 WHERE id=@p1 ", conn);
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

        List<Language> languages;
        public List<Language> getAll()
        {
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

        public void Update(Language student)
        {
            throw new NotImplementedException();
        }
    }
}
