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
using uu_library_app.Entity.Concrete;

namespace uu_library_app
{
    public partial class Get_Book_Back: Form
    {
        private Admin _admin;
        public Get_Book_Back(Admin admin)
        {
            InitializeComponent();
            _admin = admin;
        }

        MySqlConnection conn = new MySqlConnection(DbConnection.connectionString);
        LoggerManager logger = new LoggerManager(new LoggerDal());
        DepositBookManager depositBookManager = new DepositBookManager(new DepositBookDal());
        BookManager bookManager = new BookManager(new BookDal());

        MySqlDataAdapter pageAdapter;
        DataSet pageDS;
        int scollVal;
        private void listDataToTable()
        {
            DataTable dt = new DataTable();
            MySqlCommand getAllCommand = new MySqlCommand("SELECT DepositBook.depositDate, Student.number, Student.firstName, Student.lastName, Book.bookName'bookName', Book.isbnNumber, Author.firstName'authorFirstName', Author.lastName'authorLastName', Publisher.name'publisherName', Book.id, DepositBook.id, DepositBook.status FROM DepositBook INNER JOIN Student ON DepositBook.studentId = Student.id INNER JOIN Book ON DepositBook.bookId = Book.id INNER JOIN Author ON Book.authorId = Author.id INNER JOIN Publisher ON Book.publisherId = Publisher.id WHERE Student.id=@p1 AND DepositBook.status=0", conn);

            var studentId = dgvOgrenci.CurrentRow.Cells[0].Value.ToString();
            getAllCommand.Parameters.AddWithValue("@p1", studentId);
            MySqlDataAdapter da = new MySqlDataAdapter(getAllCommand);
            da.Fill(dt);
            dgvDahaOnceAlinanKitaplar.DataSource = dt;
            dgvDahaOnceAlinanKitaplar.Columns[0].Width = 100;
            dgvDahaOnceAlinanKitaplar.Columns[1].Visible = false;
            dgvDahaOnceAlinanKitaplar.Columns[0].HeaderText = "Ödünç Tarihi";
            dgvDahaOnceAlinanKitaplar.Columns[4].HeaderText = "Kitap Adı";
            dgvDahaOnceAlinanKitaplar.Columns[2].Visible = false;
            dgvDahaOnceAlinanKitaplar.Columns[3].Visible = false;
            dgvDahaOnceAlinanKitaplar.Columns[5].Visible = false;
            dgvDahaOnceAlinanKitaplar.Columns[6].Visible = false;
            dgvDahaOnceAlinanKitaplar.Columns[7].Visible = false;
            dgvDahaOnceAlinanKitaplar.Columns[8].Visible = false;
            dgvDahaOnceAlinanKitaplar.Columns[9].Visible = false;
            dgvDahaOnceAlinanKitaplar.Columns[10].Visible = false;
            dgvDahaOnceAlinanKitaplar.Columns[11].Visible = false;
        }

        private void Get_Book_Back_Load(object sender, EventArgs e)
        {
            DataListerToTableHelper.listStudentDataToTable(dgvOgrenci, conn);
            dgvDahaOnceAlinanKitaplar.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvDahaOnceAlinanKitaplar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDahaOnceAlinanKitaplar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDahaOnceAlinanKitaplar.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(46, 51, 73);
            dgvDahaOnceAlinanKitaplar.EnableHeadersVisualStyles = false;
            dgvDahaOnceAlinanKitaplar.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvDahaOnceAlinanKitaplar.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(46, 51, 73);
            dgvDahaOnceAlinanKitaplar.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12.0F, FontStyle.Bold);

            dgvDahaOnceAlinanKitaplar.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvDahaOnceAlinanKitaplar.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

        }


       
  

        private void txtOgrenciId_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btnGetir_Click(object sender, EventArgs e)
        {
            
        }

        private void dgvDahaOnceAlinanKitaplar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtKitapId.Text = dgvDahaOnceAlinanKitaplar.Rows[e.RowIndex].Cells[9].Value.ToString();
            txtAd.Text = dgvDahaOnceAlinanKitaplar.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtYayinevi.Text = dgvDahaOnceAlinanKitaplar.Rows[e.RowIndex].Cells[8].Value.ToString();
            txtIsbn.Text = dgvDahaOnceAlinanKitaplar.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtYazar.Text = dgvDahaOnceAlinanKitaplar.Rows[e.RowIndex].Cells[6].Value.ToString()+" "+dgvDahaOnceAlinanKitaplar.Rows[e.RowIndex].Cells[7].Value.ToString();
            txtDepositBookId.Text = dgvDahaOnceAlinanKitaplar.Rows[e.RowIndex].Cells[10].Value.ToString();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {

            Book book = bookManager.getById(txtKitapId.Text);
            book.StockCount = book.StockCount + 1;
            bookManager.Update(book);

            DepositBook depositBook = new DepositBook();
            depositBook.Status = true;
            depositBookManager.Update(depositBook);


            if (txtDepositBookId.Text == "")
            {
                wehMessageBox.Show("Lütfen bir kitap seçiniz!","Dikkat",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }

            try
            {
                depositBookManager.depositBook(txtDepositBookId.Text);
                Logger log = new Logger(System.Guid.NewGuid().ToString(), _admin.id, "[ ÖdünçId:" + depositBook.Id + " | KitapId:" + depositBook.BookId + " ] " + _admin.FirstName + " " + _admin.LastName + " tarafından teslim alındı! -Tarih: " + DateTime.Now);
                logger.Log(log);
                wehMessageBox.Show("Teslim alındı!",
                "Başarılı",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Information);
                listDataToTable();
            }
            catch (Exception)
            {
                MessageBox.Show("Bir hata oluştu! Tekrar deneyin...");
                throw;
            }
            
        }

        private void txtKitapAra_TextChanged(object sender, EventArgs e)
        {
            (dgvDahaOnceAlinanKitaplar.DataSource as DataTable).DefaultView.RowFilter =
            string.Format("bookName LIKE '{0}%' OR bookName LIKE '% {0}%'", txtKitapAra.Text);
        }

        private void dgvOgrenci_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            txtOgrenciId.Text = dgvOgrenci.Rows[e.RowIndex].Cells[0].Value.ToString();
            conn.Open();
            listDataToTable();
            conn.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtKitapId.Text = dgvDahaOnceAlinanKitaplar.Rows[e.RowIndex].Cells[9].Value.ToString();
            txtAd.Text = dgvDahaOnceAlinanKitaplar.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtYayinevi.Text = dgvDahaOnceAlinanKitaplar.Rows[e.RowIndex].Cells[8].Value.ToString();
            txtIsbn.Text = dgvDahaOnceAlinanKitaplar.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtYazar.Text = dgvDahaOnceAlinanKitaplar.Rows[e.RowIndex].Cells[6].Value.ToString() + " " + dgvDahaOnceAlinanKitaplar.Rows[e.RowIndex].Cells[7].Value.ToString();
            txtDepositBookId.Text = dgvDahaOnceAlinanKitaplar.Rows[e.RowIndex].Cells[10].Value.ToString();
        }

        private void wehTextBox1__TextChanged(object sender, EventArgs e)
        {
            (dgvOgrenci.DataSource as DataTable).DefaultView.RowFilter =
            string.Format("number LIKE '{0}%' OR number LIKE '% {0}%'", wehTextBox1.Texts);

        }
    }
}
