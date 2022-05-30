using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace uu_library_app.FormUI
{
    public partial class Veri_İslemleri : Form
    {
        public Veri_İslemleri()
        {
            InitializeComponent();
        }

        private void Veri_İslemleri_Load(object sender, EventArgs e)
        {
            dgvLog.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvLog.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLog.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(46, 51, 73);
            dgvLog.EnableHeadersVisualStyles = false;
            dgvLog.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvLog.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(46, 51, 73);
            dgvLog.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9.0F, FontStyle.Bold);

            dgvLogOgr.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvLogOgr.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLogOgr.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLogOgr.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(46, 51, 73);
            dgvLogOgr.EnableHeadersVisualStyles = false;
            dgvLogOgr.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvLogOgr.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(46, 51, 73);
            dgvLogOgr.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9.0F, FontStyle.Bold);

            Dictionary<string, string> comboSourceDataTypes = new Dictionary<string, string>();
            comboSourceDataTypes.Add("tumOgrenciler", "Tüm Öğrenciler");
            comboSourceDataTypes.Add("tumKitaplar", "Tüm Kitaplar");
            comboSourceDataTypes.Add("teslimTarihiGecmisOgrenciler", "Teslim Tarihi Geçmiş Öğrenciler");
            comboSourceDataTypes.Add("enCokOkunanKitaplarAsc", "En Çok Okunan Kitaplar");
            comboSourceDataTypes.Add("tumSilinmisOgrenciler", "Tüm Silinmiş Öğrenciler");
            comboSourceDataTypes.Add("tumSilinmisKitaplar", "Tüm Silinmiş Kitaplar");

            cmbVeri.DataSource = new BindingSource(comboSourceDataTypes, null);
            cmbVeri.DisplayMember = "Value";
            cmbVeri.ValueMember = "Key";
        }
    }
}
