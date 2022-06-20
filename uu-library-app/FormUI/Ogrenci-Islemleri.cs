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
using System.Runtime.InteropServices;

namespace uu_library_app.FormUI
{
    public partial class Ogrenci_Islemleri : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
            );
        public Ogrenci_Islemleri()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
        }
        MySqlConnection conn = new MySqlConnection(DbConnection.connectionString);
        StudentManager studentManager = new StudentManager(new StudentDal());
        DepartmentManager departmentManager = new DepartmentManager(new DepartmentDal());
        FacultyManager facultyManager = new FacultyManager(new FacultyDal());
        Student student;

        private void Ogrenci_Islemleri_Load(object sender, EventArgs e)
        {
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
        #region Buttons
        private void btnSorgula_Click(object sender, EventArgs e)
        {
            if (txtOgrNo.Text == "")
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

                if (student.FirstName == "" || student.FirstName == null)
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
                btnOgrenciSil.Enabled = true;

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

        private void btnBelgeYazdir_Click(object sender, EventArgs e)
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
                    doc.Add(new Paragraph(student.Number + " numaralı öğrencimiz " + student.FirstName + " " + student.LastName + " üzerinde kütüphanemize ait teslim edilmemiş kitap ve materyal yoktur."));

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
        }

        private void btnOgrenciSil_Click(object sender, EventArgs e)
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
        #endregion

        private void btnCikis_Click(object sender, EventArgs e)
        {
            btnCikis.BackColor = Color.FromArgb(46, 51, 73);
        }
    }
}
