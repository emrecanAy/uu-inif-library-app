using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uu_library_app.Core.Migration.AccessDBMigration
{
    public class OldBook
    {
        private string siraNo; //null
        private string demirbasNo;
        private string kitapAdi;
        private int ciltSayisi;
        private int adet;
        private string yazar;
        private string cevirmen;
        private string basimYili; //null
        private string fiyat; //null
        private string ebat; //null
        private int sayfaSayisi;

        public OldBook(string siraNo, string demirbasNo, string kitapAdi, int ciltSayisi, int adet, string yazar, string cevirmen, string basimYili, string fiyat, string ebat, int sayfaSayisi)
        {
            this.SiraNo = siraNo;
            this.DemirbasNo = demirbasNo;
            this.KitapAdi = kitapAdi;
            this.CiltSayisi = ciltSayisi;
            this.Adet = adet;
            this.Yazar = yazar;
            this.Cevirmen = cevirmen;
            this.BasimYili = basimYili;
            this.Fiyat = fiyat;
            this.Ebat = ebat;
            this.SayfaSayisi = sayfaSayisi;
        }

        public string SiraNo { get => siraNo; set => siraNo = value; }
        public string DemirbasNo { get => demirbasNo; set => demirbasNo = value; }
        public string KitapAdi { get => kitapAdi; set => kitapAdi = value; }
        public int CiltSayisi { get => ciltSayisi; set => ciltSayisi = value; }
        public int Adet { get => adet; set => adet = value; }
        public string Yazar { get => yazar; set => yazar = value; }
        public string Cevirmen { get => cevirmen; set => cevirmen = value; }
        public string BasimYili { get => basimYili; set => basimYili = value; }
        public string Fiyat { get => fiyat; set => fiyat = value; }
        public string Ebat { get => ebat; set => ebat = value; }
        public int SayfaSayisi { get => sayfaSayisi; set => sayfaSayisi = value; }
    }
}
