using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uu_library_app.Core.Helpers
{
    public static class SqlCommandHelper
    {
        public static MySqlCommand getCategoriesCommand(MySqlConnection conn)
        {
            MySqlCommand commandToGetAllCategories = new MySqlCommand("SELECT * FROM Category WHERE deleted=false", conn);
            return commandToGetAllCategories;
        }

        public static MySqlCommand getLocationsCommand(MySqlConnection conn)
        {
            MySqlCommand commandToGetAllLocations= new MySqlCommand("SELECT * FROM Location WHERE deleted=false", conn);
            return commandToGetAllLocations;
        }

        public static MySqlCommand getAuthorsCommand(MySqlConnection conn)
        {
            MySqlCommand commandToGetAllAuthors = new MySqlCommand("SELECT * FROM Author WHERE deleted=false", conn);
            return commandToGetAllAuthors;
        }

        public static MySqlCommand getAuthorsCommandConcatFirstNameAndLastName(MySqlConnection conn)
        {
            MySqlCommand commandToGetAllAuthors = new MySqlCommand("SELECT id, CONCAT(firstName,' ',lastName) as fullName FROM Author WHERE deleted=false", conn);
            return commandToGetAllAuthors;
        }

        public static MySqlCommand getLanguagesCommand(MySqlConnection conn)
        {
            MySqlCommand commandToGetAllLanguages = new MySqlCommand("SELECT * FROM Language WHERE deleted=false", conn);
            return commandToGetAllLanguages;
        }

        public static MySqlCommand getPublishersCommand(MySqlConnection conn)
        {
            MySqlCommand commandToGetAllPublishers = new MySqlCommand("SELECT * FROM Publisher WHERE deleted=false", conn);
            return commandToGetAllPublishers;
        }

        public static MySqlCommand getStudentsCommand(MySqlConnection conn)
        {
            MySqlCommand commandToGetAllStudents = new MySqlCommand("SELECT * FROM Student WHERE deleted=false", conn);
            return commandToGetAllStudents;
        }

        

    }
}
