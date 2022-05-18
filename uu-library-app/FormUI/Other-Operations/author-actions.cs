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
        AuthorManager manager;

        public author_actions()
        {
            InitializeComponent();
        }

     
        private void btnEkle_Click(object sender, EventArgs e)
        {
            string createGUID = System.Guid.NewGuid().ToString();
            txtId.Text = createGUID;

            manager = new AuthorManager(new AuthorDal());
            Author addToAuthor = new Author(txtId.Text, txtAd.Text, txtSoyad.Text, DateTime.Now, false);
            manager.Add(addToAuthor);
            txtId.Clear();
            txtAd.Clear();
            txtSoyad.Clear();


            //Author authorToAdd = new Author(txtId.Text, txtAd.Text, txtSoyad.Text, DateTime.Now, false);
            //_service.Add(authorToAdd);
        }

        private void btnSil_Click(object sender, EventArgs e)
        {

        }
    }
}
