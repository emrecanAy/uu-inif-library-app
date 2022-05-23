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

namespace uu_library_app.FormUI
{
    public partial class Add_Student : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
            );
        public Add_Student()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
        }

        MySqlConnection conn = new MySqlConnection(DbConnection.connectionString);
        StudentManager manager = new StudentManager(new StudentDal());


        private void Add_Student_Load(object sender, EventArgs e)
        {

        }

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

        private void clearAllFields()
        {
            txtId.Clear();
            txtAd.Clear();
            txtEmail.Clear();
            txtOkulNo.Clear();
            txtSoyad.Clear();
            comboBox1.ResetText();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            string createGUID = System.Guid.NewGuid().ToString();

            if (txtAd.Text == "" || txtSoyad.Text == "" || txtEmail.Text == "" || txtOkulNo.Text == "" || comboBox1.Text == "")
            {
                MessageBox.Show("Lütfen geçerli değerler giriniz!");
                return;
            }

            try
            {
                Student studentToAdd = new Student(createGUID, comboBox1.SelectedValue.ToString(), txtAd.Text, txtSoyad.Text, txtOkulNo.Text, "CARD-ID", txtEmail.Text);
                manager.Add(studentToAdd);
                clearAllFields();
                listDataToTable();
            }
            catch (Exception)
            {
                MessageBox.Show("Bir hata oluştu. Tekrar deneyiniz!");
                throw;
            }
            
        }

        private void Add_Student_Load_1(object sender, EventArgs e)
        {
            conn.Open();
            listDataToTable();
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            DataListerHelper.listInnerJoinAllStudentsDataToTable(dataGridView1, conn);
            MySqlCommand commandToGetAll = new MySqlCommand("SELECT * FROM Department WHERE deleted=false", conn);
            MySqlDataAdapter da = new MySqlDataAdapter(commandToGetAll);
            DataSet ds = new DataSet();
            da.Fill(ds);
            commandToGetAll.ExecuteNonQuery();
            conn.Close();

            comboBox1.DataSource = ds.Tables[0];
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "id";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter =
            string.Format("number LIKE '{0}%' OR number LIKE '% {0}%'", txtAra.Text);
        }
    }
}