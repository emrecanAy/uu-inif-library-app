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
using uu_library_app.DataAccess.Concrete;
using uu_library_app.Entity.Concrete;

namespace uu_library_app.FormUI.Settings
{
    public partial class FacultyQuick : Form
    {
        private Admin _admin;
        public FacultyQuick(Admin admin)
        {
            InitializeComponent();
            _admin = admin;
        }

        LoggerManager logger = new LoggerManager(new LoggerDal());
        FacultyManager manager = new FacultyManager(new FacultyDal());

        private void btnEkle_Click(object sender, EventArgs e)
        {
            string createGUID = System.Guid.NewGuid().ToString();
            if (txtAd.Text == "" || txtAd.Text.Length < 3)
            {
                MessageBox.Show("Lütfen en az üç harf içeren geçerli bir değer giriniz!");
                return;
            }
            Faculty facultyToAdd = new Faculty(createGUID, txtAd.Text);
            try
            {
                manager.Add(facultyToAdd);
                Logger log = new Logger(System.Guid.NewGuid().ToString(), _admin.id, "[ " + facultyToAdd.Id + " | " + facultyToAdd.Name + "] eklendi! -Tarih: " + DateTime.Now);
                logger.Log(log);
            }
            catch (Exception)
            {
                MessageBox.Show("Eklerken bir hata oluştu. Lütfen tekrar deneyiniz...");
                throw;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
