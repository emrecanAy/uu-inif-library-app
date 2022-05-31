using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uu_library_app.Entity.Concrete.DTO
{
    public class DepositBookDto
    {
        private string id;
        private string studentNo;
        private string studentFullName;
        private string book;
        private string author;
        private DateTime createdAt;

        public DepositBookDto() { }

        public DepositBookDto(string id, string studentNo, string studentFullName, string book, string author, DateTime createdAt)
        {
            this.Id = id;
            this.OgrenciNo = studentNo;
            this.Ogrenci = studentFullName;
            this.Kitap = book;
            this.Yazar = author;
            this.OlusturulmaTarihi = createdAt;
        }

        public string Id { get => id; set => id = value; }
        public string Ogrenci { get => studentFullName; set => studentFullName = value; }
        public string Kitap { get => book; set => book = value; }
        public string Yazar { get => author; set => author = value; }
        public DateTime OlusturulmaTarihi { get => createdAt; set => createdAt = value; }
        public string OgrenciNo { get => studentNo; set => studentNo = value; }
    }
}
