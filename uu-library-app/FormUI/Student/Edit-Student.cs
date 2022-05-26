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
using uu_library_app.Business.Concrete;
using uu_library_app.Core.Helpers;
using uu_library_app.DataAccess.Concrete;
using uu_library_app.Entity.Concrete;

namespace uu_library_app
{
    public partial class Edit_Student : Form
    {
        public Edit_Student()
        {
            InitializeComponent();
        }

        MySqlConnection conn = new MySqlConnection(DbConnection.connectionString);
        StudentManager manager = new StudentManager(new StudentDal());

        private void clearAllFields()
        {
            txtId.Clear();
            txtAd.Clear();
            txtEmail.Clear();
            txtOkulNo.Clear();
            txtSoyad.Clear();
            comboBox1.ResetText();
        }
     
        private void Edit_Student_Load(object sender, EventArgs e)
        {
            conn.Open();
            DataListerToTableHelper.listInnerJoinAllStudentsNotConcatDataToTable(dataGridView1, conn);
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            MySqlCommand commandToGetAll = new MySqlCommand("SELECT * FROM Department WHERE deleted=false", conn);
            MySqlDataAdapter da = new MySqlDataAdapter(commandToGetAll);
            DataSet ds = new DataSet();
            da.Fill(ds);
            commandToGetAll.ExecuteNonQuery();
            conn.Close();
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
            dataGridView1.DefaultCellStyle.ForeColor = Color.White;
            comboBox1.DataSource = ds.Tables[0];
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "id";
        }

       

       

        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter =
            string.Format("number LIKE '{0}%' OR number LIKE '% {0}%'", txtAra.Text);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtOkulNo.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtEmail.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (txtAd.Text == "" || txtSoyad.Text == "" || txtEmail.Text == "" || txtOkulNo.Text == "" || comboBox1.Text == "")
            {
                MessageBox.Show("Lütfen geçerli değerler giriniz!");
                return;
            }

            try
            {
                Student studentToUpdate = new Student(txtId.Text, comboBox1.SelectedValue.ToString(), txtAd.Text, txtSoyad.Text, txtOkulNo.Text, "CARD-ID", txtEmail.Text);
                manager.Update(studentToUpdate);
                MessageBox.Show("Başarıyla güncellendi!");
                clearAllFields();
                DataListerToTableHelper.listInnerJoinAllStudentsNotConcatDataToTable(dataGridView1, conn);
            }
            catch (Exception)
            {
                MessageBox.Show("Bir hata oluştu. Tekrar deneyiniz!");
                throw;
            }
        }
    }
    }

