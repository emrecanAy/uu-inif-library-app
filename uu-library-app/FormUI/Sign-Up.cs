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
using uu_library_app.DataAccess.Concrete;
using uu_library_app.Entity.Concrete;

namespace uu_library_app
{
    public partial class Sign_Up : Form
    {
        public Sign_Up()
        {
            InitializeComponent();
        }

        AdminManager adminManager = new AdminManager(new AdminDal());
        private void btnEkle_Click(object sender, EventArgs e)
        {
            string createGUID = System.Guid.NewGuid().ToString();
            Admin adminToAdd = new Admin(createGUID, txtAd.Text, txtSoyad.Text, txtEmail.Text, txtSifre.Text);

            if(txtSifre.Text != txtSifreTekrar.Text)
            {
                MessageBox.Show("Parolalar uyuşmuyor!");
                return;
            }
            if(txtAd.Text == "" || txtSoyad.Text == "" || txtEmail.Text == "" || txtSifre.Text == "")
            {
                MessageBox.Show("Tüm alanları doldurun!");
                return;
            }

            try
            {
                adminManager.Add(adminToAdd);
                MessageBox.Show("Başarılı bir şekilde eklendi!");
            }
            catch (Exception)
            {
                MessageBox.Show("HATA!");
                throw;
            }

        }
    }
}
