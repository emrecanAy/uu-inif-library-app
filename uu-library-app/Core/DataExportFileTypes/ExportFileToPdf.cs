using FastMember;
using MessageBoxDenemesi;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uu_library_app.Core.Helpers;
using uu_library_app.Core.Logger.FileLogger;
using uu_library_app.Entity.Concrete;
using uu_library_app.Entity.Concrete.DTO;

namespace uu_library_app.Core.DataExportFileTypes
{
    public static class ExportFileToPdf
    {
        public static void Export(ComboBox cmbVeri, DataGridView dgvData, MySqlConnection conn, FileLoggerManager fileLoggerManager, Admin _admin)
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
            if (cmbVeri.SelectedValue.ToString().Equals("tumSilinmisOgrenciler"))
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
                    FileWriter.ExportToPdf(ExportFileDataHelper.listInnerJoinAllDeletedStudentsDataToTable(dgvData, conn), Path.GetFullPath(save.FileName));
                    wehMessageBox.Show("Dosya başarıyla oluşturuldu...",
                    "Başarılı",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Information);
                    FileLogger fileLogger = new FileLogger(System.Guid.NewGuid().ToString(), _admin.Id, "[ Tüm Silinmiş Öğrenciler - PDF ] " + _admin.FirstName + " " + _admin.LastName + " tarafından oluşturuldu! -Tarih: " + DateTime.Now);
                    fileLoggerManager.Log(fileLogger);
                }
            }
            if (cmbVeri.SelectedValue.ToString().Equals("tumKitaplar"))
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "Pdf Dosyası|*.pdf";
                save.OverwritePrompt = true;
                save.CreatePrompt = true;
                save.InitialDirectory = @"C:\";
                save.Title = "Kaydet";

                if (save.ShowDialog() == DialogResult.OK)
                {
                    FileWriter.ExportToPdf(ExportFileDataHelper.listInnerJoinAllBooksDataToTable(), Path.GetFullPath(save.FileName));
                    wehMessageBox.Show("Dosya başarıyla oluşturuldu...",
                    "Başarılı",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Information);
                    FileLogger fileLogger = new FileLogger(System.Guid.NewGuid().ToString(), _admin.Id, "[ Tüm Kitaplar - PDF ] " + _admin.FirstName + " " + _admin.LastName + " tarafından oluşturuldu! -Tarih: " + DateTime.Now);
                    fileLoggerManager.Log(fileLogger);
                }
            }
            if (cmbVeri.SelectedValue.ToString().Equals("enCokOkunanOnKitap"))
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "Pdf Dosyası|*.pdf";
                save.OverwritePrompt = true;
                save.CreatePrompt = true;
                save.InitialDirectory = @"C:\";
                save.Title = "Kaydet";

                if (save.ShowDialog() == DialogResult.OK)
                {
                    FileWriter.ExportToPdf(ExportFileDataHelper.listInnerJoinMostFrequentTenBooks(), Path.GetFullPath(save.FileName));
                    wehMessageBox.Show("Dosya başarıyla oluşturuldu...",
                    "Başarılı",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Information);
                    FileLogger fileLogger = new FileLogger(System.Guid.NewGuid().ToString(), _admin.Id, "[ En Çok Okunan 10 Kitap - PDF ] " + _admin.FirstName + " " + _admin.LastName + " tarafından oluşturuldu! -Tarih: " + DateTime.Now);
                    fileLoggerManager.Log(fileLogger);
                }
            }
            if (cmbVeri.SelectedValue.ToString().Equals("tumSilinmisKitaplar"))
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "Pdf Dosyası|*.pdf";
                save.OverwritePrompt = true;
                save.CreatePrompt = true;
                save.InitialDirectory = @"C:\";
                save.Title = "Kaydet";

                if (save.ShowDialog() == DialogResult.OK)
                {
                    FileWriter.ExportToPdf(ExportFileDataHelper.listInnerJoinAllDeletedBooksDataToTable(), Path.GetFullPath(save.FileName));
                    wehMessageBox.Show("Dosya başarıyla oluşturuldu...",
                    "Başarılı",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Information);
                    FileLogger fileLogger = new FileLogger(System.Guid.NewGuid().ToString(), _admin.Id, "[ Tüm Silinmiş Kitaplar - PDF ] " + _admin.FirstName + " " + _admin.LastName + " tarafından oluşturuldu! -Tarih: " + DateTime.Now);
                    fileLoggerManager.Log(fileLogger);
                }
            }
        }
    }
}
