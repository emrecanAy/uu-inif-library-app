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
            cmbAranacakAlan.SelectedIndex = 0;
            this.dgvGecikmis.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvGecikmis.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvGecikmis.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(46, 51, 73);
            dgvGecikmis.EnableHeadersVisualStyles = false;
            dgvGecikmis.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvGecikmis.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(46, 51, 73);
            dgvGecikmis.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9.0F, FontStyle.Bold);
            dgvGecikmis.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvGecikmis.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        private void wehTextBox1__TextChanged(object sender, EventArgs e)
        {
          
        }
    }
}
