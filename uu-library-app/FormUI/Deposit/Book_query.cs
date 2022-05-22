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

namespace uu_library_app.FormUI.Deposit
{
    public partial class Book_query : Form
    {
        public Book_query()
        {
            InitializeComponent();
        }

        MySqlConnection conn = new MySqlConnection(DbConnection.connectionString);

        private void Book_query_Load(object sender, EventArgs e)
        {
            DataListerHelper.listStudentDataToTable(dgvOgrenci, conn);
            
        }

        private void dgvOgrenci_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtOgrenciId.Text = dgvOgrenci.Rows[e.RowIndex].Cells[0].Value.ToString();
            DataListerHelper.listAllTakenBooksDataToTable(dgvAlinanKitaplar, conn, txtOgrenciId.Text);
            DataListerHelper.listUndepositBooksDataToTable(dgvTeslimEdilmeyenKitaplar, conn, txtOgrenciId.Text);
            DataListerHelper.listDepositBooksDataToTable(dgvTeslimEdilenKitaplar, conn, txtOgrenciId.Text);
        }
    }
}
