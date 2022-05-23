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
            DataListerHelper.listInnerJoinSomeBookDataToTable(dataGridView1, conn);
            conn.Close();
            #endregion
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtCevirmen.Text = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
            txtCiltNo.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString(); //ciltNo eklenecek
            txtIsbn.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            txtSayfaSayisi.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            txtStokAdet.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
            cmbDil.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            cmbKategori.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            cmbKonum.Text = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
            cmbYayinevi.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            cmbYazar.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            cmbDil.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
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
                DataListerHelper.listInnerJoinSomeBookDataToTable(dataGridView1, conn);
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
