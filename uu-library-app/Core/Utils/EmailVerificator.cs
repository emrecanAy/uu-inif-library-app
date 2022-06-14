using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uu_library_app.Core.Utils
{
    public static class EmailVerificator
    {

        public static string _numbers = "0123456789";
        static Random random = new Random();
        public static string GenerateCode()
        {
            StringBuilder builder = new StringBuilder(6);
            string numberAsString = "";
            int numberAsNumber = 0;

            for (var i = 0; i < 6; i++)
            {
                builder.Append(_numbers[random.Next(0, _numbers.Length)]);
            }

            numberAsString = builder.ToString();
            numberAsNumber = int.Parse(numberAsString);
            return numberAsString;


        }

        public static bool isValidSchoolMail(string email)
        {
            string ending = email.Substring(email.IndexOf("@") + 1);
            if (ending != "ogr.uludag.edu.tr")
            {
                return false;
            }
            return true;
        }
        public static bool isValidPersonnelMail(string email)
        {
            string ending = email.Substring(email.IndexOf("@") + 1);
            if (ending != "uludag.edu.tr")
            {
                return false;
            }
            return true;
        }
    }
}
