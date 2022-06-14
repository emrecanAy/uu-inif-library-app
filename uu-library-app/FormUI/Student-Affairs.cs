using iTextSharp.text;
using iTextSharp.text.pdf;
using MessageBoxDenemesi;
using MySql.Data.MySqlClient;
using SautinSoft.Document;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uu_library_app.Business.Concrete;
using uu_library_app.Core.Helpers;
using uu_library_app.DataAccess.Concrete;
using uu_library_app.DataAccess.Concrete.EntityFramework;
using uu_library_app.Entity.Concrete;
using Color = System.Drawing.Color;
using Image = iTextSharp.text.Image;
using Paragraph = iTextSharp.text.Paragraph;

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
        Student student;
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
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.0F, FontStyle.Bold);
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
                student = studentManager.findByNumber(txtOgrNo.Text);
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

        private void button3_Click(object sender, EventArgs e)
        {
            student = studentManager.findByNumber(txtOgrNo.Text);
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Pdf Dosyası|*.pdf";
            save.OverwritePrompt = true;
            save.CreatePrompt = true;
            save.InitialDirectory = @"C:\Desktop";
            save.Title = "Kaydet";

            if (save.ShowDialog() == DialogResult.OK)
            {
                Document doc = new Document(PageSize.A4.Rotate());
                try
                {
                    PdfWriter.GetInstance(doc, new FileStream(save.FileName, FileMode.Create));
                    doc.Open();
                    iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance("https://uludag.edu.tr/img/swglogolar/inegolIsletmeFak.svg");
                    jpg.ScaleToFit(140f, 120f);
                    jpg.SpacingBefore = 10f;
                    jpg.SpacingAfter = 1f;
                    jpg.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
                    doc.Add(jpg);
                    doc.Add(new Paragraph("Uludağ Üniversitesi\nİnegöl İşletme Fakültesi"));
                    doc.Add(new Paragraph(student.Number+" numaralı öğrencimiz "+student.FirstName+" "+student.LastName+" üzerinde kütüphanemize ait teslim edilmemiş kitap ve materyal yoktur."));

                    wehMessageBox.Show("Dosya başarıyla oluşturuldu...",
                    "Başarılı",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Information);
                }
                catch (Exception)
                {

                    throw;
                }

       

                
            }

            








            ////Student student = studentManager.findByNumber(txtOgrenciId.Text);
            ////string filePath = @"C:\Users\Emrecan\Desktop\test.PDF";
            ////string fileResult = @"Result.pdf";
            ////DocumentCore dc = DocumentCore.Load(filePath);

            ////// Find a position to insert text. Before this text: "> in this position".
            ////ContentRange cr = dc.Content.Find(">").FirstOrDefault();

            ////// Insert new text.
            ////if (cr != null)
            ////    cr.Start.Insert(student.FirstName + " " + student.LastName);
            ////dc.Save(fileResult);
            ////System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(filePath) { UseShellExecute = true });
            ////System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(fileResult) { UseShellExecute = true });

            //string oldFile = @"C:\Users\Emrecan\Desktop\test.pdf";
            //string newFile = "newFile.pdf";

            //// open the reader
            //PdfReader reader = new PdfReader(oldFile);
            //iTextSharp.text.Rectangle size = reader.GetPageSizeWithRotation(1);
            //Document document = new Document(size);

            //// open the writer
            //FileStream fs = new FileStream(newFile, FileMode.Create, FileAccess.Write);
            //PdfWriter writer = PdfWriter.GetInstance(document, fs);
            //document.Open();

            //// the pdf content
            //PdfContentByte cb = writer.DirectContent;

            //// select the font properties
            //BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            //cb.SetColorFill(BaseColor.DARK_GRAY);
            //cb.SetFontAndSize(bf, 50);

            //// write the text in the pdf content
            //cb.BeginText();
            //string text = "Some random blablablabla sedkfjgsnıudfjkngmslıjdkfjgnsılkdfljgmnılsjdnfglıdsöfngıkdfgnıkdjfnghıkdfgmhdfögnhkdjfömgnhkdjföngh...";
            //// put the alignment and coordinates here
            //cb.ShowTextAligned(1, text, 300, 520, 0);
            //cb.EndText();
            //cb.BeginText();
            //text = "Other random blabla...";
            //// put the alignment and coordinates here
            //cb.ShowTextAligned(2, text, 100, 200, 0);
            //cb.EndText();

            //// create the new page and add it to the pdf
            //PdfImportedPage page = writer.GetImportedPage(reader, 1);
            //cb.AddTemplate(page, 0, 0);

            //// close the streams and voilá the file should be changed :)
            //document.Close();
            //fs.Close();
            //writer.Close();
            //reader.Close();
        }
    }
}
