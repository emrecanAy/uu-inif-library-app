using FastMember;
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
using uu_library_app.Business.Concrete;
using uu_library_app.Core.Helpers;
using uu_library_app.DataAccess.Concrete;
using uu_library_app.Entity.Concrete;
using uu_library_app.Entity.Concrete.DTO;

namespace uu_library_app.FormUI.Deposit
{
    public partial class WhoHasThatBook : Form
    {
        public WhoHasThatBook()
        {
            InitializeComponent();
        }

        MySqlConnection conn = new MySqlConnection(DbConnection.connectionString);
        DepositBookManager depositBookManager = new DepositBookManager(new DepositBookDal());
        BookManager bookManager = new BookManager(new BookDal());
        SettingsManager settingsManager = new SettingsManager(new SettingsDal());

        MySqlDataAdapter pageAdapter;
        DataSet pageDS;
        int scollVal;
        private void WhoHasThatBook_Load(object sender, EventArgs e)
        {
            //DataListerHelper.listInnerJoinAllBooksDataToTable(dgvKitaplar, conn);
            pageAdapter = DataListerToDataAdapter.listBooksForPagination(conn);
            pageDS = new DataSet();
            pageAdapter.Fill(pageDS, scollVal, 20, "book");
            dgvKitaplar.DataSource = pageDS;
            dgvKitaplar.DataMember = "book";
            dgvKitaplar.Columns[3].Width = 50;
            dgvKitaplar.Columns[0].Visible = false;
            dgvKitaplar.Columns[1].HeaderText = "Kitap";
            dgvKitaplar.Columns[2].HeaderText = "Yazar";
            dgvKitaplar.Columns[3].HeaderText = "Yayınevi";
            dgvKitaplar.Columns[8].Visible = false;
            dgvKitaplar.Columns[4].Visible = false;
            dgvKitaplar.Columns[5].Visible = false;
            dgvKitaplar.Columns[6].Visible = false;
            dgvKitaplar.Columns[7].Visible = false;
            dgvKitaplar.Columns[9].Visible = false;
            dgvKitaplar.Columns[10].Visible = false;
            dgvKitaplar.Columns[11].Visible = false;
            dgvKitaplar.Columns[12].Visible = false;
            dgvKitaplar.Columns[13].Visible = false;
            this.dgvKitaplar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvKitaplar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvKitaplar.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(46, 51, 73);
            dgvKitaplar.EnableHeadersVisualStyles = false;
            dgvKitaplar.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvKitaplar.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(46, 51, 73);
            //dgvKitaplar.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12.0F, FontStyle.Bold);
            dgvKitaplar.DefaultCellStyle.Font = new Font("Nirmala UI", 13);
            dgvKitaplar.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvKitaplar.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvKitaplar.DefaultCellStyle.ForeColor = Color.White;


        }

        private void wehTextBox1__TextChanged(object sender, EventArgs e)
        {
            (dgvKitaplar.DataSource as DataTable).DefaultView.RowFilter =
            string.Format("bookName LIKE '{0}%' OR bookName LIKE '% {0}%'", wehTextBox1.Texts);
        }

        
        public void GetStudentsWhoHasThatBook()
        {
            List<StudentDto> studentList = new List<StudentDto>();
            studentList.Clear();

            List<DepositBook> depositBooksList = depositBookManager.getAllByBookId(txtBookId.Text);
            foreach (DepositBook depositBook in depositBooksList)
            {
                DateTime dt = depositBook.DepositDate.AddDays(settingsManager.getSettings().DepositDay);
                studentList.Add(DataListerHelper.listInnerJoinStudentDataToTableByStudentId(conn, depositBook.StudentId, dt));
            }
            
            dgvUyeler.DataSource = studentList;
            dgvUyeler.Columns[0].Visible = false;
            dgvUyeler.Columns[1].HeaderText = "Öğrenci";
            dgvUyeler.Columns[2].HeaderText = "Numara";
            dgvUyeler.Columns[3].HeaderText = "Bölüm";
            dgvUyeler.Columns[4].HeaderText = "G.Tarih";
            depositBooksList.Clear();

        }

        private void dgvKitaplar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                txtBookId.Text = dgvKitaplar.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtAd.Text = dgvKitaplar.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtYayinevi.Text = dgvKitaplar.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtIsbn.Text = dgvKitaplar.Rows[e.RowIndex].Cells[7].Value.ToString();
                txtYazar.Text = dgvKitaplar.Rows[e.RowIndex].Cells[2].Value.ToString();

                GetStudentsWhoHasThatBook();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dgvUyeler.DataSource = null;

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            scollVal = scollVal + 20;
            if (scollVal > 50)
            {
                scollVal = bookManager.getAll().Count();
            }
            pageDS.Clear();
            pageAdapter.Fill(pageDS, scollVal, 20, "book");
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            scollVal = scollVal - 20;
            if (scollVal <= 0)
            {
                scollVal = 0;
            }
            pageDS.Clear();
            pageAdapter.Fill(pageDS, scollVal, 20, "book");
        }
    }
}
