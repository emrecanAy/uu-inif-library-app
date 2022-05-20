using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.Entity.Abstract;

namespace uu_library_app.Entity.Concrete
{
    public class Location : IBaseModelWithIdAndDate
    {
        /*
          
          IBaseModelWithIdAndDate'i implemente ettiği için 
         
            public string id;
            public DateTime createdAt;
            public bool deleted;
            
         yukarıdaki field'ları tekrar eklemeye gerek yok. Çünkü onlar IBaseModelWithIdAndDate'de var.
         Bu şekilde her bir varlık class'ından 3'er satır kod azaltmış oluyoruz.
         Bu field'lar tüm class'larda ortak olduğu için tekrar tekrar hepsine yazmak yerine ortak bir yerden çekelim dedim.

         Siz sadece bu class'a özgü olan fieldları ekleyin yeterli. Yukarıdaki üçü her türlü olacak zaten.
         Not: bunların da getter ve setter'larını yazmayı unutmayın.
         Örnek için bkz. Department.cs
            
         */
    }
}
