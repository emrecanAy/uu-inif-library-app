using MessageBoxDenemesi;
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

namespace uu_library_app.FormUI
{
    public partial class Gosterge_Paneli : Form
    {
        public Gosterge_Paneli()
        {
            InitializeComponent();
        }

        MySqlConnection conn = new MySqlConnection(DbConnection.connectionString);
        StudentManager studentManager = new StudentManager(new StudentDal());
        AuthorManager authorManager = new AuthorManager(new AuthorDal());
        CategoryManager categoryManager = new CategoryManager(new CategoryDal());
        BookManager bookManager = new BookManager(new BookDal());
        DepositBookManager depositBookManager = new DepositBookManager(new DepositBookDal());
        DataListerHelper dataListerHelper = new DataListerHelper();
        DashboardDataHelper dashboardDataHelper = new DashboardDataHelper();

        private void Gosterge_Paneli_Load(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                lblToplamUye.Text = studentManager.getAll().Count().ToString();
                //lblToplamYazar.Text = authorManager.getAll().Count().ToString();
                //lblToplamKategori.Text = categoryManager.getAll().Count().ToString();
                lblToplamKitap.Text = bookManager.getAll().Count().ToString();
                lblOduncSayi.Text = depositBookManager.getAll().Count().ToString();
                lblOduncVerilen.Text = depositBookManager.getAllUndeposited().Count().ToString();
                lblTeslimAlinan.Text = depositBookManager.getAllDeposited().Count().ToString();
                Student[] arrStudent = dashboardDataHelper.GetTopThreeReaders().ToArray();
                Department[] arrDepartment = dashboardDataHelper.GetTopThreeReaderDepartments().ToArray();

                Student s1 = arrStudent[0];
                Student s2 = arrStudent[1];
                Student s3 = arrStudent[2];

                Department d1 = arrDepartment[0];
                Department d2 = arrDepartment[1];
                Department d3 = arrDepartment[2];

                lblUye1.Text = s1.FirstName;
                lblUye2.Text = s2.FirstName;
                lblUye3.Text = s3.FirstName;

                lblUye1OkumaSayisi.Text = s1.Number;
                lblUye2OkumaSayisi.Text = s2.Number;
                lblUye3OkumaSayisi.Text = s3.Number;

                lblBolum1.Text = d1.Name;
                lblBolum2.Text = d2.Name;
                lblBolum3.Text = d3.Name;

                lblBolum1Sayi.Text = d1.Id.ToString();
                lblBolum2Sayi.Text = d2.Id.ToString();
                lblBolum3Sayi.Text = d3.Id.ToString();


                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter("SELECT bookId,count(*)'count', Book.bookName FROM DepositBook INNER JOIN Book ON DepositBook.bookId=Book.id GROUP BY DepositBook.bookId ORDER BY count(*) DESC LIMIT 5", conn);
                da.Fill(dt);
                chartTopBooks.DataSource = dt;
                chartTopBooks.Series[0].XValueMember = "bookName";
                chartTopBooks.Series[0].YValueMembers = "count";
                chartTopBooks.DataBind();
                conn.Close();

                DataTable dtCategory = new DataTable();
                MySqlDataAdapter daCategory = new MySqlDataAdapter("SELECT Book.categoryId,count(*)'count', Category.name FROM DepositBook INNER JOIN Book ON DepositBook.bookId = Book.id INNER JOIN Category ON Book.categoryId = Category.id GROUP BY Book.categoryId ORDER BY count DESC LIMIT 5", conn);
                daCategory.Fill(dtCategory);
                chartTopCategories.DataSource = dtCategory;
                chartTopCategories.Series[0].XValueMember = "name";
                chartTopCategories.Series[0].YValueMembers = "count";
                chartTopCategories.DataBind();
                conn.Close();

                






            }
            catch (Exception)
            {
                wehMessageBox.Show("Lütfen internet bağlantınızı kontrol edin.\nSorun devam ediyorsa bir yetkiliyle iletişime geçin...",
                 "Sunucuya bağlanırken bir hata oluştu!",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Warning);
                Application.Exit();
                return;
            }
        }

        private void btnLast7Days_Click(object sender, EventArgs e)
        {
            DateTime startDate = DateTime.Now.AddDays(-7);
            DataTable dtDeposit = new DataTable();
            MySqlCommand command = new MySqlCommand("SELECT DepositBook.id,count(*)'count', Book.bookName FROM DepositBook INNER JOIN Book ON DepositBook.bookId=Book.id WHERE DATE(DepositBook.depositDate) BETWEEN @p1 AND @p2 GROUP BY DepositBook.id", conn);
            command.Parameters.AddWithValue("@p1", startDate);
            command.Parameters.AddWithValue("@p2", DateTime.Now);
            MySqlDataAdapter daDeposit = new MySqlDataAdapter(command);
            daDeposit.Fill(dtDeposit);
            chartDepositBook.DataSource = dtDeposit;
            chartDepositBook.Series[0].XValueMember = "bookName";
            chartDepositBook.Series[0].YValueMembers = "count";
            chartDepositBook.DataBind();
            

        }

        private void btnLast30Days_Click(object sender, EventArgs e)
        {
            DateTime startDate = DateTime.Now.AddDays(-30);
            DataTable dtDeposit = new DataTable();
            MySqlCommand command = new MySqlCommand("SELECT id,count(*)'count' FROM DepositBook WHERE DATE(depositDate) BETWEEN @p1 AND @p2 GROUP BY id", conn);
            command.Parameters.AddWithValue("@p1", startDate);
            command.Parameters.AddWithValue("@p2", DateTime.Now);
            MySqlDataAdapter daDeposit = new MySqlDataAdapter(command);
            daDeposit.Fill(dtDeposit);
            chartDepositBook.DataSource = dtDeposit;
            chartDepositBook.Series[0].XValueMember = "count";
            chartDepositBook.Series[0].YValueMembers = "count";
            chartDepositBook.DataBind();
        }

        private void label27_Click(object sender, EventArgs e)
        {

        }
    }
}
