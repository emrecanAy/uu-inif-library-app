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
using uu_library_app.DataAccess.Concrete.EntityFramework;

namespace uu_library_app
{
    public partial class Borrowing_Book : Form
    {
        public Borrowing_Book()
        {
            InitializeComponent();
        }

        MySqlConnection conn = new MySqlConnection(DbConnection.connectionString);
        DepositBookManager depositBookManager = new DepositBookManager(new DepositBookDal());
        StudentManager studentManager = new StudentManager(new StudentDal());
        private void listDepositBookDataToTable()
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("Select * From DepositBook WHERE deleted=false", conn);
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

        DataTable dt = new DataTable();
        
        private void listStudentDataToTable()
        {
            MySqlDataAdapter da = new MySqlDataAdapter("Select * From Student WHERE deleted=false", conn);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[1].HeaderText = "";
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[2].HeaderText = "";
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].HeaderText = "";
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;

            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.DefaultCellStyle.Font = new Font("Nirmala UI", 13);

        }

        private void Borrowing_Book_Load(object sender, EventArgs e)
        {
         
          listStudentDataToTable();
          txtAra.Text = "Öğrenci numarasını giriniz...";
          txtAra.ForeColor = Color.Silver;

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            

        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            DataView dataView = new DataView();
            dataView = dt.DefaultView;
            dataView.RowFilter = "number like '"+txtAra.Text+"%'";
            dataGridView1.DataSource = dataView;
            dataGridView1.Visible = true;
        }

        private void txtAra_MouseEnter(object sender, EventArgs e)
        {
            if (txtAra.Text == "Öğrenci numarasını giriniz...")
            {
                txtAra.Text = "";
            }

        }

        private void txtAra_MouseLeave(object sender, EventArgs e)
        {
            if (txtAra.Text.Trim() == "")
            {
                txtAra.Text = "Öğrenci numarasını giriniz...";
                txtAra.ForeColor = Color.Silver;
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtOgrenciAdSoyad.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() +" "+ dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtBolum.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            txtOkulNo.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
        }
    }
}
