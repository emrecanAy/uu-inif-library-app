using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uu_library_app.Business.Abstract;
using uu_library_app.Business.Concrete;
using uu_library_app.DataAccess.Concrete;
using uu_library_app.Entity.Concrete;

namespace uu_library_app
{
    public partial class author_actions : Form
    {
        AuthorManager manager = new AuthorManager(new AuthorDal());

        public author_actions()
        {
            InitializeComponent();
        }

     
        private void btnEkle_Click(object sender, EventArgs e)
        {
            string createGUID = System.Guid.NewGuid().ToString(); 
            Author authorToAdd = new Author(createGUID, txtAd.Text, txtSoyad.Text);

            try
            {
                manager.Add(authorToAdd);
                txtId.Clear();
                txtAd.Clear();
                txtSoyad.Clear();
            }
            catch (Exception)
            {
                MessageBox.Show("Bir hata oluştu. Tekrar deneyiniz.");
                throw;
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            
            try
            {
                manager.Delete(txtId.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Bir hata oluştu. Tekrar deneyiniz.");
                throw;
            }
            
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            Author authorToUpdate = new Author(txtId.Text, txtAd.Text, txtSoyad.Text);
            try
            {
                manager.Update(authorToUpdate);
            }
            catch (Exception)
            {
                MessageBox.Show("Bir hata oluştu. Tekrar deneyiniz.");
                throw;
            }
        }
    }
}
