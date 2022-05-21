using MySql.Data.MySqlClient;
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
using uu_library_app.Core.Helpers;
using uu_library_app.DataAccess.Concrete;
using uu_library_app.Entity.Concrete;

namespace uu_library_app
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        MySqlConnection conn = new MySqlConnection(DbConnection.connectionString);
        StudentManager studentManager = new StudentManager(new StudentDal());
        AuthorManager authorManager = new AuthorManager(new AuthorDal());
        CategoryManager categoryManager = new CategoryManager(new CategoryDal());

        private void panelChildForm_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            int studentsCount = studentManager.getAll().Count();
            int authorsCount = authorManager.getAll().Count();
            int categoriesCount = categoryManager.getAll().Count();

            lblToplamUye.Text = studentsCount.ToString();
            lblToplamYazar.Text = authorsCount.ToString();
            lblToplamKategori.Text = categoriesCount.ToString();
        }
    }
}
