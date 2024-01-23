using MessageBoxDenemesi;
using MySql.Data.MySqlClient;
using PagedList;
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
        BookManager bookManager = new BookManager(new BookDal());
        SettingsManager settingsManager = new SettingsManager(new SettingsDal());


        MySqlDataAdapter pageAdapter;
        DataSet pageDS;
        int scollVal;
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
            dgvDeneme.RowTemplate.Height = 50;

            pageAdapter = DataListerToDataAdapter.listBooksForPagination(conn);
            pageDS = new DataSet();
            pageAdapter.Fill(pageDS, scollVal, 40, "book");
            dataGridView2.DataSource = pageDS;
            dataGridView2.DataMember = "book";
            dataGridView2.Columns[0].Visible = false;
            dataGridView2.Columns[1].HeaderText = "Kitap";
            dataGridView2.Columns[2].HeaderText = "Yazar";
            dataGridView2.Columns[3].Visible = false;
            dataGridView2.Columns[4].Visible = false;
            dataGridView2.Columns[5].Visible = false;
            dataGridView2.Columns[6].Visible = false;
            dataGridView2.Columns[7].Visible = false;
            dataGridView2.Columns[8].Visible = false;
            dataGridView2.Columns[9].Visible = false;
            dataGridView2.Columns[10].Visible = false;
            dataGridView2.Columns[11].Visible = false;
            dataGridView2.Columns[12].Visible = false;
            dataGridView2.Columns[13].Visible = false;
            dataGridView2.Columns[14].Visible = false;
            dataGridView2.DefaultCellStyle.Font = new Font("Nirmala UI", 13);
        

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
        private void btnNext_Click(object sender, EventArgs e)
        {
            scollVal = scollVal + 20;
            if (scollVal > 50)
            {
                scollVal = bookManager.getAll().Count();
            }
            pageDS.Clear();
            pageAdapter.Fill(pageDS, scollVal, 40, "book");
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            scollVal = scollVal - 20;
            if (scollVal <= 0)
            {
                scollVal = 0;
            }
            pageDS.Clear();
            pageAdapter.Fill(pageDS, scollVal, 40, "book");
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
            if(e.RowIndex >= 0)
            {
                txtKitapId.Text = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtKitapAd.Text = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtYayinevi.Text = dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtYazar.Text = dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();

                DataListerToTableHelper.GetStudentsWhoHasThatBook(depositBookManager, settingsManager, conn, dgvUyeler, txtKitapId.Text);
                dgvUyeler.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dgvUyeler.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            }
            

        }

        private void wehTextBox1__TextChanged(object sender, EventArgs e)
        {
            (dgvDeneme.DataSource as DataTable).DefaultView.RowFilter =
           string.Format("number LIKE '{0}%' OR number LIKE '% {0}%'", wehTextBox1.Texts);
        }

        private void dgvDeneme_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                if (cmbKisi.SelectedValue.ToString() == "student")
                {
                    txtOgrenciId.Text = dgvDeneme.Rows[e.RowIndex].Cells[0].Value.ToString();
                    txtAdSoyad.Text = dgvDeneme.Rows[e.RowIndex].Cells[1].Value.ToString();
                    txtOkulNo.Text = dgvDeneme.Rows[e.RowIndex].Cells[3].Value.ToString();

                }
                else
                {
                    txtOgrenciId.Text = dgvDeneme.Rows[e.RowIndex].Cells[0].Value.ToString();
                    txtAdSoyad.Text = dgvDeneme.Rows[e.RowIndex].Cells[1].Value.ToString();
                    txtOkulNo.Text = dgvDeneme.Rows[e.RowIndex].Cells[2].Value.ToString();
                }
            }
           
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void wehTextBox2__TextChanged(object sender, EventArgs e)
        {
            if(txtKitapAra.Text == "")
            {
                pageAdapter = DataListerToDataAdapter.listBooksForPagination(conn);
                pageDS = new DataSet();
                pageAdapter.Fill(pageDS, scollVal, 40, "book");
                dataGridView2.DataSource = pageDS;
                dataGridView2.DataMember = "book";
                dataGridView2.Columns[0].Visible = false;
                dataGridView2.Columns[1].HeaderText = "Kitap";
                dataGridView2.Columns[2].HeaderText = "Yazar";
                dataGridView2.Columns[3].Visible = false;
                dataGridView2.Columns[4].Visible = false;
                dataGridView2.Columns[5].Visible = false;
                dataGridView2.Columns[6].Visible = false;
                dataGridView2.Columns[7].Visible = false;
                dataGridView2.Columns[8].Visible = false;
                dataGridView2.Columns[9].Visible = false;
                dataGridView2.Columns[10].Visible = false;
                dataGridView2.Columns[11].Visible = false;
                dataGridView2.Columns[12].Visible = false;
                dataGridView2.Columns[13].Visible = false;
                dataGridView2.Columns[14].Visible = false;
                dataGridView2.DefaultCellStyle.Font = new Font("Nirmala UI", 13);
            }
            else
            {
                //DataTable dt = new DataTable();
                //MySqlCommand commandToGetAllByName = new MySqlCommand("SELECT Book.id, Book.bookName, CONCAT( Author.firstName, ' ', Author.lastName ) AS authorFullName, Publisher.name'publisherName', Language.language, Category.name'categoryName', Book.pageCount, Book.isbnNumber, Book.publishDate, Book.stockCount, Location.shelf, Book.interpreter'interpreterName', Book.createdAt, Book.publishCount, Book.fixtureNo FROM Book INNER JOIN Language ON Book.languageId = Language.id INNER JOIN Author ON Book.authorId = Author.id INNER JOIN Category ON Book.categoryId = Category.id INNER JOIN Publisher ON Book.publisherId = Publisher.id INNER JOIN Location ON Book.locationId = Location.id WHERE Book.bookName LIKE '%' "+ txtKitapAra.Text +" '%' AND Book.deleted=0", conn);
                ////commandToGetAllByName.Parameters.AddWithValue("@p1", txtKitapAra.Text);
                //MySqlDataAdapter da = new MySqlDataAdapter(commandToGetAllByName);
                //da.Fill(dt);

                ////pageAdapter = DataListerToDataAdapter.findAndListBooksForPagination(conn, txtKitapAra.Text);
                ////pageDS = new DataSet();
                //////pageAdapter.Fill(pageDS, scollVal, 20, "book");
                //////dataGridView2.DataSource = pageDS;
                ////DataTable dt = new DataTable();
                ////pageAdapter.Fill(dt);
                ////dataGridView2.DataSource = dt;

                //////pageDS.Tables[0].DefaultView.RowFilter = string.Format("bookName like '{0}%'", wehTextBox2.Texts);
                //////dataGridView2.DataSource = pageDS;
                //dataGridView2.DataSource = dt;
                //dataGridView2.Columns[0].Visible = false;
                //dataGridView2.Columns[1].HeaderText = "Kitap";
                //dataGridView2.Columns[2].HeaderText = "Yazar";
                //dataGridView2.Columns[3].Visible = false;
                //dataGridView2.Columns[4].Visible = false;
                //dataGridView2.Columns[5].Visible = false;
                //dataGridView2.Columns[6].Visible = false;
                //dataGridView2.Columns[7].Visible = false;
                //dataGridView2.Columns[8].Visible = false;
                //dataGridView2.Columns[9].Visible = false;
                //dataGridView2.Columns[10].Visible = false;
                //dataGridView2.Columns[11].Visible = false;
                //dataGridView2.Columns[12].Visible = false;
                //dataGridView2.Columns[13].Visible = false;
                //dataGridView2.Columns[14].Visible = false;
                //dataGridView2.DefaultCellStyle.Font = new Font("Nirmala UI", 13);
            }
            

        }

        private void cmbKisi_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtAdSoyad.Text = "";
            txtOkulNo.Text = "";
            if(cmbKisi.SelectedValue.ToString() == "personnel")
            {
                lblOkulNo.Text = "Sicil No";
                DataListerToTableHelper.listBorrowingBookPersonnelDataToTable(dgvDeneme, conn);

            }
            if (cmbKisi.SelectedValue.ToString() == "student")
            {
                lblOkulNo.Text = "Okul No";
                DataListerToTableHelper.listBorrowingBookStudentDataToTable(dgvDeneme, conn);
              
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            MySqlCommand commandToGetAllByName = new MySqlCommand("SELECT Book.id, Book.bookName, CONCAT( Author.firstName, ' ', Author.lastName ) AS authorFullName, Publisher.name'publisherName', Language.language, Category.name'categoryName', Book.pageCount, Book.isbnNumber, Book.publishDate, Book.stockCount, Location.shelf, Book.interpreter'interpreterName', Book.createdAt, Book.publishCount, Book.fixtureNo FROM Book INNER JOIN Language ON Book.languageId = Language.id INNER JOIN Author ON Book.authorId = Author.id INNER JOIN Category ON Book.categoryId = Category.id INNER JOIN Publisher ON Book.publisherId = Publisher.id INNER JOIN Location ON Book.locationId = Location.id WHERE Book.bookName LIKE @p1 AND Book.deleted=0", conn);
            commandToGetAllByName.Parameters.AddWithValue("@p1", string.Format("%{0}%", txtKitapAra.Text));
            MySqlDataAdapter da = new MySqlDataAdapter(commandToGetAllByName);
            da.Fill(dt);

            //pageAdapter = DataListerToDataAdapter.findAndListBooksForPagination(conn, txtKitapAra.Text);
            //pageDS = new DataSet();
            ////pageAdapter.Fill(pageDS, scollVal, 20, "book");
            ////dataGridView2.DataSource = pageDS;
            //DataTable dt = new DataTable();
            //pageAdapter.Fill(dt);
            //dataGridView2.DataSource = dt;

            ////pageDS.Tables[0].DefaultView.RowFilter = string.Format("bookName like '{0}%'", wehTextBox2.Texts);
            ////dataGridView2.DataSource = pageDS;
            dataGridView2.DataSource = dt;
            dataGridView2.Columns[0].Visible = false;
            dataGridView2.Columns[1].HeaderText = "Kitap";
            dataGridView2.Columns[2].HeaderText = "Yazar";
            dataGridView2.Columns[3].Visible = false;
            dataGridView2.Columns[4].Visible = false;
            dataGridView2.Columns[5].Visible = false;
            dataGridView2.Columns[6].Visible = false;
            dataGridView2.Columns[7].Visible = false;
            dataGridView2.Columns[8].Visible = false;
            dataGridView2.Columns[9].Visible = false;
            dataGridView2.Columns[10].Visible = false;
            dataGridView2.Columns[11].Visible = false;
            dataGridView2.Columns[12].Visible = false;
            dataGridView2.Columns[13].Visible = false;
            dataGridView2.Columns[14].Visible = false;
            dataGridView2.DefaultCellStyle.Font = new Font("Nirmala UI", 13);
        }
    }
}
