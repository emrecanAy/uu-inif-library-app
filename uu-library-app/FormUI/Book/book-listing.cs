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
    public partial class book_listing : Form
    {
        public book_listing()
        {
            InitializeComponent();
        }

        MySqlConnection conn = new MySqlConnection(DbConnection.connectionString);

        private void book_listing_Load(object sender, EventArgs e)
        {
            DataListerHelper.listInnerJoinAllBooksDataToTable(dataGridView1, conn);
           
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            DataView dataView = new DataView();
            dataView = dt.DefaultView;
            dataView.RowFilter = "bookName like '" + txtSearch.Text + "%'";
            dataGridView1.DataSource = dataView;
            dataGridView1.Visible = true;
        }
    }
}
