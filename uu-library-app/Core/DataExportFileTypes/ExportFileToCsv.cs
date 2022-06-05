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
    public static class ExportFileToCsv
    {
        public static void Export(ComboBox cmbVeri, DataGridView dgvData, MySqlConnection conn, FileLoggerManager fileLoggerManager, Admin _admin)
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
                    FileWriter.ExportToCSV(ExportFileDataHelper.listInnerJoinAllStudentsDataToTable(dgvData, conn), Path.GetFullPath(save.FileName));
                    wehMessageBox.Show("Dosya başarıyla oluşturuldu...",
                    "Başarılı",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Information);
                    FileLogger fileLogger = new FileLogger(System.Guid.NewGuid().ToString(), _admin.Id, "[ Tüm Öğrenciler - CSV ] " + _admin.FirstName + " " + _admin.LastName + " tarafından oluşturuldu! -Tarih: " + DateTime.Now);
                    fileLoggerManager.Log(fileLogger);
                }
            }
            if (cmbVeri.SelectedValue.ToString().Equals("teslimTarihiGecmisOgrenciler"))
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "CSV dosyası|*.csv";
                save.OverwritePrompt = true;
                save.CreatePrompt = true;
                save.InitialDirectory = @"C:\";
                save.Title = "Kaydet";

                if (save.ShowDialog() == DialogResult.OK)
                {
                    DataTable dtDbDto = FileWriter.ToDataTable<DepositBookDto>(ExportFileDataHelper.getExpiredBookWithNames());
                    FileWriter.ExportToCSV(dtDbDto, Path.GetFullPath(save.FileName));
                    wehMessageBox.Show("Dosya başarıyla oluşturuldu...",
                    "Başarılı",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Information);
                    FileLogger fileLogger = new FileLogger(System.Guid.NewGuid().ToString(), _admin.Id, "[ Teslim Tarihi Geçmiş Öğrenciler - CSV ] " + _admin.FirstName + " " + _admin.LastName + " tarafından oluşturuldu! -Tarih: " + DateTime.Now);
                    fileLoggerManager.Log(fileLogger);
                }
            }
            if (cmbVeri.SelectedValue.ToString().Equals("tumSilinmisOgrenciler"))
            {
                DataListerToTableHelper.listInnerJoinAllStudentsDataToTable(dgvData, conn);
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "CSV dosyası|*.csv";
                save.OverwritePrompt = true;
                save.CreatePrompt = true;
                save.InitialDirectory = @"C:\";
                save.Title = "Kaydet";

                if (save.ShowDialog() == DialogResult.OK)
                {
                    FileWriter.ExportToCSV(ExportFileDataHelper.listInnerJoinAllDeletedStudentsDataToTable(dgvData, conn), Path.GetFullPath(save.FileName));
                    wehMessageBox.Show("Dosya başarıyla oluşturuldu...",
                    "Başarılı",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Information);
                    FileLogger fileLogger = new FileLogger(System.Guid.NewGuid().ToString(), _admin.Id, "[ Tüm Silinmiş Öğrenciler - CSV ] " + _admin.FirstName + " " + _admin.LastName + " tarafından oluşturuldu! -Tarih: " + DateTime.Now);
                    fileLoggerManager.Log(fileLogger);
                }
            }
            if (cmbVeri.SelectedValue.ToString().Equals("tumKitaplar"))
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "CSV dosyası|*.csv";
                save.OverwritePrompt = true;
                save.CreatePrompt = true;
                save.InitialDirectory = @"C:\";
                save.Title = "Kaydet";

                if (save.ShowDialog() == DialogResult.OK)
                {
                    FileWriter.ExportToCSV(ExportFileDataHelper.listInnerJoinAllBooksDataToTableWithTrNames(), Path.GetFullPath(save.FileName));
                    wehMessageBox.Show("Dosya başarıyla oluşturuldu...",
                    "Başarılı",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Information);
                    FileLogger fileLogger = new FileLogger(System.Guid.NewGuid().ToString(), _admin.Id, "[ Tüm Kitaplar - CSV ] " + _admin.FirstName + " " + _admin.LastName + " tarafından oluşturuldu! -Tarih: " + DateTime.Now);
                    fileLoggerManager.Log(fileLogger);
                }
            }
            if (cmbVeri.SelectedValue.ToString().Equals("enCokOkunanOnKitap"))
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "CSV dosyası|*.csv";
                save.OverwritePrompt = true;
                save.CreatePrompt = true;
                save.InitialDirectory = @"C:\";
                save.Title = "Kaydet";

                if (save.ShowDialog() == DialogResult.OK)
                {
                    FileWriter.ExportToCSV(ExportFileDataHelper.listInnerJoinMostFrequentTenBooks(), Path.GetFullPath(save.FileName));
                    wehMessageBox.Show("Dosya başarıyla oluşturuldu...",
                    "Başarılı",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Information);
                    FileLogger fileLogger = new FileLogger(System.Guid.NewGuid().ToString(), _admin.Id, "[ En Çok Okunan 10 Kitap - CSV ] " + _admin.FirstName + " " + _admin.LastName + " tarafından oluşturuldu! -Tarih: " + DateTime.Now);
                    fileLoggerManager.Log(fileLogger);
                }
            }
            if (cmbVeri.SelectedValue.ToString().Equals("tumSilinmisKitaplar"))
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "CSV dosyası|*.csv";
                save.OverwritePrompt = true;
                save.CreatePrompt = true;
                save.InitialDirectory = @"C:\";
                save.Title = "Kaydet";

                if (save.ShowDialog() == DialogResult.OK)
                {
                    FileWriter.ExportToCSV(ExportFileDataHelper.listInnerJoinAllDeletedBooksDataToTable(), Path.GetFullPath(save.FileName));
                    wehMessageBox.Show("Dosya başarıyla oluşturuldu...",
                    "Başarılı",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Information);
                    FileLogger fileLogger = new FileLogger(System.Guid.NewGuid().ToString(), _admin.Id, "[ Tüm Silinmiş Kitaplar - CSV ] " + _admin.FirstName + " " + _admin.LastName + " tarafından oluşturuldu! -Tarih: " + DateTime.Now);
                    fileLoggerManager.Log(fileLogger);
                }
            }
        }
    }
}
