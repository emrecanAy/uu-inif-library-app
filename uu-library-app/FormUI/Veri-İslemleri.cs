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
            dgvLog.DefaultCellStyle.ForeColor = Color.Black;

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
            

        }

        private void dgvDeneme_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtOgrenciId.Text = dgvDeneme.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            if (cmbVeri.SelectedValue.ToString().Equals("tumOgrenciler"))
            {
                DataListerToTableHelper.listInnerJoinAllStudentsDataToTable(dgvData, conn);
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "Pdf Dosyası|*.pdf";
                save.OverwritePrompt = true;
                save.CreatePrompt = true;
                save.InitialDirectory = @"C:\";
                save.Title = "Kaydet";

                if (save.ShowDialog() == DialogResult.OK)
                {
                    FileWriter.ExportToPdf(ExportFileDataHelper.listInnerJoinAllStudentsDataToTable(dgvData, conn), Path.GetFullPath(save.FileName));
                    wehMessageBox.Show("Dosya başarıyla oluşturuldu...",
                    "Başarılı",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Information);
                    FileLogger fileLogger = new FileLogger(System.Guid.NewGuid().ToString(), _admin.Id, "[ Tüm Öğrenciler - PDF ] " + _admin.FirstName + " " + _admin.LastName + " tarafından oluşturuldu! -Tarih: " + DateTime.Now);
                    fileLoggerManager.Log(fileLogger);
                }
            }
            if (cmbVeri.SelectedValue.ToString().Equals("teslimTarihiGecmisOgrenciler"))
            {
                IEnumerable<DepositBookDto> data = ExportFileDataHelper.getExpiredBookWithNames();
                DataTable table = new DataTable();
                using (var reader = ObjectReader.Create(data))
                {
                    table.Load(reader);
                }
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "Pdf Dosyası|*.pdf";
                save.OverwritePrompt = true;
                save.CreatePrompt = true;
                save.InitialDirectory = @"C:\";
                save.Title = "Kaydet";

                if (save.ShowDialog() == DialogResult.OK)
                {
                    FileWriter.ExportToPdf(table, Path.GetFullPath(save.FileName));
                    wehMessageBox.Show("Dosya başarıyla oluşturuldu...",
                    "Başarılı",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Information);
                    FileLogger fileLogger = new FileLogger(System.Guid.NewGuid().ToString(), _admin.Id, "[ Teslim Tarihi Gecikmiş Öğrenciler - PDF ] " + _admin.FirstName + " " + _admin.LastName + " tarafından oluşturuldu! -Tarih: " + DateTime.Now);
                    fileLoggerManager.Log(fileLogger);
                }
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (cmbVeri.SelectedValue.ToString().Equals("tumOgrenciler"))
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "EXCEL dosyası|*.xlsx";
                save.OverwritePrompt = true;
                save.CreatePrompt = true;
                save.InitialDirectory = @"C:\";
                save.Title = "Kaydet";

                if (save.ShowDialog() == DialogResult.OK)
                {
                    FileWriter.ToEXCEL(ExportFileDataHelper.listInnerJoinAllStudentsDataToTable(dgvData, conn), Path.GetFullPath(save.FileName));
                    wehMessageBox.Show("Dosya başarıyla oluşturuldu...",
                    "Başarılı",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Information);
                    FileLogger fileLogger = new FileLogger(System.Guid.NewGuid().ToString(), _admin.Id, "[ Tüm Öğrenciler - EXCEL ] " + _admin.FirstName + " " + _admin.LastName + " tarafından oluşturuldu! -Tarih: " + DateTime.Now);
                    fileLoggerManager.Log(fileLogger);
                }
            }
        }

        private void btnCSV_Click(object sender, EventArgs e)
        {
            if (cmbVeri.SelectedValue.ToString().Equals("tumOgrenciler"))
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "CSV dosyası|*.csv";
                save.OverwritePrompt = true;
                save.CreatePrompt = true;
                save.InitialDirectory = @"C:\";
                save.Title = "Kaydet";

                if (save.ShowDialog() == DialogResult.OK)
                {
                    FileWriter.ToCSV(ExportFileDataHelper.listInnerJoinAllStudentsDataToTable(dgvData, conn), Path.GetFullPath(save.FileName));
                    wehMessageBox.Show("Dosya başarıyla oluşturuldu...",
                    "Başarılı",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Information);
                    FileLogger fileLogger = new FileLogger(System.Guid.NewGuid().ToString(), _admin.Id, "[ Tüm Öğrenciler - CSV ] " + _admin.FirstName + " " + _admin.LastName + " tarafından oluşturuldu! -Tarih: " + DateTime.Now);
                    fileLoggerManager.Log(fileLogger);
                }
            }
        }

        private void btnJSON_Click(object sender, EventArgs e)
        {
            if (cmbVeri.SelectedValue.ToString().Equals("tumOgrenciler"))
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "JSON dosyası|*.json";
                save.OverwritePrompt = true;
                save.CreatePrompt = true;
                save.InitialDirectory = @"C:\";
                save.Title = "Kaydet";

                if (save.ShowDialog() == DialogResult.OK)
                {
                    DataSet ds = new DataSet();
                    ds.Tables.Add(ExportFileDataHelper.listInnerJoinAllStudentsDataToTable(dgvData, conn));
                    FileWriter.ToJSON(ds, Path.GetFullPath(save.FileName));
                    wehMessageBox.Show("Dosya başarıyla oluşturuldu...",
                    "Başarılı",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Information);
                    FileLogger fileLogger = new FileLogger(System.Guid.NewGuid().ToString(), _admin.Id, "[ Tüm Öğrenciler - JSON ] " + _admin.FirstName + " " + _admin.LastName + " tarafından oluşturuldu! -Tarih: " + DateTime.Now);
                    fileLoggerManager.Log(fileLogger);
                }
            }
        }

        private void btnXML_Click(object sender, EventArgs e)
        {
            if (cmbVeri.SelectedValue.ToString().Equals("tumOgrenciler"))
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "XML dosyası|*.xml";
                save.OverwritePrompt = true;
                save.CreatePrompt = true;
                save.InitialDirectory = @"C:\";
                save.Title = "Kaydet";

                if (save.ShowDialog() == DialogResult.OK)
                {
                    DataSet ds = new DataSet();
                    ds.Tables.Add(ExportFileDataHelper.listInnerJoinAllStudentsDataToTable(dgvData, conn));
                    FileWriter.ToXML(ds, Path.GetFullPath(save.FileName), "Ogrenciler");
                    wehMessageBox.Show("Dosya başarıyla oluşturuldu...",
                    "Başarılı",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Information);
                    FileLogger fileLogger = new FileLogger(System.Guid.NewGuid().ToString(), _admin.Id, "[ Tüm Öğrenciler - XML ] " + _admin.FirstName + " " + _admin.LastName + " tarafından oluşturuldu! -Tarih: " + DateTime.Now);
                    fileLoggerManager.Log(fileLogger);
                }
            }
        }

        private void wehTextBox1__TextChanged(object sender, EventArgs e)
        {
            (dgvDeneme.DataSource as DataTable).DefaultView.RowFilter =
            string.Format("number LIKE '{0}%' OR number LIKE '% {0}%'", wehTextBox1.Texts);
        }
    }
}
