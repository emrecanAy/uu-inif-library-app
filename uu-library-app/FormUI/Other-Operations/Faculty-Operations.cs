﻿using MessageBoxDenemesi;
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
using uu_library_app.Entity.Concrete;

namespace uu_library_app.FormUI.Other_Operations
{
    public partial class Faculty_Operations : Form
    {
        private Admin _admin;
        public Faculty_Operations(Admin admin)
        {
            InitializeComponent();
            _admin = admin;
        }

        MySqlConnection conn = new MySqlConnection(DbConnection.connectionString);
        LoggerManager logger = new LoggerManager(new LoggerDal());
        FacultyManager manager = new FacultyManager(new FacultyDal());

        private void btnEkle_Click(object sender, EventArgs e)
        {
            string createGUID = System.Guid.NewGuid().ToString();
            if (txtAd.Text == "" || txtAd.Text.Length < 3)
            {
                MessageBox.Show("Lütfen en az üç harf içeren geçerli bir değer giriniz!");
                return;
            }
            Faculty facultyToAdd = new Faculty(createGUID, txtAd.Text);

            try
            {
                manager.Add(facultyToAdd);
                Logger log = new Logger(System.Guid.NewGuid().ToString(), _admin.id, "[ " + facultyToAdd.Id + " | " + facultyToAdd.Name + "]" + _admin.FirstName + " " + _admin.LastName + " tarafından eklendi! -Tarih: " + DateTime.Now);
                logger.Log(log);
                listDataToTable();
            }
            catch (Exception)
            {
                MessageBox.Show("Eklerken bir hata oluştu. Lütfen tekrar deneyiniz...");
                throw;
            }
        }

        private void Faculty_Operations_Load(object sender, EventArgs e)
        {
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
            
            listDataToTable();
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
                DialogResult dialogResult = wehMessageBox.Show("Silmek istediğinize emin misiniz? Bu işlem bu fakülteye ait olan bütün öğrencileri de silecektir!",
               "Uyarı!",
                 MessageBoxButtons.YesNo,
                 MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    Faculty faculty = new Faculty(txtId.Text, txtAd.Text);
                    manager.Delete(faculty);
                    Logger log = new Logger(System.Guid.NewGuid().ToString(), _admin.id, "[ " + faculty.Id + " | " + faculty.Name + "]" + _admin.FirstName + " " + _admin.LastName + " tarafından silindi! -Tarih: " + DateTime.Now);
                    logger.Log(log);
                    listDataToTable();
                    clearAllFields();
                }
                
            }
            catch (Exception)
            {
                MessageBox.Show("Bir hata oluştu. Lütfen tekrar deneyiniz...");
                throw;
            }
        }

        private void listDataToTable()
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("Select * From Faculty WHERE deleted=false", conn);
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

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            Faculty faculty = new Faculty(txtId.Text, txtAd.Text);

            try
            {
                if (txtAd.Text == "")
                {
                    MessageBox.Show("Geçerli bir değer giriniz!");
                    return;
                }

                DialogResult dialogResult = wehMessageBox.Show("Güncellemek istediğinize emin misiniz?",
               "Uyarı!",
                 MessageBoxButtons.YesNo,
                 MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    manager.Update(faculty);
                    Logger log = new Logger(System.Guid.NewGuid().ToString(), _admin.id, "[ " + faculty.Id + " | " + faculty.Name + "]" + _admin.FirstName + " " + _admin.LastName + " tarafından güncellendi! -Tarih: " + DateTime.Now);
                    logger.Log(log);
                    listDataToTable();
                    clearAllFields();
                }

                
            }
            catch (Exception)
            {
                MessageBox.Show("Bir hata oluştu. Lütfen tekrar deneyiniz...");
                throw;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
        }
    }
}
