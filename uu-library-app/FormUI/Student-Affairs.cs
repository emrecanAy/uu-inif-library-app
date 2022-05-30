using MessageBoxDenemesi;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uu_library_app.Business.Concrete;
using uu_library_app.Core.Helpers;
using uu_library_app.DataAccess.Concrete;
using uu_library_app.DataAccess.Concrete.EntityFramework;
using uu_library_app.Entity.Concrete;

namespace uu_library_app
{
    public partial class Student_Affairs : Form
    {
        public Student_Affairs()
        {
            InitializeComponent();
        }

        MySqlConnection conn = new MySqlConnection(DbConnection.connectionString);
        StudentManager studentManager = new StudentManager(new StudentDal());
        DepartmentManager departmentManager = new DepartmentManager(new DepartmentDal());
        FacultyManager facultyManager = new FacultyManager(new FacultyDal());
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

            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(46, 51, 73);
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(46, 51, 73);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9.0F, FontStyle.Bold);
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

       
        private void button2_Click(object sender, EventArgs e)
        {
            if(txtOgrNo.Text == "")
            {
                wehMessageBox.Show("Lütfen bir öğrenci numarası girin!",
                  "Uyarı!",
                  MessageBoxButtons.OK,
                   MessageBoxIcon.Warning);
                return;
            }
            try
            {
                Student student = studentManager.findByNumber(txtOgrNo.Text);

                if(student.FirstName == "" || student.FirstName == null)
                {
                    wehMessageBox.Show("Bu numaraya ait öğrenci bulunamadı!",
                  "Uyarı!",
                  MessageBoxButtons.OK,
                   MessageBoxIcon.Warning);
                    return;
                }
                txtAd.Text = student.FirstName;
                txtSoyad.Text = student.LastName;
                txtEmail.Text = student.Email;
                txtFakulte.Text = facultyManager.FindById(student.FacultyId).Name;
                txtBolum.Text = departmentManager.FindById(student.DepartmentId).Name;
                txtOgrenciId.Text = student.Id;
                button1.Enabled = true;

                DataListerHelper.listUndepositBooksDataToTableConcat(dataGridView1, conn, student.Id);

            }
            catch (Exception)
            {
                wehMessageBox.Show("Bu numaraya ait öğrenci bulunamadı!",
                  "Uyarı!",
                  MessageBoxButtons.OK,
                   MessageBoxIcon.Warning);
                return;
            }
            
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = wehMessageBox.Show("Öğrenciyi silmek istediğinize emin misiniz?",
                "Uyarı!",
                  MessageBoxButtons.YesNo,
                  MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                Student student = studentManager.findByNumber(txtOgrenciId.Text);
                studentManager.Delete(student);
                txtAd.Text = "";
                txtBolum.Text = "";
                txtEmail.Text = "";
                txtFakulte.Text = "";
                txtSoyad.Text = "";
                txtOgrenciId.Text = "";
                txtOgrNo.Text = "";
                

            }
            
        }
    }
}
