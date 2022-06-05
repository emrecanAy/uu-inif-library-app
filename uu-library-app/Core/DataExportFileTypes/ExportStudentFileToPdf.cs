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

namespace uu_library_app.Core.DataExportFileTypes
{
    public static class ExportStudentFileToPdf
    {
        public static void Export(ComboBox cmbVeriOgr, FileLoggerManager fileLoggerManager, MySqlConnection conn, DataGridView dgvData, Admin _admin, string studentId, string studentFullName)
        {
            if (cmbVeriOgr.SelectedValue.ToString().Equals("oduncAlinan"))
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "PDF Dosyası|*.pdf";
                save.OverwritePrompt = true;
                save.CreatePrompt = true;
                save.InitialDirectory = @"C:\";
                save.Title = "Kaydet";

                if (save.ShowDialog() == DialogResult.OK)
                {
                    FileWriter.ExportToPdf(DataListerToTableHelper.listAllTakenBooksDataToTableByStudentId(conn, studentId), Path.GetFullPath(save.FileName));
                    wehMessageBox.Show("Dosya başarıyla oluşturuldu...",
                    "Başarılı",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Information);
                    FileLogger fileLogger = new FileLogger(System.Guid.NewGuid().ToString(), _admin.Id, "[ Öğrenci: " + studentFullName + "  | Tüm Ödünç Alınan Kitaplar - PDF ] " + _admin.FirstName + " " + _admin.LastName + " tarafından oluşturuldu! -Tarih: " + DateTime.Now);
                    fileLoggerManager.Log(fileLogger);
                }
            }
            if (cmbVeriOgr.SelectedValue.ToString().Equals("teslimAlinan"))
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "PDF Dosyası|*.pdf";
                save.OverwritePrompt = true;
                save.CreatePrompt = true;
                save.InitialDirectory = @"C:\";
                save.Title = "Kaydet";

                if (save.ShowDialog() == DialogResult.OK)
                {
                    FileWriter.ExportToPdf(DataListerHelper.listDepositBooksDataToTableByStudentId(dgvData, conn, studentId), Path.GetFullPath(save.FileName));
                    wehMessageBox.Show("Dosya başarıyla oluşturuldu...",
                    "Başarılı",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Information);
                    FileLogger fileLogger = new FileLogger(System.Guid.NewGuid().ToString(), _admin.Id, "[ Öğrenci: " + studentFullName + "  | Tüm Teslim Edilen Kitaplar - PDF ] " + _admin.FirstName + " " + _admin.LastName + " tarafından oluşturuldu! -Tarih: " + DateTime.Now);
                    fileLoggerManager.Log(fileLogger);
                }
            }
            if (cmbVeriOgr.SelectedValue.ToString().Equals("geciken"))
            {
                IEnumerable<DepositBookDto> data = ExportFileDataHelper.getExpiredBookWithNamesByStudentId(studentId);
                DataTable table = new DataTable();
                using (var reader = ObjectReader.Create(data))
                {
                    table.Load(reader);
                }
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "PDF Dosyası|*.pdf";
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
                    FileLogger fileLogger = new FileLogger(System.Guid.NewGuid().ToString(), _admin.Id, "[ Öğrenci: " + studentFullName + "  | Tüm Teslim Tarihi Geciken Kitaplar - PDF ] " + _admin.FirstName + " " + _admin.LastName + " tarafından oluşturuldu! -Tarih: " + DateTime.Now);
                    fileLoggerManager.Log(fileLogger);
                }
            }
        }
    }
}
