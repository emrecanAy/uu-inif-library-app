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
            pageDS = new DataSet();
            pageAdapter.Fill(pageDS, scollVal, 20, "book");
            dataGridView1.DataSource = pageDS;
            dataGridView1.DataMember = "book";
            conn.Close();
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
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.DefaultCellStyle.Font = new Font("Nirmala UI", 13);
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
                    DataListerToTableHelper.listInnerJoinSomeBookDataToTable(dataGridView1, conn);
                    clearAllFields();

                }         
            }
            catch (Exception)
            {
                wehMessageBox.Show("Lütfen gerekli tüm alanları doldurun!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
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

            dataGridView1.RowHeadersVisible = false;
            dataGridView1.DefaultCellStyle.Font = new Font("Nirmala UI", 13);
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            scollVal = scollVal - 20;
            if (scollVal <= 0)
            {
                scollVal = 0;
            }
            pageDS.Clear();
            pageAdapter.Fill(pageDS, scollVal, 20, "book");
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            scollVal = scollVal + 20;
            if (scollVal > 50)
            {
                scollVal = bookManager.getAll().Count();
            }
            pageDS.Clear();
            pageAdapter.Fill(pageDS, scollVal, 20, "book");
        }
    }
}
