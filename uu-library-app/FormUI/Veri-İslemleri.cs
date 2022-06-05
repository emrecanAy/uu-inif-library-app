using FastMember;
using MessageBoxDenemesi;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uu_library_app.Core.DataExportFileTypes;
using uu_library_app.Core.Helpers;
using uu_library_app.Core.Logger.FileLogger;
using uu_library_app.Entity.Concrete;
using uu_library_app.Entity.Concrete.DTO;

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
        FileLoggerManager fileLoggerManager = new FileLoggerManager(new FileLoggerDal());
        private void Veri_İslemleri_Load(object sender, EventArgs e)
        {
            #region Students
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(SqlCommandHelper.getStudentsCommand(conn));
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
            #endregion
            #region LeftLog
            dgvLog.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvLog.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLog.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(46, 51, 73);
            dgvLog.EnableHeadersVisualStyles = false;
            dgvLog.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvLog.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(46, 51, 73);
            dgvLog.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9.0F, FontStyle.Bold);
            dgvLog.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvLog.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            Dictionary<string, string> comboSourceDataTypes = new Dictionary<string, string>();
            comboSourceDataTypes.Add("tumOgrenciler", "Tüm Öğrenciler");
            comboSourceDataTypes.Add("teslimTarihiGecmisOgrenciler", "Teslim Tarihi Geçmiş Öğrenciler");
            comboSourceDataTypes.Add("tumSilinmisOgrenciler", "Tüm Silinmiş Öğrenciler");
            comboSourceDataTypes.Add("tumKitaplar", "Tüm Kitaplar");       
            comboSourceDataTypes.Add("enCokOkunanOnKitap", "En Çok Okunan 10 Kitap");
            comboSourceDataTypes.Add("tumSilinmisKitaplar", "Tüm Silinmiş Kitaplar");

            cmbVeri.DataSource = new BindingSource(comboSourceDataTypes, null);
            cmbVeri.DisplayMember = "Value";
            cmbVeri.ValueMember = "Key";
            #endregion
            #region RightLog

            dgvLogOgr.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvLogOgr.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLogOgr.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLogOgr.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(46, 51, 73);
            dgvLogOgr.EnableHeadersVisualStyles = false;
            dgvLogOgr.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvLogOgr.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(46, 51, 73);
            dgvLogOgr.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9.0F, FontStyle.Bold);

            Dictionary<string, string> comboSourceDataTypesOgr = new Dictionary<string, string>();
            comboSourceDataTypesOgr.Add("oduncAlinan", "Ödünç Aldığı Kitaplar");
            comboSourceDataTypesOgr.Add("teslimAlinan", "Teslim Ettiği Kitaplar");
            comboSourceDataTypesOgr.Add("geciken", "Teslim Tarihi Geciken Kitaplar");

            cmbVeriOgr.DataSource = new BindingSource(comboSourceDataTypesOgr, null);
            cmbVeriOgr.DisplayMember = "Value";
            cmbVeriOgr.ValueMember = "Key";

            DataTable dtFileLog = new DataTable();
            MySqlDataAdapter daFileLog = new MySqlDataAdapter("Select * From FileLog", conn);
            daFileLog.Fill(dtFileLog);
            dgvLog.DataSource = dtFileLog;
            dgvLog.ColumnHeadersVisible = false;
            dgvLog.Columns[0].Visible = false;
            dgvLog.Columns[1].Visible = false;
            dgvLog.Columns[2].HeaderText = "Log Message";
            dgvLog.Columns[3].Visible = false;
            dgvLog.DefaultCellStyle.Font = new Font("Nirmala UI", 13);


            dgvLogOgr.DataSource = dtFileLog;
            dgvLogOgr.ColumnHeadersVisible = false;
            dgvLogOgr.Columns[0].Visible = false;
            dgvLogOgr.Columns[1].Visible = false;
            dgvLogOgr.Columns[2].HeaderText = "Log Message";
            dgvLogOgr.Columns[3].Visible = false;
            dgvLogOgr.DefaultCellStyle.Font = new Font("Nirmala UI", 13);
            #endregion
        }
        private void dgvDeneme_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtOgrenciId.Text = dgvDeneme.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtOgrenciAdSoyad.Text = dgvDeneme.Rows[e.RowIndex].Cells[1].Value.ToString() + dgvDeneme.Rows[e.RowIndex].Cells[2].Value.ToString(); ;
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            ExportFileToPdf.Export(cmbVeri, dgvData, conn, fileLoggerManager, _admin);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            ExportFileToExcel.Export(cmbVeri, dgvData, conn, fileLoggerManager, _admin);
        }

        private void btnCSV_Click(object sender, EventArgs e)
        {
            ExportFileToCsv.Export(cmbVeri, dgvData, conn, fileLoggerManager, _admin);
        }

        private void btnJSON_Click(object sender, EventArgs e)
        {
            ExportFileToJson.Export(cmbVeri, dgvData, conn, fileLoggerManager, _admin);
        }

        private void btnXML_Click(object sender, EventArgs e)
        {
            ExportFileToXml.Export(cmbVeri, dgvData, conn, fileLoggerManager, _admin);
        }

        private void wehTextBox1__TextChanged(object sender, EventArgs e)
        {
            (dgvDeneme.DataSource as DataTable).DefaultView.RowFilter =
            string.Format("number LIKE '{0}%' OR number LIKE '% {0}%'", wehTextBox1.Texts);
        }

        private void btnPDFOgr_Click(object sender, EventArgs e)
        {
            ExportStudentFileToPdf.Export(cmbVeriOgr, fileLoggerManager, conn, dgvData, _admin, txtOgrenciId.Text, txtOgrenciAdSoyad.Text);
        }

        private void btnExcelOgr_Click(object sender, EventArgs e)
        {
            ExportStudentFileToExcel.Export(cmbVeriOgr, fileLoggerManager, conn, dgvData, _admin, txtOgrenciId.Text, txtOgrenciAdSoyad.Text);
        }

        private void btnCSVOgr_Click(object sender, EventArgs e)
        {
            ExportStudentFileToCsv.Export(cmbVeriOgr, fileLoggerManager, conn, dgvData, _admin, txtOgrenciId.Text, txtOgrenciAdSoyad.Text);
        }

        private void btnJSONOgr_Click(object sender, EventArgs e)
        {
            ExportStudentFileToJson.Export(cmbVeriOgr, fileLoggerManager, conn, dgvData, _admin, txtOgrenciId.Text, txtOgrenciAdSoyad.Text);
        }

        private void btnXMLOgr_Click(object sender, EventArgs e)
        {
            ExportStudentFileToXml.Export(cmbVeriOgr, fileLoggerManager, conn, dgvData, _admin, txtOgrenciId.Text, txtOgrenciAdSoyad.Text);

        }
    }
}
