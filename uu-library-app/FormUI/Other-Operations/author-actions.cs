using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uu_library_app.Business.Abstract;
using uu_library_app.Business.Concrete;
using uu_library_app.Core.Helpers;
using uu_library_app.DataAccess.Concrete;
using uu_library_app.Entity.Concrete;

namespace uu_library_app
{
    public partial class author_actions : Form
    {

        MySqlConnection conn = new MySqlConnection(DbConnection.connectionString);
        AuthorManager manager = new AuthorManager(new AuthorDal());

        public author_actions()
        {
            InitializeComponent();
        }

        private void listDataToTable()
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("Select * From Author WHERE deleted=false", conn);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[1].HeaderText = "";
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            //dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.DefaultCellStyle.Font = new Font("Nirmala UI", 13);
        }
        private void clearAllFields()
        {
            txtId.Clear();
            txtAd.Clear();
            txtSoyad.Clear();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            string createGUID = System.Guid.NewGuid().ToString();

            if (txtAd.Text == "" || txtSoyad.Text == "" || txtAd.Text.Length < 2 || txtSoyad.Text.Length < 2)
            {
                MessageBox.Show("Lütfen geçerli ve en az 2 karakter içeren değerler giriniz");
                return;
            }

            Author authorToAdd = new Author(createGUID, txtAd.Text, txtSoyad.Text);
            try
            {
                manager.Add(authorToAdd);
                listDataToTable();
                clearAllFields();
            }
            catch (Exception)
            {
                MessageBox.Show("Bir hata oluştu. Tekrar deneyiniz.");
                throw;
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if(txtId.Text == "")
            {
                MessageBox.Show("Silmek istediğiniz konumu seçin!");
                return;
            }
            
            try
            {
                manager.Delete(txtId.Text);
                listDataToTable();
                clearAllFields();
            }
            catch (Exception)
            {
                MessageBox.Show("Bir hata oluştu. Tekrar deneyiniz.");
                throw;
            }
            
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if(txtAd.Text == "" || txtSoyad.Text == "" || txtAd.Text.Length < 2 || txtSoyad.Text.Length < 2)
            {
                MessageBox.Show("Lütfen geçerli ve en az 2 karakter içeren değerler giriniz");
                return;
            }

            Author authorToUpdate = new Author(txtId.Text, txtAd.Text, txtSoyad.Text);
            try
            {
                manager.Update(authorToUpdate);
                listDataToTable();
                clearAllFields();
            }
            catch (Exception)
            {
                MessageBox.Show("Bir hata oluştu. Tekrar deneyiniz.");
                throw;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void author_actions_Load(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                listDataToTable();
                
            }
            catch (Exception)
            {
                MessageBox.Show("Server down!");
            }
            conn.Close();


        }
    }
}
