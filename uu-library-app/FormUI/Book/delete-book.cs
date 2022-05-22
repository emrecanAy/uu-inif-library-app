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

namespace uu_library_app
{
    public partial class delete_book : Form
    {
        public delete_book()
        {
            InitializeComponent();
        }

        MySqlConnection conn = new MySqlConnection(DbConnection.connectionString);
        BookManager bookManager = new BookManager(new BookDal());
        private void listDataToTable()
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM Book WHERE deleted=false", conn);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[3].HeaderText = "Yazar";
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[5].HeaderText = "Yayınevi";
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[1].HeaderText = "İsim";
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns[11].Visible = false;
            dataGridView1.Columns[12].Visible = false;
            dataGridView1.Columns[13].Visible = false;
            dataGridView1.Columns[14].Visible = false;
            dataGridView1.Columns[15].Visible = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.DefaultCellStyle.Font = new Font("Nirmala UI", 13);
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
            cmbDil.ResetText();
        }


        private void delete_book_Load(object sender, EventArgs e)
        {

            #region
            conn.Open();
            listDataToTable();
            conn.Close();
            #endregion
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtCevirmen.Text = dataGridView1.Rows[e.RowIndex].Cells[14].Value.ToString();
            txtCiltNo.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
            txtIsbn.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            txtSayfaSayisi.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            txtStokAdet.Text = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
            cmbDil.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            cmbKategori.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            cmbKonum.Text = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
            cmbYayinevi.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            cmbYazar.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            cmbDil.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if(txtId.Text == "")
            {
                MessageBox.Show("Lütfen silmek istediğiniz kitabı seçiniz!");
                return;
            }

            try
            {
                bookManager.Delete(txtId.Text);
                listDataToTable();
                clearAllFields();
                MessageBox.Show("Başarıyla silindi...");
            }
            catch (Exception)
            {
                MessageBox.Show("Bir hata oluştu. Tekrar deneyin!");
                throw;
            }

        }
    }
}
