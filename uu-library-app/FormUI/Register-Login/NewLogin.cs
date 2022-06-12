using MessageBoxDenemesi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uu_library_app.Business.Concrete;
using uu_library_app.Core.Utils;
using uu_library_app.DataAccess.Concrete;
using uu_library_app.Entity.Concrete;

namespace uu_library_app.FormUI.Register_Login
{
    public partial class NewLogin : Form
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
        public NewLogin()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));

        }

        AdminManager adminManager = new AdminManager(new AdminDal());
        private void NewLogin_Load(object sender, EventArgs e)
        {
            loginTextBox3.ForeColor = Color.White;
            loginTextBox4.ForeColor = Color.White;
 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (loginTextBox4.Text == "" || loginTextBox3.Text == "")
                {
                    wehMessageBox.Show("Tüm alanları doldurun!",
                   "Uyarı!",
                   MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    if (adminManager.checkIfEmailEqualsToPassword(loginTextBox4.Text, loginTextBox3.Text))
                    {
                        this.Hide();
                        LibrarianInterface librarianInterface = new LibrarianInterface(adminManager.getbyEmail(loginTextBox4.Text));
                        librarianInterface.Show();
                    }
                    else
                    {
                        wehMessageBox.Show("Eposta ve parola birbiriyle uyuşmuyor!",
                        "Uyarı!",
                        MessageBoxButtons.OK,
                         MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                wehMessageBox.Show("Lütfen internet bağlantınızı kontrol edin.\nSorun devam ediyorsa bir yetkiliyle iletişime geçin...",
                "Sunucuya bağlanırken bir hata oluştu!",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NewRegister newRegister = new NewRegister();
            newRegister.Show();
            this.Hide();
        }

        string code = EmailVerificator.GenerateCode();
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(loginTextBox4.Text != "")
            {
                wehMessageBox.Show("E-Posta adresinize gelen onaylama e-postasını kontrol ederek onay kodunu giriniz...",
              "ONAY",
              MessageBoxButtons.OK,
              MessageBoxIcon.Warning);
                adminManager.sendEmailVerificationCode(loginTextBox4.Text, code);
                panelOnay.Visible = true;
            }
            else
            {
                wehMessageBox.Show("Lütfen geçerli bir e-posta adresi girin...",
              "UYARI",
              MessageBoxButtons.OK,
              MessageBoxIcon.Warning);
            }
           
        }

        private void btnOnayKodu_Click(object sender, EventArgs e)
        {
            if (txtOnayKodu.Text == code)
            {
                wehMessageBox.Show("Başarılı! Lütfen yeni şifrenizi giriniz...",
                 "Yeni Şifre!",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Warning);
                System.Threading.Thread.Sleep(4000);
                panelOnay.Visible = false;
                panelYeniSifre.Visible = true;

            }
            else
            {
                wehMessageBox.Show("Onay kodu hatalı! Tekrar giriniz...", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnYeniSifre_Click(object sender, EventArgs e)
        {
            Admin admin = adminManager.getbyEmail(loginTextBox4.Text);
            if(loginTextBox1.Text != loginTextBox2.Text)
            {
                wehMessageBox.Show("Şifreler uyuşmuyor!",
                "UYARI!",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
                return;
            }
            admin.Password = StringEncoder.Encrypt(loginTextBox1.Text);
            try
            {
                adminManager.Update(admin);
                wehMessageBox.Show("Şifre başarıyla güncellendi! Giriş sayfasına yönlendiriliyorsunuz...",
                "Bilgi!",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
                this.Hide();
                NewLogin login = new NewLogin();
                login.Show();
            }
            catch (Exception ex)
            {
                wehMessageBox.Show("Şifre güncellenirken bir hata oluştu! Tekrar deneyiniz...",
                "Uyarı!",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            }
            

        }

        private void label4_Click(object sender, EventArgs e)
        {
            string crew = "Emrecan Ay @Team Lead (Fullstack Developer) ayemrecan.info@gmail.com \nŞenol Şen @UI Lead (UI Developer) \nMelike Yıldız @Contributor \nŞaban Dönmez @Contributor \nUmut Kozan @Contributor \nAriq Naufal @Contributor";
            DialogResult dialogResult = wehMessageBox.Show(crew,
                "AssemSoft!",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Asterisk);
        }
    }
}
