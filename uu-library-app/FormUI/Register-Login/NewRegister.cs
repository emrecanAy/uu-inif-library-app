using MessageBoxDenemesi;
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

namespace uu_library_app.FormUI.Register_Login
{
    public partial class NewRegister : Form
    {
        public NewRegister()
        {
            InitializeComponent();
        }

        private void NewRegister_Load(object sender, EventArgs e)
        {
            txtSifre.ForeColor = Color.White;
            txtEmail.ForeColor = Color.White;
            txtAd.ForeColor = Color.White;
            txtSoyad.ForeColor = Color.White;
            txtSifreTekrar.ForeColor = Color.White;
        }

        AdminManager adminManager = new AdminManager(new AdminDal());
        Admin admin;
        string code = EmailVerificator.GenerateCode();
        private void button2_Click(object sender, EventArgs e)
        {
            string createGUID = System.Guid.NewGuid().ToString();
            admin = new Admin(createGUID, txtAd.Text, txtSoyad.Text, txtEmail.Text, StringEncoder.Encrypt(txtSifre.Text));
            if (txtSifre.Text != txtSifreTekrar.Text)
            {
                MessageBox.Show("Parolalar uyuşmuyor!");
                return;
            }
            if (txtAd.Text == "" || txtSoyad.Text == "" || txtEmail.Text == "" || txtSifre.Text == "")
            {
                MessageBox.Show("Tüm alanları doldurun!");
                return;
            }
            adminManager.sendEmailVerificationCode(txtEmail.Text, code);
            panelOnay.Visible = true;
            button2.Enabled = false;
            txtAd.Enabled = false;
            txtSoyad.Enabled = false;
            txtEmail.Enabled = false;
            txtSifre.Enabled = false;
            txtSifreTekrar.Enabled = false;
        }

        private void btnOnayKodu_Click(object sender, EventArgs e)
        {
            
            if (txtOnayKodu.Text == code)
            {
                adminManager.Add(admin, txtOnayKodu.Text);
                wehMessageBox.Show("Başarıyla kaydoldunuz! Giriş sayfasına yönlendiriliyorsunuz...",
                 "Başarılı!",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Warning);
                System.Threading.Thread.Sleep(4000);
                this.Hide();
                NewLogin login = new NewLogin();
                login.Show();

            }
            else
            {
                MessageBox.Show("Onay kodu hatalı! Tekrar giriniz...");
            }
        }
    }
}
