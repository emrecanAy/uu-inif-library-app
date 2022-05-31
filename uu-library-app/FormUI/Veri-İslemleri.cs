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

            Dictionary<string, string> comboSourceFileTypes = new Dictionary<string, string>();
            comboSourceFileTypes.Add("excel", "EXCEL");
            comboSourceFileTypes.Add("pdf", "PDF");
            comboSourceFileTypes.Add("csv", "CSV");
            comboSourceFileTypes.Add("xml", "XML");
            comboSourceFileTypes.Add("json", "JSON");

            cmbDosyaTuru.DataSource = new BindingSource(comboSourceFileTypes, null);
            cmbDosyaTuru.DisplayMember = "Value";
            cmbDosyaTuru.ValueMember = "Key";

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
                }
            }
        }
    }
}
