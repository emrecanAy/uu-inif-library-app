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
    public partial class delete_book : Form
    {
        private Admin _admin;
        public delete_book(Admin admin)
        {
            InitializeComponent();
            _admin = admin;
        }

        static MySqlConnection conn = new MySqlConnection(DbConnection.connectionString);
        LoggerManager logger = new LoggerManager(new LoggerDal());
        BookManager bookManager = new BookManager(new BookDal());

        MySqlDataAdapter pageAdapter = DataListerToDataAdapter.listBooksForPagination(conn);
        DataSet pageDS;
        int scollVal;
        private void clearAllFields()
        {
            txtId.Clear();
            txtAd.Clear();
            txtCevirmen.Clear();
            txtCiltNo.Clear();
            txtId.Clear();
            txtIsbn.Clear();
            txtSayfaSayisi.Clear();
            txtStokAdet.Clear();
            txtDil.Text = "";
            txtKategori.Text = "";
            txtKonum.Text = "";
            txtYayinevi.Text = "";
            txtYazar.Text = "";
            txtDil.ResetText();
        }


        private void delete_book_Load(object sender, EventArgs e)
        {
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

            conn.Open();
            DataTable dt = new DataTable();

            MySqlCommand command = new MySqlCommand("SELECT Book.id, Book.bookName, CONCAT( Author.firstName, ' ', Author.lastName ) AS authorFullName, Publisher.name'publisherName', Language.language, Category.name'categoryName', Book.pageCount, Book.isbnNumber, Book.publishDate, Book.stockCount, Location.shelf, Book.interpreter'interpreterName', Book.createdAt, Book.fixtureNo FROM Book INNER JOIN Language ON Book.languageId = Language.id INNER JOIN Author ON Book.authorId = Author.id INNER JOIN Category ON Book.categoryId = Category.id INNER JOIN Publisher ON Book.publisherId = Publisher.id INNER JOIN Location ON Book.locationId = Location.id WHERE Book.deleted=0", conn);
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            da.Fill(dt);
            dataGridView1.DataSource = dt;

    
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Kitap";
            dataGridView1.Columns[2].HeaderText = "Yazar";
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns[11].Visible = false;
            dataGridView1.Columns[12].Visible = false;
            dataGridView1.Columns[13].Visible = false;
            //dataGridView1.Columns[14].Visible = false;

            dataGridView1.RowHeadersVisible = false;
            dataGridView1.DefaultCellStyle.Font = new Font("Nirmala UI", 13);
            conn.Close();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = wehMessageBox.Show("Silmek istediğinize emin misiniz?",
                "Uyarı!",
                  MessageBoxButtons.YesNo,
                  MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
              
                    
                        Book bookToDelete = new Book(txtId.Text, txtAd.Text, txtDil.Text, txtYazar.Text, txtKategori.Text, txtYayinevi.Text, txtKonum.Text, Convert.ToInt32(txtSayfaSayisi.Text), txtIsbn.Text, Convert.ToDateTime(txtYayinlanmaTarihi.Text), Convert.ToInt32(txtCiltNo.Text), Convert.ToInt32(txtStokAdet.Text), txtCevirmen.Text, txtDemirbasNo.Text);
                        Logger log = new Logger(System.Guid.NewGuid().ToString(), _admin.id, "[ " + bookToDelete.Id + " | " + bookToDelete.BookName + " ] " + _admin.FirstName + " " + _admin.LastName + " tarafından silindi! -Tarih: " + DateTime.Now);
                        bookManager.Delete(bookToDelete);
                        logger.Log(log);
                        pageAdapter = DataListerToDataAdapter.listBooksForPagination(conn);
                        pageDS = new DataSet();
                        pageAdapter.Fill(pageDS, scollVal, 20, "book");
                        dataGridView1.DataSource = pageDS;
                        clearAllFields();
                   


                }         
            }
            catch (Exception)
            {
                wehMessageBox.Show("Bu kitap; ödünç verilen kitaplarda bulunan bir öğrenciye veya öğrencilere ait olduğu için öncelikle ödünç kitaplara giderek bu kitaba ait olan ödünç kitabı veya kitapları silmeniz veya teslim almanız gerekmektedir!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                txtId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtYazar.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtYayinevi.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtDil.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtKategori.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                txtSayfaSayisi.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                txtIsbn.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                txtYayinlanmaTarihi.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                txtCiltNo.Text = dataGridView1.Rows[e.RowIndex].Cells[13].Value.ToString();
                txtStokAdet.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                txtKonum.Text = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
                txtCevirmen.Text = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
                txtDemirbasNo.Text = dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString();
            }
        }

        private void wehTextBox1__TextChanged(object sender, EventArgs e)
        {
            pageDS = new DataSet();
            pageAdapter.Fill(pageDS, scollVal, 20, "book");
            dataGridView1.DataSource = pageDS;

            pageDS.Tables[0].DefaultView.RowFilter = string.Format("bookName like '{0}%'", wehTextBox1.Texts);
            dataGridView1.DataSource = pageDS.Tables[0];
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Kitap";
            dataGridView1.Columns[2].HeaderText = "Yazar";
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns[11].Visible = false;
            dataGridView1.Columns[12].Visible = false;
            dataGridView1.Columns[13].Visible = false;
            dataGridView1.Columns[14].Visible = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.DefaultCellStyle.Font = new Font("Nirmala UI", 13);
        }       
    }
}
