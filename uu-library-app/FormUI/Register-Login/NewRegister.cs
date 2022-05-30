using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            loginTextBox3.ForeColor = Color.White;
            loginTextBox4.ForeColor = Color.White;
            loginTextBox1.ForeColor = Color.White;
            loginTextBox2.ForeColor = Color.White;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
