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
using uu_library_app.FormUI.TextBoxHelper;

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

      
        private void WhoHasThatBook_Load(object sender, EventArgs e)
        {
           conn.Open();

            DataTable dt = new DataTable();

            MySqlCommand command = new MySqlCommand("SELECT Book.id, Book.bookName, CONCAT(Author.firstName, ' ', Author.lastName) AS authorFullName, Publisher.name AS publisherName, Language.language, Category.name AS categoryName, Book.pageCount, Book.isbnNumber, Book.publishDate, Book.stockCount, Location.shelf, Book.interpreter AS interpreterName, Book.createdAt, Book.publishCount, Book.fixtureNo FROM Book INNER JOIN Language ON Book.languageId = Language.id INNER JOIN Author ON Book.authorId = Author.id INNER JOIN Category ON Book.categoryId = Category.id INNER JOIN Publisher ON Book.publisherId = Publisher.id INNER JOIN Location ON Book.locationId = Location.id WHERE Book.deleted=0", conn);
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            da.Fill(dt);
            dgvKitaplar.DataSource = dt;


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


            this.dgvUyeler.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUyeler.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUyeler.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(46, 51, 73);
            dgvUyeler.EnableHeadersVisualStyles = false;
            dgvUyeler.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvUyeler.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(46, 51, 73);
            dgvUyeler.DefaultCellStyle.Font = new Font("Nirmala UI", 13);
            dgvUyeler.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvUyeler.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvUyeler.DefaultCellStyle.ForeColor = Color.White;
            conn.Close();

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

        public void GetPersonnelsWhoHasThatBook()
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

      

        private void cmbDil_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
    }
}
