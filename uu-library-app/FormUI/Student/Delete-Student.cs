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
using uu_library_app.DataAccess.Concrete;

namespace uu_library_app
{
    public partial class Delete_Student : Form
    {
        public Delete_Student()
        {
            InitializeComponent();
        }

        MySqlConnection conn = new MySqlConnection("Server=172.21.54.3;uid=ASSEMSoft;pwd=Assemsoft1320..!;database=ASSEMSoft");
        StudentManager manager = new StudentManager(new StudentDal());

        private void listDataToTable()
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM Student WHERE deleted=false", conn);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Ad";
            dataGridView1.Columns[2].HeaderText = "Soyad";
            dataGridView1.Columns[3].HeaderText = "Okul No";
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].HeaderText = "Eposta";
            dataGridView1.Columns[6].HeaderText = "Bölüm";
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.DefaultCellStyle.Font = new Font("Nirmala UI", 13);
        }

        private void Delete_Student_Load(object sender, EventArgs e)
        {
            conn.Open();
            listDataToTable();
            conn.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtOkulNo.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtMail.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtBolum.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(txtId.Text == "")
            {
                MessageBox.Show("Silmek istediğiniz öğrenciyi seçiniz!");
                return;
            }

            try
            {
                manager.Delete(txtId.Text);
                listDataToTable();
                MessageBox.Show("Başarılı bir şekilde silindi.");
            }
            catch (Exception)
            {
                MessageBox.Show("Bir hata oluştu. Tekrar deneyiniz!");
                throw;
            }
           
        }
    }
}
