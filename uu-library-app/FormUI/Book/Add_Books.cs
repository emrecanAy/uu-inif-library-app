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
using uu_library_app.FormUI.Book.Quick_Menu;
using uu_library_app.FormUI.MailSettings;
using MessageBoxDenemesi;
using uu_library_app.Core.Exceptions;

namespace uu_library_app
{
    public partial class Add_Books : Form
    {
        private Admin _admin;
        public Add_Books(Admin admin)
        { 
            InitializeComponent();
            _admin = admin;
        }

        static MySqlConnection conn = new MySqlConnection(DbConnection.connectionString);
        BookManager bookManager = new BookManager(new BookDal());
        LoggerManager logger = new LoggerManager(new LoggerDal());
        AdminManager adminManager = new AdminManager(new AdminDal());

        MySqlDataAdapter pageAdapter = DataListerToDataAdapter.listBooksForPagination(conn);
        DataSet pageDS;
        int scollVal;

        public void fillData()
        {
            try
            {
                conn.Open();              
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
                cmbKategori.Text = "";

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

            }
            catch (Exception)
            {
                wehMessageBox.Show("Lütfen internet bağlantınızı kontrol edin.\nSorun devam ediyorsa bir yetkiliyle iletişime geçin...",
                "Sunucuya bağlanırken bir hata oluştu!",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
                Application.Exit();
                return;
            }
        }

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
            txtDemirbasNo.Text = "";
            cmbDil.ResetText();
        }
        private void Add_Books_Load(object sender, EventArgs e)
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
            cmbKategori.Text = "";

            #region crud1
            try
            {
                conn.Open();

                pageAdapter = DataListerToDataAdapter.listBooksForPagination(conn);
                pageDS = new DataSet();
                pageAdapter.Fill(pageDS, scollVal, 20, "book");
                dataGridView1.DataSource = pageDS;
                dataGridView1.DataMember = "book";
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
                cmbKategori.Text = "";

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

            }
            catch (Exception)
            {
                wehMessageBox.Show("Lütfen internet bağlantınızı kontrol edin.\nSorun devam ediyorsa bir yetkiliyle iletişime geçin...",
                "Sunucuya bağlanırken bir hata oluştu!",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
                Application.Exit();
                return;
            }
            #endregion
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

        private void txtId_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if(txtAd.Text != "" && txtIsbn.Text != "" && txtSayfaSayisi.Text != "" && txtStokAdet.Text != "" && txtDemirbasNo.Text != "" && txtCiltNo.Text != "")
            {
                string createGUID = System.Guid.NewGuid().ToString();
                try
                {
                    Book bookToAdd = new Book(createGUID, txtAd.Text, cmbDil.SelectedValue.ToString(), cmbYazar.SelectedValue.ToString(), cmbKategori.SelectedValue.ToString(), cmbYayinevi.SelectedValue.ToString(), cmbKonum.SelectedValue.ToString(), Convert.ToInt32(txtSayfaSayisi.Text), txtIsbn.Text, Convert.ToDateTime(dateTime1.Text), Convert.ToInt32(txtCiltNo.Text), Convert.ToInt32(txtStokAdet.Text), txtCevirmen.Text, txtDemirbasNo.Text);
                    bookManager.Add(bookToAdd);
                    Logger log = new Logger(System.Guid.NewGuid().ToString(), _admin.id, "[ " + bookToAdd.Id + " | " + bookToAdd.BookName + " ] " + _admin.FirstName + " " + _admin.LastName + " tarafından eklendi! -Tarih: " + DateTime.Now);
                    logger.Log(log);
                    wehMessageBox.Show("Kitap başarıyla eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    pageAdapter = DataListerToDataAdapter.listBooksForPagination(conn);
                    pageDS = new DataSet();
                    pageAdapter.Fill(pageDS, scollVal, 20, "book");
                    dataGridView1.DataSource = pageDS;
                    clearAllFields();
                }
                catch (Exception)
                {
                    wehMessageBox.Show("İnternet bağlantınızı kontrol ederek tekrar deneyiniz. Sorunun devam etmesi durumunda bir yetkiliyle iletişime geçiniz.", "Bağlantı Hatası!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                wehMessageBox.Show("Gerekli alanları doldurun...", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

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

        private void btnHizliYazar_Click(object sender, EventArgs e)
        {
            AuthorQuick quick = new AuthorQuick(_admin);
            quick.Show();
            fillData();
        }

        private void btnHizliYayinevi_Click(object sender, EventArgs e)
        {
            PublisherQuick quick = new PublisherQuick(_admin);
            quick.Show();
            fillData();
        }

        private void btnHizliDil_Click(object sender, EventArgs e)
        {
            LanguageQuick quick = new LanguageQuick(_admin);
            quick.Show();
            fillData();
        }

        private void btnHizliKategori_Click(object sender, EventArgs e)
        {
            CategoryQuick quick = new CategoryQuick(_admin);
            quick.Show();
            fillData();
        }

        private void btnHizliKonum_Click(object sender, EventArgs e)
        {
            LocationQuick quick = new LocationQuick(_admin);
            quick.Show();
            fillData();
        }

        private void txtIsbn_Click(object sender, EventArgs e)
        {
            this.txtIsbn.Select(0, 0);
        }

        private void txtSayfaSayisi_Click(object sender, EventArgs e)
        {
            this.txtSayfaSayisi.Select(0, 0);
        }

        private void txtStokAdet_Click(object sender, EventArgs e)
        {
            this.txtStokAdet.Select(0, 0);
        }

        private void txtCiltNo_Click(object sender, EventArgs e)
        {
            this.txtCiltNo.Select(0, 0);
        }

        private void txtDemirbasNo_Click(object sender, EventArgs e)
        {
            this.txtDemirbasNo.Select(0, 0);
        }

        private void btnHizliYazar_Leave(object sender, EventArgs e)
        {
            fillData();
        }

        private void btnHizliYayinevi_Leave(object sender, EventArgs e)
        {
            fillData();
        }

        private void btnHizliDil_Leave(object sender, EventArgs e)
        {
            fillData();
        }

        private void btnHizliKategori_Leave(object sender, EventArgs e)
        {
            fillData();
        }

        private void btnHizliKonum_Leave(object sender, EventArgs e)
        {
            fillData();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
