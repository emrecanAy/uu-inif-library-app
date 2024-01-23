using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uu_library_app.Core.Migration.AccessDBMigration
{
    public class MigrationDA
    {
        public void ConvertCsvToEntity()
        {
            // path to the csv file
            string path = "C:/Users/Emrecan/Desktop/liste.csv";

            string[] lines = System.IO.File.ReadAllLines(path);
            foreach (string line in lines)
            {
                string[] columns = line.Split(',');
                foreach (string column in columns)
                {
                    Console.WriteLine(column);
                }
            }
        }
    }
}
