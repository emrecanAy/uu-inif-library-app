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
    public partial class Language_Operations : Form
    {
        private Admin _admin;
        public Language_Operations(Admin admin)
        {
            InitializeComponent();
            _admin = admin;
        }

        MySqlConnection conn = new MySqlConnection(DbConnection.connectionString);
        LoggerManager logger = new LoggerManager(new LoggerDal());
        LanguageManager manager = new LanguageManager(new LanguageDal());

        private void clearAllFields()
        {
            txtId.Clear();
            txtDil.Clear();
        }

        private void listDataToTable()
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("Select * From Language WHERE deleted=false", conn);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[1].HeaderText = "";
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.DefaultCellStyle.Font = new Font("Nirmala UI", 13);
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            string createGUID = System.Guid.NewGuid().ToString();
            if (txtDil.Text == "" || txtDil.Text.Length < 3)
            {
                MessageBox.Show("Lütfen en az üç harf içeren geçerli bir değer giriniz!");
                return;
            }
            Language languageToAdd = new Language(createGUID, txtDil.Text);

            try
            {
                manager.Add(languageToAdd);
                Logger log = new Logger(System.Guid.NewGuid().ToString(), _admin.id, "[ " + languageToAdd.Id + " | " + languageToAdd.LanguageName + "] eklendi! -Tarih: " + DateTime.Now);
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

        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtId.Text == "")
                {
                    MessageBox.Show("Lütfen silinecek dili seçin...");
                    return;
                }
                manager.Delete(txtId.Text);
                //buraya log eklenecek
                listDataToTable();
                clearAllFields();
                MessageBox.Show("Başarıyla silindi.");
            }
            catch (Exception)
            {
                MessageBox.Show("Bir hata oluştu. Lütfen tekrar deneyiniz...");
                throw;
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            Language languageToUpdate = new Language(txtId.Text, txtDil.Text);

            try
            {
                if (txtDil.Text == "" && txtDil.Text.Length < 3)
                {
                    MessageBox.Show("Geçerli bir değer giriniz!");
                    return;
                }
                manager.Update(languageToUpdate);
                Logger log = new Logger(System.Guid.NewGuid().ToString(), _admin.id, "[ " + languageToUpdate.Id + " | " + languageToUpdate.LanguageName + "] güncellendi! -Tarih: " + DateTime.Now);
                logger.Log(log);
                listDataToTable();
                clearAllFields();
            }
            catch (Exception)
            {
                MessageBox.Show("Bir hata oluştu. Lütfen tekrar deneyiniz...");
                throw;
            }
        }

        private void Language_Operations_Load(object sender, EventArgs e)
        {
            conn.Open();
            listDataToTable();
            conn.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtDil.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
        }
    }
}
