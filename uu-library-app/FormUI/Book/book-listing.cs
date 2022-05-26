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
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(46, 51, 73);
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(46, 51, 73);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9.0F, FontStyle.Bold);
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.ScrollBars = ScrollBars.None;

        }
        private void wehTextBox1__TextChanged(object sender, EventArgs e)
        {
            string value = cmbAranacakAlan.SelectedValue.ToString();
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter =
            string.Format("" + value + " LIKE '{0}%' OR " + value + " LIKE '% {0}%'", wehTextBox1.Texts);
        }
    }
}
