using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace uu_library_app
{
    public partial class Student_Affairs : Form
    {
        public Student_Affairs()
        {
            InitializeComponent();
        }

        private void Student_Affairs_Load(object sender, EventArgs e)
        {
            /*
            okul numarası textboxa girilecek.
            o okul numarasına ait öğrenciye select sorgusu atılacak. StudentDal'a findByStudentId metodu yazılacak.
            gelen data Öğrenci Bilgileri paneline doldurulacak.
            
            Aynı zamanda o öğrenciye ait zimmetlenmiş kitaplara sorgu atılacak WHERE şartı teslim etmediği kitaplar olacak. DepositBook'a findAllByStudentId metodu yazılacak.
            gelen data datagride basılacak. Eğer teslim etmediği kitap varsa teslim etmesi beklenecek.
            eğer data boş gelirse demek ki teslim etmediği kitap yok demektir.
            Dolayısıyla yeşil bir bildirim mesajı çıkarılacak. "Öğrenci student.name bütün kitapları teslim etmiştir!"
            Sonra Belge Yazdır ile belgeyi yazdıracak. Belgenin içerisinde taslak olarak string bir ifade bekleyen boşluk olacak. pdfYazdir(string ogrenciAdi, string okulno)
            Belge Tasarımı. İçerik: Öğrenci Görev: Windows Form'da kullanılabilecek PdfExport yöntemlerini araştır ve PDF'e yazı yazabilmeyi araştır.
             
             */

        }
    }
}
