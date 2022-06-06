using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uu_library_app.Core.Helpers
{
    public static class DataListerToDataAdapter
    {
        public static MySqlDataAdapter listBooksForPagination(MySqlConnection conn)
        {
            MySqlCommand command = new MySqlCommand("SELECT Book.id, Book.bookName, CONCAT( Author.firstName, ' ', Author.lastName ) AS authorFullName, Publisher.name, Book.isbnNumber FROM Book INNER JOIN Author ON Book.authorId = Author.id INNER JOIN Publisher ON Book.publisherId = Publisher.id WHERE Book.deleted=0", conn);
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            return da;
        }
    }
}
