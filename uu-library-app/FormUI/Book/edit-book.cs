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
    public partial class edit_book : Form
    {
        private Admin _admin;
        public edit_book(Admin admin)
        {
            InitializeComponent();
            _admin = admin;
        }

        MySqlConnection conn = new MySqlConnection(DbConnection.connectionString);
        LoggerManager logger = new LoggerManager(new LoggerDal());
        BookManager bookManager = new BookManager(new BookDal());
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
            cmbDil.Text = "";
            cmbKategori.Text = "";
            cmbKonum.Text = "";
            cmbYayinevi.Text = "";
            cmbYazar.Text = "";
            cmbDil.ResetText();
            txtDemirbasNo.Clear();
        }

        private void edit_book_Load(object sender, EventArgs e)
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

            #region crud1
            conn.Open();
            DataListerToTableHelper.listInnerJoinSomeBookDataToTable(dataGridView1, conn);
            MySqlDataAdapter daCategories = new MySqlDataAdapter(SqlCommandHelper.getCategoriesCommand(conn));
            MySqlDataAdapter daLocations = new MySqlDataAdapter(SqlCommandHelper.getLocationsCommand(conn));
            MySqlDataAdapter daAuthors = new MySqlDataAdapter(SqlCommandHelper.getAuthorsCommandConcatFirstNameAndLastName(conn));
            MySqlDataAdapter daLanguages = new MySqlDataAdapter(SqlCommandHelper.getLanguagesCommand(conn));
            MySqlDataAdapter daPublishers = new MySqlDataAdapter(SqlCommandHelper.getPublishersCommand(conn));
            DataSet dsCategories = new DataSet();
            DataSet dsLocations = new DataSet();
            DataSet dsAuthors = new DataSet();
            DataSet dsLanguages = new DataSet();
            DataSet dsPublishers = new DataSet();
            daCategories.Fill(dsCategories);
            daLocations.Fill(dsLocations);
            daAuthors.Fill(dsAuthors);
            daLanguages.Fill(dsLanguages);
            daPublishers.Fill(dsPublishers);
            SqlCommandHelper.getCategoriesCommand(conn).ExecuteNonQuery();
            SqlCommandHelper.getLocationsCommand(conn).ExecuteNonQuery();
            SqlCommandHelper.getAuthorsCommandConcatFirstNameAndLastName(conn).ExecuteNonQuery();
            SqlCommandHelper.getLanguagesCommand(conn).ExecuteNonQuery();
            SqlCommandHelper.getPublishersCommand(conn).ExecuteNonQuery();
            conn.Close();

            //Kategori
            cmbKategori.DataSource = dsCategories.Tables[0];
            cmbKategori.DisplayMember = "name";
            cmbKategori.ValueMember = "id";

            //Konum
            cmbKonum.DataSource = dsLocations.Tables[0];
            cmbKonum.DisplayMember = "shelf";
            cmbKonum.ValueMember = "id";

            //Yazar
            cmbYazar.DataSource = dsAuthors.Tables[0];
            cmbYazar.DisplayMember = "fullName";
            cmbYazar.ValueMember = "id";

            //Dil
            cmbDil.DataSource = dsLanguages.Tables[0];
            cmbDil.DisplayMember = "language";
            cmbDil.ValueMember = "id";

            //Yayınevi
            cmbYayinevi.DataSource = dsPublishers.Tables[0];
            cmbYayinevi.DisplayMember = "name";
            cmbYayinevi.ValueMember = "id";

            #endregion
        }
        private void btnGuncelle_Click(object sender, EventArgs e)
        {    
            try
            {
                DialogResult dialogResult = wehMessageBox.Show("Güncellemek istediğinize emin misiniz?",
               "Uyarı!",
                 MessageBoxButtons.YesNo,
                 MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    Book bookToUpdate = new Book(txtId.Text, txtAd.Text, cmbDil.SelectedValue.ToString(), cmbYazar.SelectedValue.ToString(), cmbKategori.SelectedValue.ToString(), cmbYayinevi.SelectedValue.ToString(), cmbKonum.SelectedValue.ToString(), Convert.ToInt32(txtSayfaSayisi.Text), txtIsbn.Text, Convert.ToDateTime(dateTime1.Text), Convert.ToInt32(txtCiltNo.Text), Convert.ToInt32(txtStokAdet.Text), txtCevirmen.Text, txtDemirbasNo.Text);
                    Logger log = new Logger(System.Guid.NewGuid().ToString(), _admin.id, "[ " + bookToUpdate.Id + " | " + bookToUpdate.BookName + " ] " + _admin.FirstName + " " + _admin.LastName + " tarafından güncellendi! -Tarih: " + DateTime.Now);
                    bookManager.Update(bookToUpdate);
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

        private void wehTextBox1__TextChanged(object sender, EventArgs e)
        {
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter =
          string.Format("bookName LIKE '{0}%' OR bookName LIKE '% {0}%'", wehTextBox1.Texts);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtCevirmen.Text = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
            txtCiltNo.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString(); //ciltNo eklenecek
            txtIsbn.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            txtSayfaSayisi.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            dateTime1.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
            txtStokAdet.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
            cmbDil.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            cmbKategori.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            cmbKonum.Text = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
            cmbYayinevi.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            cmbYazar.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            cmbDil.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtDemirbasNo.Text = dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString();
        }

        private void txtCevirmen_Click(object sender, EventArgs e)
        {
            this.txtCevirmen.Select(0, 0);
        }

        private void txtIsbn_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void txtIsbn_Click(object sender, EventArgs e)
        {
            this.txtIsbn.Select(0, 0);
        }

        private void txtStokAdet_Click(object sender, EventArgs e)
        {
            this.txtStokAdet.Select(0, 0);
        }

        private void txtSayfaSayisi_Click(object sender, EventArgs e)
        {
            this.txtSayfaSayisi.Select(0, 0);
        }

        private void txtCiltNo_Click(object sender, EventArgs e)
        {
            this.txtCiltNo.Select(0, 0);
        }

        private void txtDemirbasNo_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void txtDemirbasNo_Click(object sender, EventArgs e)
        {
            this.txtDemirbasNo.Select(0, 0);
        }
    }
 }
