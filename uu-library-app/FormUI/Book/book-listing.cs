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
            //dataGridView1.
            DataListerToTableHelper.listInnerJoinAllBooksDataToTable(dataGridView1, conn);
            Dictionary<string, string> comboSource = new Dictionary<string, string>();
            comboSource.Add("bookName", "Kitap");
            comboSource.Add("authorFullName", "Yazar");
            comboSource.Add("categoryName", "Kategori");
            comboSource.Add("language", "Dil");
            comboSource.Add("publisherName", "Yayınevi");
            comboSource.Add("isbnNumber", "ISBN");
            comboSource.Add("shelf", "Konum");
            comboSource.Add("interpreterName", "Çevirmen");
            cmbAranacakAlan.DataSource = new BindingSource(comboSource, null);
            cmbAranacakAlan.DisplayMember = "Value";
            cmbAranacakAlan.ValueMember = "Key";

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string value = cmbAranacakAlan.SelectedValue.ToString();
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter =
            string.Format(""+value+ " LIKE '{0}%' OR " + value + " LIKE '% {0}%'", txtAra.Text);

        }
    }
}
