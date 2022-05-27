using MySql.Data.MySqlClient;
using MySql;
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
using uu_library_app.DataAccess.Concrete;
using uu_library_app.Entity.Concrete;
using uu_library_app.Core.Helpers;

namespace uu_library_app
{
    public partial class Category_Operations : Form
    {
        private Admin _admin;
        public Category_Operations()
        {
            InitializeComponent();
        }
        MySqlConnection conn = new MySqlConnection(DbConnection.connectionString);
        LoggerManager logger = new LoggerManager(new LoggerDal());
        CategoryManager manager = new CategoryManager(new CategoryDal());
        private void listDataToTable()
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("Select * From Category WHERE deleted=false", conn);
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

        private void clearAllFields()
        {
            txtId.Clear();
            txtAd.Clear();
        }
        private void Category_Operations_Load(object sender, EventArgs e)
        {
            conn.Open();
            listDataToTable();
            conn.Close();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            string createGUID = System.Guid.NewGuid().ToString();
            if (txtAd.Text == "" || txtAd.Text.Length < 3)
            {
                MessageBox.Show("Lütfen en az üç harf içeren geçerli bir değer giriniz!");
                return;
            }
            Category categoryToAdd = new Category(createGUID, txtAd.Text);

            try
            {
                manager.Add(categoryToAdd);
                Logger log = new Logger(System.Guid.NewGuid().ToString(), _admin.id, "[ " + categoryToAdd.Id + " | " + categoryToAdd.Name + "] eklendi! -Tarih: " + DateTime.Now);
                logger.Log(log);
                listDataToTable();
                clearAllFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eklerken bir hata oluştu. Lütfen tekrar deneyiniz...");
                throw ex;
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                MessageBox.Show("Silmek istediğiniz bölümü seçin!");
                return;
            }
            try
            {
                manager.Delete(txtId.Text);
                //buraya log yapılcak
                listDataToTable();
                clearAllFields();
            }
            catch (Exception)
            {
                MessageBox.Show("Bir hata oluştu. Lütfen tekrar deneyiniz...");
                throw;
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            Category categoryToUpdate = new Category(txtId.Text, txtAd.Text);

            try
            {
                if (txtAd.Text == "")
                {
                    MessageBox.Show("Geçerli bir değer giriniz!");
                    return;
                }
                manager.Update(categoryToUpdate);
                Logger log = new Logger(System.Guid.NewGuid().ToString(), _admin.id, "[ " + categoryToUpdate.Id + " | " + categoryToUpdate.Name + "] güncellendi! -Tarih: " + DateTime.Now);
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

      

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
        }
    }
}
