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
            Dictionary<string, string> comboSource = new Dictionary<string, string>();
            comboSource.Add("studentFullName", "Öğrenci Adı");
            comboSource.Add("number", "Okul No");
            comboSource.Add("eMail", "Eposta");
            comboSource.Add("name", "Bölüm");
            cmbAranacakAlan.DataSource = new BindingSource(comboSource, null);
            cmbAranacakAlan.DisplayMember = "Value";
            cmbAranacakAlan.ValueMember = "Key";
        }

        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            string value = cmbAranacakAlan.SelectedValue.ToString();
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter =
            string.Format("" + value + " LIKE '{0}%' OR " + value + " LIKE '% {0}%'", txtAra.Text);
        }
    }
}
