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
using uu_library_app.Core.RoutinCustomJobs;
using uu_library_app.DataAccess.Concrete;
using uu_library_app.Entity.Concrete;

namespace uu_library_app
{
    public partial class Dashboard : Form
    {
        public Dashboard()
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
        MailSenderJob mailSenderJob = new MailSenderJob();

        private void panelChildForm_Paint(object sender, PaintEventArgs e)
        {
                
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                lblToplamUye.Text = studentManager.getAll().Count().ToString();
                lblToplamYazar.Text = authorManager.getAll().Count().ToString();
                lblToplamKategori.Text = categoryManager.getAll().Count().ToString();
                lblToplamKitap.Text = bookManager.getAll().Count().ToString();
                lblTumEmanetKitaplar.Text = depositBookManager.getAll().Count().ToString();
                lblEmanetVerilenKitap.Text = depositBookManager.getAllUndeposited().Count().ToString();
                lblTeslimEdilenKitap.Text = depositBookManager.getAllDeposited().Count().ToString();

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
    }
}
