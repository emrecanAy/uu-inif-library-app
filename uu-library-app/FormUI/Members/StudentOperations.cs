using MessageBoxDenemesi;
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
using uu_library_app.FormUI.MailSettings;

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

        Admin _admin;
        public Add_Student(Admin admin)
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
            _admin = admin;
        }

        MySqlConnection conn = new MySqlConnection(DbConnection.connectionString);
        LoggerManager logger = new LoggerManager(new LoggerDal());
        StudentManager manager = new StudentManager(new StudentDal());

        private void clearAllFields()
        {
            txtId.Clear();
            txtAd.Clear();
            txtOkulNo.Clear();
            txtEmail.Clear();
            txtSoyad.Clear();
            comboBox1.ResetText();
            cmbFakulte.ResetText();
            comboBox1.SelectedIndex = -1;
            cmbFakulte.SelectedIndex = -1;
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            string createGUID = System.Guid.NewGuid().ToString();

            if (txtAd.Text == "" || txtSoyad.Text == "" || txtOkulNo.Text == "" || txtEmail.Text == "" || comboBox1.Text == "")
            {
                wehMessageBox.Show("Lütfen geçerli değerler giriniz!","Hata!",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Student studentToAdd = new Student(createGUID, comboBox1.SelectedValue.ToString(), cmbFakulte.SelectedValue.ToString(), txtAd.Text, txtSoyad.Text, txtOkulNo.Text, "CARD-ID", txtEmail.Text);
                manager.Add(studentToAdd);
                Logger log = new Logger(System.Guid.NewGuid().ToString(), _admin.id, "[ " + studentToAdd.Id + " | " + studentToAdd.Number + " ] " + _admin.FirstName + " " + _admin.LastName + " tarafından eklendi! -Tarih: " + DateTime.Now);
                logger.Log(log);
                clearAllFields();
                DataListerToTableHelper.listInnerJoinAllStudentsNotConcatDataToTable(dataGridView1, conn);
            }
            catch (Exception)
            {
                wehMessageBox.Show("Bir hata oluştu. Tekrar deneyiniz!","Hata!",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                throw;
            }
            
        }

        private void Add_Student_Load_1(object sender, EventArgs e)
        {

            conn.Open();
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            DataListerToTableHelper.listInnerJoinAllStudentsNotConcatDataToTable(dataGridView1, conn);
            MySqlCommand commandToGetAll = new MySqlCommand("SELECT * FROM Department WHERE deleted=false", conn);
            MySqlCommand commandToFaculties = new MySqlCommand("SELECT * FROM Faculty WHERE deleted=false", conn);
            MySqlDataAdapter da = new MySqlDataAdapter(commandToGetAll);
            MySqlDataAdapter daFaculty = new MySqlDataAdapter(commandToFaculties);
            DataSet ds = new DataSet();
            DataSet dsFaculty = new DataSet();
            da.Fill(ds);
            daFaculty.Fill(dsFaculty);
            commandToGetAll.ExecuteNonQuery();
            commandToFaculties.ExecuteNonQuery();
            conn.Close();

            comboBox1.DataSource = ds.Tables[0];
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "id";

            cmbFakulte.DataSource = dsFaculty.Tables[0];
            cmbFakulte.DisplayMember = "name";
            cmbFakulte.ValueMember = "id";

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

            ToolTip Aciklama = new ToolTip();
            Aciklama.ToolTipTitle = "Kitap Adı Giriniz!";
            Aciklama.ToolTipIcon = ToolTipIcon.Info;
            Aciklama.IsBalloon = true;
            Aciklama.SetToolTip(wehTextBox1, "    ");
        }
   

        private void wehTextBox1__TextChanged(object sender, EventArgs e)
        {
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter =
              string.Format("number LIKE '{0}%' OR number LIKE '% {0}%'", wehTextBox1.Texts);
        }

        private void btnHızlıFakulte_Click(object sender, EventArgs e)
        {
            FacultyQuick quick = new FacultyQuick(_admin);
            quick.Show();
        }

        private void btnHızlıBolum_Click(object sender, EventArgs e)
        {
            DepartmentQuick quick = new DepartmentQuick(_admin);
            quick.Show();
        }

        private void txtOkulNo_Click(object sender, EventArgs e)
        {
            this.txtOkulNo.Select(0, 0);
        }

        private void txtOkulNo_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                txtId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtSoyad.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtOkulNo.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtEmail.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                cmbFakulte.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            }   
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (txtAd.Text == "" || txtSoyad.Text == "" || txtEmail.Text == "" || txtOkulNo.Text == "" || comboBox1.Text == "")
            {
                wehMessageBox.Show("Lütfen geçerli değerler giriniz!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DialogResult dialogResult = wehMessageBox.Show("Güncellemek istediğinize emin misiniz?",
                "Uyarı!",
                  MessageBoxButtons.YesNo,
                  MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    Student studentToUpdate = new Student(txtId.Text, comboBox1.SelectedValue.ToString(), cmbFakulte.SelectedValue.ToString(), txtAd.Text, txtSoyad.Text, txtOkulNo.Text, "CARD-ID", txtEmail.Text);
                    manager.Update(studentToUpdate);
                    Logger log = new Logger(System.Guid.NewGuid().ToString(), _admin.id, "[ " + studentToUpdate.Id + " | " + studentToUpdate.Number + " ] " + _admin.FirstName + " " + _admin.LastName + " tarafından güncellendi! -Tarih: " + DateTime.Now);
                    logger.Log(log);
                    clearAllFields();
                    DataListerToTableHelper.listInnerJoinAllStudentsNotConcatDataToTable(dataGridView1, conn);

                }
            }
            catch (Exception)
            {
                wehMessageBox.Show("Bir hata oluştu. Tekrar deneyiniz!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                throw;
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                wehMessageBox.Show("Silmek istediğiniz öğrenciyi seçiniz!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DialogResult dialogResult = wehMessageBox.Show("Silmek istediğinize emin misiniz?",
                "Uyarı!",
                  MessageBoxButtons.YesNo,
                  MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    Student student = new Student(txtId.Text, comboBox1.Text, cmbFakulte.Text, txtAd.Text, txtSoyad.Text, txtOkulNo.Text, "CARD-ID", txtEmail.Text);
                    manager.Delete(student);
                    Logger log = new Logger(System.Guid.NewGuid().ToString(), _admin.id, "[ " + student.Id + " | " + student.Number + " ] " + _admin.FirstName + " " + _admin.LastName + " tarafından silindi! -Tarih: " + DateTime.Now);
                    logger.Log(log);
                    clearAllFields();
                    DataListerToTableHelper.listInnerJoinAllStudentsNotConcatDataToTable(dataGridView1, conn);
                }
            }
            catch (Exception ex)
            {
                wehMessageBox.Show(ex.Message, "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtOkulNo_TextChanged_1(object sender, EventArgs e)
        {
            txtEmail.Text = txtOkulNo.Text + "@ogr.uludag.edu.tr";
        }
    }
}