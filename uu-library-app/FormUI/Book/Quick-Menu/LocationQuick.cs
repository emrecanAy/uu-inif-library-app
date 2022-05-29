﻿using MySql.Data.MySqlClient;
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

namespace uu_library_app.FormUI.Book.Quick_Menu
{
    public partial class LocationQuick : Form
    {
        private Admin _admin;
        public LocationQuick(Admin admin)
        {
            InitializeComponent();
            _admin = admin;
        }

        MySqlConnection conn = new MySqlConnection(DbConnection.connectionString);
        LocationManager manager = new LocationManager(new LocationDal());
        LoggerManager logger = new LoggerManager(new LoggerDal());
        private void btnEkle_Click(object sender, EventArgs e)
        {
            string createGUID = System.Guid.NewGuid().ToString();
            if (txtKonum.Text == "")
            {
                MessageBox.Show("Lütfen geçerli bir değer giriniz!");
                return;
            }

            Location locationToAdd = new Location(createGUID, txtKonum.Text, cmbKategori.SelectedValue.ToString());
            try
            {
                manager.Add(locationToAdd);
                Logger log = new Logger(System.Guid.NewGuid().ToString(), _admin.id, "[ KonumId: " + locationToAdd.Id + " | " + locationToAdd.Shelf + " | KategoriId: " + locationToAdd.CategoryId + "" + "] eklendi! -Tarih: " + DateTime.Now);
                logger.Log(log);
            }
            catch (Exception)
            {
                MessageBox.Show("Eklerken bir hata oluştu. Lütfen tekrar deneyiniz...");
                throw;
            }
        }

        private void LocationQuick_Load(object sender, EventArgs e)
        {
            conn.Open();
            MySqlDataAdapter daCategories = new MySqlDataAdapter(SqlCommandHelper.getCategoriesCommand(conn));
            DataSet dsCategories = new DataSet();
            daCategories.Fill(dsCategories);
            SqlCommandHelper.getCategoriesCommand(conn).ExecuteNonQuery();
            conn.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}