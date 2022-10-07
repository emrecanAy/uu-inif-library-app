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
    public partial class AuthorQuick : Form
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
        public AuthorQuick(Admin admin)
        {
            InitializeComponent();
            _admin = admin;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));

        }

        AuthorManager manager = new AuthorManager(new AuthorDal());
        LoggerManager logger = new LoggerManager(new LoggerDal());

        private void btnEkle_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            
        }

        private void btnEkle_Click_1(object sender, EventArgs e)
        {
            string createGUID = System.Guid.NewGuid().ToString();

            if (txtAd.Text == "" || txtSoyad.Text == "" || txtAd.Text.Length < 2 || txtSoyad.Text.Length < 2)
            {
                wehMessageBox.Show("Lütfen geçerli ve en az 2 karakter içeren değerler giriniz", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Author authorToAdd = new Author(createGUID, txtAd.Text, txtSoyad.Text);
            try
            {
                manager.Add(authorToAdd);
                Logger log = new Logger(System.Guid.NewGuid().ToString(), _admin.id, "[ " + authorToAdd.Id + " | " + authorToAdd.FirstName + " " + authorToAdd.LastName + "" + "]" + _admin.FirstName + " " + _admin.LastName + " tarafından eklendi! -Tarih: " + DateTime.Now);
                logger.Log(log);
                wehMessageBox.Show("Başarıyla eklendi!",
                "Başarılı",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Information);
                this.Hide();
            }
            catch (Exception)
            {
                wehMessageBox.Show("Bir hata oluştu. Tekrar deneyin!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                throw;
            }
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
