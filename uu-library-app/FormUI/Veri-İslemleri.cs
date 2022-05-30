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
using uu_library_app.Entity.Concrete;

namespace uu_library_app.FormUI
{
    public partial class Veri_İslemleri : Form
    {
        Admin _admin;
        public Veri_İslemleri(Admin admin)
        {
            InitializeComponent();
            _admin = admin;
        }

        MySqlConnection conn = new MySqlConnection(DbConnection.connectionString);
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

            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("Select * From Student WHERE deleted=false", conn);
            da.Fill(dt);
            dgvDeneme.DataSource = dt;
            dgvDeneme.ColumnHeadersVisible = false;
            dgvDeneme.Columns[0].Visible = false;
            dgvDeneme.Columns[4].Visible = false;
            dgvDeneme.Columns[5].Visible = false;
            dgvDeneme.Columns[6].Visible = false;
            dgvDeneme.Columns[7].Visible = false;
            dgvDeneme.Columns[8].Visible = false;
            dgvDeneme.Columns[3].Visible = false;
            dgvDeneme.DefaultCellStyle.Font = new Font("Nirmala UI", 13);
            dgvDeneme.ScrollBars = ScrollBars.None;

            Dictionary<string, string> comboSourceDataTypes = new Dictionary<string, string>();
            comboSourceDataTypes.Add("tumOgrenciler", "Tüm Öğrenciler");
            comboSourceDataTypes.Add("teslimTarihiGecmisOgrenciler", "Teslim Tarihi Geçmiş Öğrenciler");
            comboSourceDataTypes.Add("tumSilinmisOgrenciler", "Tüm Silinmiş Öğrenciler");
            comboSourceDataTypes.Add("tumKitaplar", "Tüm Kitaplar");       
            comboSourceDataTypes.Add("enCokOkunanKitaplarAsc", "En Çok Okunan Kitaplar");
            comboSourceDataTypes.Add("tumSilinmisKitaplar", "Tüm Silinmiş Kitaplar");

            cmbVeri.DataSource = new BindingSource(comboSourceDataTypes, null);
            cmbVeri.DisplayMember = "Value";
            cmbVeri.ValueMember = "Key";

        }

        private void dgvDeneme_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtOgrenciId.Text = dgvDeneme.Rows[e.RowIndex].Cells[0].Value.ToString();
        }
    }
}
