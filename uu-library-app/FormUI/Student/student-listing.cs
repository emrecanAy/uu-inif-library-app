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
            DataListerToTableHelper.listInnerJoinAllStudentsDataToTable(dataGridView1, conn);
            Dictionary<string, string> comboSource = new Dictionary<string, string>();
            comboSource.Add("studentFullName", "Öğrenci Adı");
            comboSource.Add("number", "Okul No");
            comboSource.Add("eMail", "Eposta");
            comboSource.Add("name", "Bölüm");
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
        }

        private void wehTextBox1__TextChanged(object sender, EventArgs e)
        {
            string value = cmbAranacakAlan.SelectedValue.ToString();
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter =
            string.Format("" + value + " LIKE '{0}%' OR " + value + " LIKE '% {0}%'", wehTextBox1.Texts);
        }

        private void radiusButton1_Click(object sender, EventArgs e)
        {
            DataListerToTableHelper.listInnerJoinAllStudentsDataToTableBetweenDates(dataGridView1, conn, dateTime1.Value, dateTime2.Value);

        }
    }
}
