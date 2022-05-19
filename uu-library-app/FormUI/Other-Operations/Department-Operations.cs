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
using uu_library_app.DataAccess.Concrete.EntityFramework;
using uu_library_app.Entity.Concrete;

namespace uu_library_app
{
    public partial class Department_Operations : Form
    {
        public Department_Operations()
        {
            InitializeComponent();
        }

        MySqlConnection conn = new MySqlConnection("Server=172.21.54.3;uid=ASSEMSoft;pwd=Assemsoft1320..!;database=ASSEMSoft");
        DepartmentManager manager = new DepartmentManager(new DepartmentDal());

        private void listDataToTable()
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("Select * From Department WHERE deleted=false", conn);
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
       

        private void btnEkle_Click(object sender, EventArgs e)
        {
            string createGUID = System.Guid.NewGuid().ToString();
            if(txtAd.Text == "" || txtAd.Text.Length < 3)
            {
                MessageBox.Show("Lütfen en az üç harf içeren geçerli bir değer giriniz!");
                return;
            }
            Department departmentToAdd = new Department(createGUID, txtAd.Text);

            try
            {
                manager.Add(departmentToAdd);
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
            if (txtId.Text == "")
            {
                MessageBox.Show("Silmek istediğiniz bölümü seçin!");
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
                MessageBox.Show("Bir hata oluştu. Lütfen tekrar deneyiniz...");
                throw;
            }
            
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            Department departmentToUpdate = new Department(txtId.Text, txtAd.Text); 
            
            try
            {
                if(txtAd.Text == "")
                {
                    MessageBox.Show("Geçerli bir değer giriniz!");
                    return;
                }
                manager.Update(departmentToUpdate);
                listDataToTable();
                clearAllFields();
            }
            catch (Exception)
            {
                MessageBox.Show("Bir hata oluştu. Lütfen tekrar deneyiniz...");
                throw;
            }
        }

        private void Department_Operations_Load(object sender, EventArgs e)
        {
            conn.Open();
            listDataToTable();
            conn.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
        }
    }
}
