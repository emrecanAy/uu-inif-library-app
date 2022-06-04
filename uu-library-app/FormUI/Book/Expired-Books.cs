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

namespace uu_library_app.FormUI.Book
{
    public partial class Expired_Books : Form
    {
        public Expired_Books()
        {
            InitializeComponent();
        }

        private void Expired_Books_Load(object sender, EventArgs e)
        {
            dgvGecikmis.DataSource = ExportFileDataHelper.getExpiredBookWithNames();
            dgvGecikmis.Columns[0].Visible = false;

            Dictionary<string, string> comboSource = new Dictionary<string, string>();
            comboSource.Add("Ogrenci", "Öğrenci");
            comboSource.Add("studentNo", "Okul No");
            comboSource.Add("book", "Kitap");
            comboSource.Add("author", "Yazar");
            cmbAranacakAlan.DataSource = new BindingSource(comboSource, null);
            cmbAranacakAlan.DisplayMember = "Value";
            cmbAranacakAlan.ValueMember = "Key";
        }

        private void wehTextBox1__TextChanged(object sender, EventArgs e)
        {
            string value = cmbAranacakAlan.SelectedValue.ToString();
            (dgvGecikmis.DataSource as DataTable).DefaultView.RowFilter =
            string.Format("" + value + " LIKE '{0}%' OR " + value + " LIKE '% {0}%'", wehTextBox1.Texts);
        }
    }
}
