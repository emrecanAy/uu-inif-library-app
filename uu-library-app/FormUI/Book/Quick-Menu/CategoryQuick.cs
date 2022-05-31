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

namespace uu_library_app.FormUI.MailSettings
{
    public partial class CategoryQuick : Form
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
        public CategoryQuick(Admin admin)
        {
            InitializeComponent();
            _admin = admin;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));

        }

        CategoryManager manager = new CategoryManager(new CategoryDal());
        LoggerManager logger = new LoggerManager(new LoggerDal());

        private void btnEkle_Click(object sender, EventArgs e)
        {
            string createGUID = System.Guid.NewGuid().ToString();
            if (txtAd.Text == "" || txtAd.Text.Length < 3)
            {
                MessageBox.Show("Lütfen en az üç harf içeren geçerli bir değer giriniz!");
                return;
            }
            
            try
            {
                Category categoryToAdd = new Category(createGUID, txtAd.Text);
                manager.Add(categoryToAdd);
                Logger log = new Logger(System.Guid.NewGuid().ToString(), _admin.id, "[ " + categoryToAdd.Id + " | " + categoryToAdd.Name + "]" + _admin.FirstName + " " + _admin.LastName + " tarafından eklendi! -Tarih: " + DateTime.Now);
                logger.Log(log);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eklerken bir hata oluştu. Lütfen tekrar deneyiniz...");
                throw ex;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
