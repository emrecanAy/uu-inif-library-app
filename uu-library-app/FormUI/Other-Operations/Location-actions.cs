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

namespace uu_library_app.FormUI.Other_Operations
{
    public partial class Location_actions : Form
    {
        private Admin _admin;

        public Location_actions(Admin admin)
        {
            InitializeComponent();
            _admin = admin;
        }

        MySqlConnection conn = new MySqlConnection(DbConnection.connectionString);
        LoggerManager logger = new LoggerManager(new LoggerDal());
        LocationManager manager = new LocationManager(new LocationDal());

        private void clearAllFields()
        {
            txtId.Clear();
            txtAd.Clear();
        }

        private void listDataToTable()
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT Location.id, Location.shelf, Category.name FROM Location INNER JOIN Category ON Location.categoryId=Category.id WHERE Location.deleted=0", conn);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[1].HeaderText = "Konum";
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[2].HeaderText = "Kategori";
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.DefaultCellStyle.Font = new Font("Nirmala UI", 13);
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
           
        }

        private void Location_actions_Load(object sender, EventArgs e)
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
            conn.Open();
            listDataToTable();
            MySqlDataAdapter daCategories = new MySqlDataAdapter(SqlCommandHelper.getCategoriesCommand(conn));
            DataSet dsCategories = new DataSet();
            daCategories.Fill(dsCategories);
            SqlCommandHelper.getCategoriesCommand(conn).ExecuteNonQuery();

            //Konum
            cmbKategori.DataSource = dsCategories.Tables[0];
            cmbKategori.DisplayMember = "name";
            cmbKategori.ValueMember = "id";
            conn.Close();
        }

        private void btnEkle_Click_1(object sender, EventArgs e)
        {
            string createGUID = System.Guid.NewGuid().ToString();
            if (txtAd.Text == "")
            {
                MessageBox.Show("Lütfen geçerli bir değer giriniz!");
                return;
            }

            Location locationToAdd = new Location(createGUID, txtAd.Text, cmbKategori.SelectedValue.ToString());
            try
            {
                manager.Add(locationToAdd);
                Logger log = new Logger(System.Guid.NewGuid().ToString(), _admin.id, "[ KonumId: " + locationToAdd.Id + " | " + locationToAdd.Shelf + " | KategoriId: " + locationToAdd.CategoryId + "" + "]" + _admin.FirstName + " " + _admin.LastName + " tarafından eklendi! -Tarih: " + DateTime.Now);
                logger.Log(log);
                listDataToTable();
                clearAllFields();
            }
            catch (Exception)
            {
                MessageBox.Show("Eklerken bir hata oluştu. Lütfen tekrar deneyiniz...");
                throw;
            }
        }

        private void btnSil_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (txtId.Text == "")
                {
                    MessageBox.Show("Lütfen silinecek dili seçin...");
                    return;
                }

                DialogResult dialogResult = wehMessageBox.Show("Silmek istediğinize emin misiniz? Bu işlem bu konuma ait olan bütün kitapları da silecektir!",
               "Uyarı!",
                 MessageBoxButtons.YesNo,
                 MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    Location location = new Location(txtId.Text, txtAd.Text, cmbKategori.SelectedValue.ToString());
                    Logger log = new Logger(System.Guid.NewGuid().ToString(), _admin.id, "[ KonumId: " + location.Id + " | " + location.Shelf + " | KategoriId: " + location.CategoryId + "" + "]" + _admin.FirstName + " " + _admin.LastName + " tarafından silindi! -Tarih: " + DateTime.Now);
                    logger.Log(log);
                    manager.Delete(location);
                    listDataToTable();
                    clearAllFields();
                }


            }
            catch (Exception)
            {
                MessageBox.Show("Bir hata oluştu. Lütfen tekrar deneyiniz...");
                throw;
            }
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            txtId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            cmbKategori.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void btnGuncelle_Click_1(object sender, EventArgs e)
        {
            Location locationToUpdate = new Location(txtId.Text, txtAd.Text, cmbKategori.SelectedValue.ToString());

            try
            {
                if (txtAd.Text == "")
                {
                    MessageBox.Show("Geçerli bir değer giriniz!");
                    return;
                }

                DialogResult dialogResult = wehMessageBox.Show("Güncellemek istediğinize emin misiniz?",
               "Uyarı!",
                 MessageBoxButtons.YesNo,
                 MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    manager.Update(locationToUpdate);
                    Logger log = new Logger(System.Guid.NewGuid().ToString(), _admin.id, "[ KonumId: " + locationToUpdate.Id + " | " + locationToUpdate.Shelf + " | KategoriId: " + locationToUpdate.CategoryId + "" + "]" + _admin.FirstName + " " + _admin.LastName + " tarafından güncellendi! -Tarih: " + DateTime.Now);
                    logger.Log(log);
                    listDataToTable();
                    clearAllFields();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Bir hata oluştu. Lütfen tekrar deneyiniz...");
                throw;
            }
        }
    }
}
