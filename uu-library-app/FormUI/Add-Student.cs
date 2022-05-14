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
using uu_library_app.Entity.Concrete;

namespace uu_library_app.FormUI
{
    public partial class Add_Student : Form
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
        public Add_Student()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
        }

<<<<<<< HEAD
        
=======
        private void Add_Student_Load(object sender, EventArgs e)
        {

        }

        IStudentService _service;

        public Add_Student(IStudentService service)
        {
            _service = service;
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            Student studentToAdd = new Student("hmjbjhbkmnö","jkdsflsdf" , txtAd.Text, txtSoyad.Text, txtOkulNo.Text, txtEmail.Text, "sadfasd", DateTime.Now);
            _service.Add(studentToAdd);
        }
>>>>>>> 4bdb76d9eef3ab9779f7ef3b0d4cbc19a1e6f493
    }
}
