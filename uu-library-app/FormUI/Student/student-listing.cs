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
using uu_library_app.Core.Helpers;

namespace uu_library_app
{
    public partial class student_listing : Form
    {
        public student_listing()
        {
            InitializeComponent();
        }

        MySqlConnection conn = new MySqlConnection(DbConnection.connectionString);
        private void student_listing_Load(object sender, EventArgs e)
        {
            DataListerHelper.listInnerJoinAllStudentsDataToTable(dataGridView1, conn);
        }
    }
}
