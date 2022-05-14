using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql;
using MySql.Data.MySqlClient;

namespace uu_library_app
{
    public partial class Edit_Student : Form
    {
        public Edit_Student()
        {
            InitializeComponent();
        }
        MySqlConnection baglanti = new MySqlConnection();

        void listStudent()
        {
            baglanti.ConnectionString = "Server=172.21.54.3; uid=ASSEMSoft; pwd=Assemsoft1320..!;database=ASSEMSoft";
            DataTable table = new DataTable();
            MySqlDataAdapter komutListele = new MySqlDataAdapter("Select * from Student", baglanti);
            komutListele.Fill(table);
            dataGridView1.DataSource = table;
        }
        private void Edit_Student_Load(object sender, EventArgs e)
        {
            listStudent();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.ConnectionString = "Server=172.21.54.3; uid=ASSEMSoft; pwd=Assemsoft1320..!;database=ASSEMSoft";
            baglanti.Open();
            MySqlCommand komutGuncelle = new MySqlCommand("Update Student set firstName=@p1,LastName=@p2,eMail=@p3,number=@p4 where id=@p5", baglanti);
            komutGuncelle.Parameters.AddWithValue("@p1", txtAd.Text);
            komutGuncelle.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komutGuncelle.Parameters.AddWithValue("@p3", txtMail.Text);
            komutGuncelle.Parameters.AddWithValue("@p4", txtOkulNo.Text);
            komutGuncelle.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Öğrenci Güncelleme Başarılı !", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtMail.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtOkulNo.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
        }
    }
}
