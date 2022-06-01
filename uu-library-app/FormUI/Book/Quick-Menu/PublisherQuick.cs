using MessageBoxDenemesi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
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
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
               int nLeftRect,
               int nTopRect,
               int nRightRect,
               int nBottomRect,
               int nWidthEllipse,
               int nHeightEllipse
               );
        private Admin _admin;
        public PublisherQuick(Admin admin)
        {
            InitializeComponent();
            _admin = admin;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));

        }

        LoggerManager logger = new LoggerManager(new LoggerDal());
        PublisherManager manager = new PublisherManager(new PublisherDal());
        private void btnEkle_Click(object sender, EventArgs e)
        {
            string createGUID = System.Guid.NewGuid().ToString();
            if (txtAd.Text == "")
            {
                wehMessageBox.Show("Lütfen geçerli bir değer giriniz!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Publisher publisherToAdd = new Publisher(createGUID, txtAd.Text);

            try
            {
                manager.Add(publisherToAdd);
                Logger log = new Logger(System.Guid.NewGuid().ToString(), _admin.id, "[ " + publisherToAdd.Id + " | " + publisherToAdd.Name + "]" + _admin.FirstName + " " + _admin.LastName + " tarafından eklendi! -Tarih: " + DateTime.Now);
                logger.Log(log);
                wehMessageBox.Show("Başarıyla eklendi!",
                "Başarılı",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                wehMessageBox.Show("Eklerken bir hata oluştu. Lütfen tekrar deneyiniz...", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                throw;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
