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
using uu_library_app.DataAccess.Concrete;

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
                MessageBox.Show("Oh wait !");
                throw;
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
    }
}
