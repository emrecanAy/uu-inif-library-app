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

            //DataListerHelper.listStudentDataToTable(dgvOgrenci, conn);
            //DataListerHelper.listStudentDataToTable(dgvDeneme, conn);
            dgvDeneme.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvDeneme.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDeneme.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDeneme.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(46, 51, 73);
            dgvDeneme.EnableHeadersVisualStyles = false;
            dgvDeneme.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvDeneme.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(46, 51, 73);
            dgvDeneme.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9.0F, FontStyle.Bold);
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
            dgvDeneme.DefaultCellStyle.Font = new Font("Nirmala UI", 13);
            dgvDeneme.ScrollBars = ScrollBars.None;



            dgvDeneme.DefaultCellStyle.WrapMode = DataGridViewTriState.True;


        }

        private void wehTextBox1__TextChanged(object sender, EventArgs e)
        {
            (dgvDeneme.DataSource as DataTable).DefaultView.RowFilter =
            string.Format("number LIKE '{0}%' OR number LIKE '% {0}%'", wehTextBox1.Texts);
        }

        private void dgvOgrenci_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtOgrenciId.Text = dgvDeneme.Rows[e.RowIndex].Cells[0].Value.ToString();
            DataListerHelper.listAllTakenBooksDataToTable(dgvAlinanKitaplar, conn, txtOgrenciId.Text);
            DataListerHelper.listUndepositBooksDataToTable(dgvTeslimEdilmeyenKitaplar, conn, txtOgrenciId.Text);
            DataListerHelper.listDepositBooksDataToTable(dgvTeslimEdilenKitaplar, conn, txtOgrenciId.Text);
        }

        private void dgvDeneme_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtOgrenciId.Text = dgvDeneme.Rows[e.RowIndex].Cells[0].Value.ToString();
            DataListerHelper.listAllTakenBooksDataToTable(dgvAlinanKitaplar, conn, txtOgrenciId.Text);
            DataListerHelper.listUndepositBooksDataToTable(dgvTeslimEdilmeyenKitaplar, conn, txtOgrenciId.Text);
            DataListerHelper.listDepositBooksDataToTable(dgvTeslimEdilenKitaplar, conn, txtOgrenciId.Text);
        }
    }
}
