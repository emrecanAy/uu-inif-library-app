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
    public partial class Add_Books : Form
    {
        public Add_Books()
        { 
            InitializeComponent();
        }

        MySqlConnection conn = new MySqlConnection(DbConnection.connectionString);
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
            dataGridView1.ScrollBars = ScrollBars.None;
            cmbKategori.Text = "";
            if (cmbKategori.Text == "")
                cmbKategori.Text = "Select one of the answers";

            #region crud1
            try
            {
                conn.Open();
                DataListerToTableHelper.listInnerJoinSomeBookDataToTable(dataGridView1, conn);
                MySqlDataAdapter daCategories = new MySqlDataAdapter(SqlCommandHelper.getCategoriesCommand(conn));
                MySqlDataAdapter daLocations = new MySqlDataAdapter(SqlCommandHelper.getLocationsCommand(conn));
                MySqlDataAdapter daAuthors = new MySqlDataAdapter(SqlCommandHelper.getAuthorsCommand(conn));
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
                SqlCommandHelper.getAuthorsCommand(conn).ExecuteNonQuery();
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
                cmbYazar.DisplayMember = "firstName"; //Tekrar bakılacak. Ad ve soyad comboboxta yanyana yazması lazım.
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
                MessageBox.Show("Lütfen internet bağlantınızı kontrol edin.\nSorun devam ediyorsa bir yetkiliyle iletişime geçin...", "Sunucuya bağlanırken bir hata oluştu!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return;
            }
            

            

            #endregion
        }

        

      

        private void txtId_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            string createGUID = System.Guid.NewGuid().ToString();
            if (txtAd.Text == "" || txtIsbn.Text == "" || txtSayfaSayisi.Text == "" || txtStokAdet.Text == "" || cmbDil.Text == "" || cmbKategori.Text == "" || cmbKonum.Text == "" || cmbYayinevi.Text == "" || cmbYazar.Text == "")
            {

                MessageBox.Show("Tüm değerleri giriniz...");
                return;
            }
            Book bookToAdd = new Book(createGUID, txtAd.Text, cmbDil.SelectedValue.ToString(), cmbYazar.SelectedValue.ToString(), cmbKategori.SelectedValue.ToString(), cmbYayinevi.SelectedValue.ToString(), cmbKonum.SelectedValue.ToString(), Convert.ToInt32(txtSayfaSayisi.Text), txtIsbn.Text, Convert.ToDateTime(dateTime1.Text), Convert.ToInt32(txtCiltNo.Text), Convert.ToInt32(txtStokAdet.Text), txtCevirmen.Text);
            try
            {
                bookManager.Add(bookToAdd);
                MessageBox.Show("Başarıyla eklendi!");
                DataListerToTableHelper.listInnerJoinSomeBookDataToTable(dataGridView1, conn);
                clearAllFields();
            }
            catch (Exception)
            {
                MessageBox.Show("Bir hata oluştu. Tekrar deneyin!");
                throw;
            }


            /*
              Kütüphaneci yeni kitap eklerken ekleyeceği kitabı öncelikle kitaplar kısmında arayacak.
              Varsa güncelle kısmından countunu artıracak.
              Yoksa da yeni ekleyecek.
            */

            /*
             Kitap ekle kısmında ufak bir buton olacak. O butona tıkladığında yeni form açılacak.
             */
        }

        private void wehTextBox1__TextChanged(object sender, EventArgs e)
        {
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter =
          string.Format("bookName LIKE '{0}%' OR bookName LIKE '% {0}%'", wehTextBox1.Texts);
        }
    }
}
