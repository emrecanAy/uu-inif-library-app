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
using uu_library_app.FormUI.Deposit;

namespace uu_library_app
{
    public partial class Borrowing_Book : Form
    {
        private Admin _admin;
        public Borrowing_Book(Admin admin)
        {
            InitializeComponent();
            _admin = admin;
        }

        MySqlConnection conn = new MySqlConnection(DbConnection.connectionString);
        LoggerManager logger = new LoggerManager(new LoggerDal());
        DepositBookManager depositBookManager = new DepositBookManager(new DepositBookDal());
        StudentManager studentManager = new StudentManager(new StudentDal());
        BookManager bookManager = new BookManager(new BookDal());
        SettingsManager settingsManager = new SettingsManager(new SettingsDal());

        private void Borrowing_Book_Load(object sender, EventArgs e)
        {
            dgvDeneme.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvDeneme.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDeneme.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDeneme.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(46, 51, 73);
            dgvDeneme.EnableHeadersVisualStyles = false;
            dgvDeneme.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvDeneme.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(46, 51, 73);
            dgvDeneme.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12.0F, FontStyle.Bold);
            DataListerToTableHelper.listBorrowingBookStudentDataToTable(dgvDeneme, conn);
            DataListerToTableHelper.listInnerJoinBorrowingBookDataToTable(dataGridView2, conn);
            dgvDeneme.RowTemplate.Height = 50;

            dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(46, 51, 73);
            dataGridView2.EnableHeadersVisualStyles = false;
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView2.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(46, 51, 73);
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9.0F, FontStyle.Bold);
            dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView2.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvDeneme.ScrollBars = ScrollBars.None;

            Dictionary<string, string> comboSourceDataTypes = new Dictionary<string, string>();
            comboSourceDataTypes.Add("student", "Öğrenci");
            comboSourceDataTypes.Add("personnel", "Personel");
            cmbKisi.DataSource = new BindingSource(comboSourceDataTypes, null);
            cmbKisi.DisplayMember = "Value";
            cmbKisi.ValueMember = "Key";

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        DataView dataView = new DataView();
        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (txtKitapId.Text == "" || txtKitapAd.Text == "")
            {
                wehMessageBox.Show("Ödünç verilecek kitabı seçin...", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtOgrenciId.Text == "" || txtAdSoyad.Text == "")
            {
                wehMessageBox.Show("Ödünç verilecek öğrenciyi seçin...", "Dikkat!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string createGUID = System.Guid.NewGuid().ToString();
                Book book = bookManager.getById(txtKitapId.Text);
                Console.WriteLine(book.BookName);
                DepositBook depositBookToAdd = new DepositBook(createGUID, txtOgrenciId.Text, txtKitapId.Text, DateTime.Now);
                Logger log = new Logger(System.Guid.NewGuid().ToString(), _admin.id, "[KitapId:" + depositBookToAdd.BookId + "| ÖğrenciId:" + depositBookToAdd.StudentId + " ] " + _admin.FirstName + " " + _admin.LastName + " tarafından ödünç verildi! -Tarih: " + DateTime.Now);

                if (book.StockCount == 0)
                {
                    wehMessageBox.Show("Kütüphanede bulunan tüm " + book.BookName + " kitapları daha önce ödünç verildiği için kütüphanede aktif olarak yoktur!", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                depositBookManager.Add(depositBookToAdd);
                logger.Log(log);
                wehMessageBox.Show("Kitap başarıyla ödünç verildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                book.StockCount = book.StockCount - 1;
                bookManager.Update(book);
                Console.WriteLine("Stok adet: " + book.StockCount);

            }
            catch (Exception ex)
            {
                wehMessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
  
        }

        private void txtKitapId_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtKitapId.Text = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtKitapAd.Text = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtYayinevi.Text = dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtYazar.Text = dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();

            DataListerToTableHelper.GetStudentsWhoHasThatBook(depositBookManager, settingsManager, conn, dgvUyeler, txtKitapId.Text);
            dgvUyeler.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvUyeler.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

        }

        private void wehTextBox1__TextChanged(object sender, EventArgs e)
        {
            (dgvDeneme.DataSource as DataTable).DefaultView.RowFilter =
           string.Format("number LIKE '{0}%' OR number LIKE '% {0}%'", wehTextBox1.Texts);
        }

        private void dgvDeneme_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(cmbKisi.SelectedValue.ToString() == "student")
            {
                txtOgrenciId.Text = dgvDeneme.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtAdSoyad.Text = dgvDeneme.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtOkulNo.Text = dgvDeneme.Rows[e.RowIndex].Cells[3].Value.ToString();
            }
            else
            {
                txtOgrenciId.Text = dgvDeneme.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtAdSoyad.Text = dgvDeneme.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtOkulNo.Text = dgvDeneme.Rows[e.RowIndex].Cells[4].Value.ToString();
            }
           
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void wehTextBox2__TextChanged(object sender, EventArgs e)
        {
            (dataGridView2.DataSource as DataTable).DefaultView.RowFilter =
           string.Format("bookName LIKE '{0}%' OR bookName LIKE '% {0}%'", wehTextBox2.Texts);
        }

        private void cmbKisi_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtAdSoyad.Text = "";
            txtOkulNo.Text = "";
            if(cmbKisi.SelectedValue.ToString() == "personnel")
            {
                lblOkulNo.Text = "Fakülte";
                DataListerToTableHelper.listBorrowingBookPersonnelDataToTable(dgvDeneme, conn);

            }
            if (cmbKisi.SelectedValue.ToString() == "student")
            {
                lblOkulNo.Text = "Okul No";
                DataListerToTableHelper.listBorrowingBookStudentDataToTable(dgvDeneme, conn);
            }

        }
    }
}
