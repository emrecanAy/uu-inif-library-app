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
using uu_library_app.DataAccess.Concrete;

namespace uu_library_app.FormUI
{
    public partial class wLogin : Form
    {
        public wLogin()
        {
            InitializeComponent();
        }

        AdminManager adminManager = new AdminManager(new AdminDal());

        private void label3_Click(object sender, EventArgs e)
        {
            string crew = "Emrecan Ay @Team Lead (Fullstack Developer) ayemrecan.info@gmail.com \nŞenol Şen @UI Lead (UI Developer) \nMelike Yıldız @Contributor \nŞaban Dönmez @Contributor \nUmut Kozan @Contributor \nAriq Naufal @Contributor";
            DialogResult dialogResult = wehMessageBox.Show(crew,
                "AssemSoft!",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Asterisk);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (adminManager.checkIfEmailEqualsToPassword(txtEmail.Text, txtSifre.Text))
                {
                    this.Hide();
                    LibrarianInterface librarianInterface = new LibrarianInterface(adminManager.getbyEmail(txtEmail.Text));
                    librarianInterface.Show();
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Oh wait !");
                throw;
            }
        }
    }
}
