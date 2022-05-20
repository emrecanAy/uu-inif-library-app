using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.Entity.Abstract;

namespace uu_library_app.Entity.Concrete
{
    public class Admin : IBaseModelWithIdAndDate
    {
        private string firstName;
        private string lastName;
        private string eMail;
        private string password;

        public Admin()
        {
        }

        public Admin(string id,string firstName, string lastName, string eMail, string password)
        {
            this.id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.EMail = eMail;
            this.Password = password;
        }

        public string Id { get => id; set => id = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string EMail { get => eMail; set => eMail = value; }
        public string Password { get => password; set => password = value; }
        public DateTime CreatedAt { get => createdAt; set => createdAt = value; }
        public bool Deleted { get => deleted; set => deleted = value; }






        /*
          
          IBaseModelWithIdAndDate'i implemente ettiği için 
         
            public string id;
            public DateTime createdAt;
            public bool deleted;
            
         yukarıdaki field'ları tekrar eklemeye gerek yok. Çünkü onlar IBaseModelWithIdAndDate'de var.
         Bu şekilde her bir varlık class'ından 3'er satır kod azaltmış oluyoruz.
         Bu field'lar tüm class'larda ortak olduğu için tekrar tekrar hepsine yazmak yerine ortak bir yerden çekelim dedim.

         Siz sadece bu class'a özgü olan fieldları ekleyin yeterli. Yukarıdaki üçü her türlü olacak zaten.
         Not: Bu üç field'ın da getter ve setter'larını yazmayı unutmayın.
         Örnek için bkz. Department.cs
            
         */
    }
}
