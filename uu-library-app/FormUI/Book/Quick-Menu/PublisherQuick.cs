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

namespace uu_library_app.FormUI.Book.Quick_Menu
{
    public partial class PublisherQuick : Form
    {
        private Admin _admin;
        public PublisherQuick(Admin admin)
        {
            InitializeComponent();
            _admin = admin;
        }

        LoggerManager logger = new LoggerManager(new LoggerDal());
        PublisherManager manager = new PublisherManager(new PublisherDal());
        private void btnEkle_Click(object sender, EventArgs e)
        {
            string createGUID = System.Guid.NewGuid().ToString();
            if (txtAd.Text == "")
            {
                MessageBox.Show("Lütfen geçerli bir değer giriniz!");
                return;
            }
            Publisher publisherToAdd = new Publisher(createGUID, txtAd.Text);

            try
            {
                manager.Add(publisherToAdd);
                Logger log = new Logger(System.Guid.NewGuid().ToString(), _admin.id, "[ " + publisherToAdd.Id + " | " + publisherToAdd.Name + "] eklendi! -Tarih: " + DateTime.Now);
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
