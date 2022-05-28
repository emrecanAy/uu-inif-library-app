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
using uu_library_app.Core.Utils;
using uu_library_app.DataAccess.Concrete;
using uu_library_app.Entity.Concrete;
using uu_library_app.FormUI;

namespace uu_library_app
{
    public partial class Sign_Up : Form
    {
        public Sign_Up()
        {
            InitializeComponent();
        }

        AdminManager adminManager = new AdminManager(new AdminDal());
        Admin admin;
        string code = EmailVerificator.GenerateCode();
        private void btnEkle_Click(object sender, EventArgs e)
        {

            string createGUID = System.Guid.NewGuid().ToString();
            admin = new Admin(createGUID, txtAd.Text, txtSoyad.Text, txtEmail.Text, StringEncoder.Encrypt(txtSifre.Text));
            if (txtSifre.Text != txtSifreTekrar.Text)
            {
                MessageBox.Show("Parolalar uyuşmuyor!");
                return;
            }
            if(txtAd.Text == "" || txtSoyad.Text == "" || txtEmail.Text == "" || txtSifre.Text == "")
            {
                MessageBox.Show("Tüm alanları doldurun!");
                return;
            }
            
            adminManager.sendEmailVerificationCode(txtEmail.Text, code);
            panelOnayMail.Visible = true;
            

        }

        private void Sign_Up_Load(object sender, EventArgs e)
        {

        }

        private void btnOnay_Click(object sender, EventArgs e)
        {
            if(txtOnayKodu.Text == code)
            {
                adminManager.Add(admin, txtOnayKodu.Text);
            }
            else
            {
                MessageBox.Show("Onay kodu hatalı! Tekrar giriniz...");
            }
            
        }
    }
}
