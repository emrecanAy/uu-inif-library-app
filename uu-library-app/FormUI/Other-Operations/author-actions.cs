﻿using MessageBoxDenemesi;
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

namespace uu_library_app
{
    public partial class author_actions : Form
    {
        private Admin _admin;

        MySqlConnection conn = new MySqlConnection(DbConnection.connectionString);
        LoggerManager logger = new LoggerManager(new LoggerDal());
        AuthorManager manager = new AuthorManager(new AuthorDal());

        public author_actions(Admin admin)
        {
            InitializeComponent();
            _admin = admin;
        }

        private void listDataToTable()
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("Select * From Author WHERE deleted=false", conn);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[1].HeaderText = "";
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.DefaultCellStyle.Font = new Font("Nirmala UI", 13);
        }
        private void clearAllFields()
        {
            txtId.Clear();
            txtAd.Clear();
            txtSoyad.Clear();
        }

        private void author_actions_Load(object sender, EventArgs e)
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
            try
            {
                conn.Open();
                listDataToTable();
                
            }
            catch (Exception)
            {
                MessageBox.Show("Server down!");
            }
            conn.Close();


        }

        private void btnSil_Click_1(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                MessageBox.Show("Silmek istediğiniz konumu seçin!");
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
                    Author authorToDelete = new Author(txtId.Text, txtAd.Text, txtSoyad.Text);
                    Logger log = new Logger(System.Guid.NewGuid().ToString(), _admin.id, "[ " + authorToDelete.Id + " " + authorToDelete.LastName + " ]" + _admin.FirstName + " " + _admin.LastName + " tarafından silindi! -Tarih: " + DateTime.Now);
                    manager.Delete(authorToDelete);
                    listDataToTable();
                    logger.Log(log);
                    clearAllFields();

                }
            }
            catch (Exception ex)
            {
                wehMessageBox.Show(ex.Message,"Hata!",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private void btnEkle_Click_1(object sender, EventArgs e)
        {
            string createGUID = System.Guid.NewGuid().ToString();

            if (txtAd.Text == "" || txtSoyad.Text == "" || txtAd.Text.Length < 2 || txtSoyad.Text.Length < 2)
            {
                wehMessageBox.Show("Lütfen geçerli ve en az 2 karakter içeren değerler giriniz","Hata!",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }

            Author authorToAdd = new Author(createGUID, txtAd.Text, txtSoyad.Text);
            try
            {
                manager.Add(authorToAdd);
                Logger log = new Logger(System.Guid.NewGuid().ToString(), _admin.id, "[ " + authorToAdd.Id + " | " + authorToAdd.FirstName + " " + authorToAdd.LastName + "" + "]" + _admin.FirstName + " " + _admin.LastName + " tarafından eklendi! -Tarih: " + DateTime.Now);
                logger.Log(log);
                listDataToTable();
                clearAllFields();
            }
            catch (Exception)
            {
                wehMessageBox.Show("Bir hata oluştu. Tekrar deneyiniz.", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                throw;
            }
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                txtId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtSoyad.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            }       
        }

        private void btnGuncelle_Click_1(object sender, EventArgs e)
        {
            if (txtAd.Text == "" || txtSoyad.Text == "" || txtAd.Text.Length < 2 || txtSoyad.Text.Length < 2)
            {
                wehMessageBox.Show("Lütfen geçerli ve en az 2 karakter içeren değerler giriniz", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Author authorToUpdate = new Author(txtId.Text, txtAd.Text, txtSoyad.Text);
            try
            {
                DialogResult dialogResult = wehMessageBox.Show("Güncellemek istediğinize emin misiniz?",
               "Uyarı!",
                 MessageBoxButtons.YesNo,
                 MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    manager.Update(authorToUpdate);
                    Logger log = new Logger(System.Guid.NewGuid().ToString(), _admin.id, "[ " + authorToUpdate.Id + " | " + authorToUpdate.FirstName + " " + authorToUpdate.LastName + "" + "]" + _admin.FirstName + " " + _admin.LastName + " tarafından güncellendi! -Tarih: " + DateTime.Now);
                    logger.Log(log);
                    listDataToTable();
                    clearAllFields();
                }


            }
            catch (Exception)
            {
                wehMessageBox.Show("Bir hata oluştu. Tekrar deneyiniz.", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                throw;
            }
        }

        private void wehTextBox2__TextChanged(object sender, EventArgs e)
        {
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter =
           string.Format("firstName LIKE '{0}%' OR firstName LIKE '% {0}%'", wehTextBox2.Texts);
        }
    }
}
