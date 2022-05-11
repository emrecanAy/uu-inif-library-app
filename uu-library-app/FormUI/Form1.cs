using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using uu_library_app.DataAccess.Concrete.EntityFramework;
using uu_library_app.DataAccess.Abstract;
using uu_library_app.Entity.Concrete;

namespace uu_library_app
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DepartmentDal departmentDal = new DepartmentDal();
            Department departmentToAdd = new Department();
            departmentToAdd.Id = "djfkgsodpf56sdf5sdf";
            departmentToAdd.Name = "YBS";
            departmentDal.Add(departmentToAdd);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
