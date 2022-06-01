using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MessageBoxDenemesi;
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
        Admin _admin;
        public Edit_Student(Admin admin)
        {
            InitializeComponent();
            _admin = admin;
        }

        MySqlConnection conn = new MySqlConnection(DbConnection.connectionString);
        LoggerManager logger = new LoggerManager(new LoggerDal());
        PersonnelManager manager = new PersonnelManager(new PersonnelDal());

        private void clearAllFields()
        {
            txtId.Clear();
            txtAd.Clear();
            txtEmail.Clear();
            txtSoyad.Clear();
            cmbDepartman.ResetText();
        }
     
        private void Edit_Student_Load(object sender, EventArgs e)
        {
            conn.Open();
            DataListerToTableHelper.listInnerJoinAllPersonnelsNotConcatDataToTable(dataGridView1, conn);
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            MySqlCommand commandToGetAll = new MySqlCommand("SELECT * FROM Department WHERE deleted=false", conn);
            MySqlCommand allFaculty = new MySqlCommand("SELECT * FROM Faculty WHERE deleted=false", conn);
            MySqlDataAdapter da = new MySqlDataAdapter(commandToGetAll);
            MySqlDataAdapter daFaculty = new MySqlDataAdapter(allFaculty);
            DataSet ds = new DataSet();
            DataSet dsFaculty = new DataSet();
            da.Fill(ds);
            daFaculty.Fill(dsFaculty);
            commandToGetAll.ExecuteNonQuery();
            allFaculty.ExecuteNonQuery();
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

            cmbDepartman.DataSource = ds.Tables[0];
            cmbDepartman.DisplayMember = "name";
            cmbDepartman.ValueMember = "id";

            cmbFakulte.DataSource = dsFaculty.Tables[0];
            cmbFakulte.DisplayMember = "name";
            cmbFakulte.ValueMember = "id";
        }

    

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtEmail.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            cmbDepartman.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            cmbFakulte.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            
        }

        private void wehTextBox1__TextChanged(object sender, EventArgs e)
        {
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter =
            string.Format("number LIKE '{0}%' OR number LIKE '% {0}%'", wehTextBox1.Texts);
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            string createGUID = System.Guid.NewGuid().ToString();

            if (txtAd.Text == "" || txtSoyad.Text == "" || txtEmail.Text == "" || cmbDepartman.Text == "")
            {
                wehMessageBox.Show("Lütfen geçerli değerler giriniz!","Hata!",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Personnel personnel = new Personnel(createGUID, cmbFakulte.SelectedValue.ToString(), cmbDepartman.SelectedValue.ToString(), txtAd.Text, txtSoyad.Text, txtEmail.Text);
                manager.Add(personnel);
                Logger log = new Logger(System.Guid.NewGuid().ToString(), _admin.id, "[ " + personnel.Id + " | " + personnel.Email + " ] " + _admin.FirstName + " " + _admin.LastName + " tarafından eklendi! -Tarih: " + DateTime.Now);
                logger.Log(log);
                clearAllFields();
                DataListerToTableHelper.listInnerJoinAllPersonnelsNotConcatDataToTable(dataGridView1, conn);
            }
            catch (Exception)
            {
                wehMessageBox.Show("Bir hata oluştu. Tekrar deneyiniz!","Hata!",MessageBoxButtons.OK,MessageBoxIcon.Warning);
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
                    Personnel personnel = new Personnel(txtId.Text, cmbFakulte.SelectedValue.ToString(), cmbDepartman.SelectedValue.ToString(), txtAd.Text, txtSoyad.Text, txtEmail.Text);
                    manager.Delete(personnel);
                    Logger log = new Logger(System.Guid.NewGuid().ToString(), _admin.id, "[ " + personnel.Id + " | " + personnel.Email + " ] " + _admin.FirstName + " " + _admin.LastName + " tarafından silindi! -Tarih: " + DateTime.Now);
                    logger.Log(log);
                    clearAllFields();
                    DataListerToTableHelper.listInnerJoinAllPersonnelsNotConcatDataToTable(dataGridView1, conn);
                }
            }
            catch (Exception)
            {
                wehMessageBox.Show("Bir hata oluştu. Tekrar deneyiniz!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                throw;
            }
        }

        private void btnGuncelle_Click_1(object sender, EventArgs e)
        {
            if (txtAd.Text == "" || txtSoyad.Text == "" || txtEmail.Text == "")
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
                    Personnel personnel = new Personnel(txtId.Text, cmbFakulte.SelectedValue.ToString(), cmbDepartman.SelectedValue.ToString(), txtAd.Text, txtSoyad.Text, txtEmail.Text);
                    manager.Update(personnel);
                    Logger log = new Logger(System.Guid.NewGuid().ToString(), _admin.id, "[ " + personnel.Id + " | " + personnel.Email + " ] " + _admin.FirstName + " " + _admin.LastName + " tarafından güncellendi! -Tarih: " + DateTime.Now);
                    logger.Log(log);
                    clearAllFields();
                    DataListerToTableHelper.listInnerJoinAllPersonnelsNotConcatDataToTable(dataGridView1, conn);

                }
            }
            catch (Exception)
            {
                wehMessageBox.Show("Bir hata oluştu. Tekrar deneyiniz!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                throw;
            }
        }
    }
    }

