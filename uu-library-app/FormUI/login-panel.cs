using MySql.Data.MySqlClient;
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
using uu_library_app.Core.Helpers;
using uu_library_app.DataAccess.Concrete;
using uu_library_app.FormUI;

namespace uu_library_app
{
    public partial class login_panel : Form
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
        public login_panel()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
        }
        MySqlConnection conn = new MySqlConnection(DbConnection.connectionString);
        AdminManager adminManager = new AdminManager(new AdminDal());



        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Sign_Up sign_Up = new Sign_Up();
            sign_Up.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (adminManager.checkIfEmailEqualsToPassword(txtEmail.Text, txtPassword.Text))
                {
                    this.Hide();
                    LibrarianInterface openApp = new LibrarianInterface(adminManager.getbyEmail(txtEmail.Text));
                    openApp.Show();
                    MessageBox.Show("Giriş Başarılı");
                }
                else { MessageBox.Show("Hatalı Kullanıcı Adı veya Şifre !"); }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Oh wait !");
                throw;
            }    
            
        }

        private void login_panel_Load(object sender, EventArgs e)
        {

        }
    }
}
