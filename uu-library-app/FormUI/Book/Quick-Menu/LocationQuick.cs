using MessageBoxDenemesi;
using MySql.Data.MySqlClient;
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
using uu_library_app.Core.Helpers;
using uu_library_app.DataAccess.Concrete;
using uu_library_app.Entity.Concrete;

namespace uu_library_app.FormUI.Book.Quick_Menu
{
    public partial class LocationQuick : Form
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
        public LocationQuick(Admin admin)
        {
            InitializeComponent();
            _admin = admin;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));

            MySqlDataAdapter daCategories = new MySqlDataAdapter(SqlCommandHelper.getCategoriesCommand(conn));
            DataSet dsCategories = new DataSet();
            daCategories.Fill(dsCategories);

            //Kategori
            cmbKategori.DataSource = dsCategories.Tables[0];
            cmbKategori.DisplayMember = "name";
            cmbKategori.ValueMember = "id";
            cmbKategori.Text = "";

        }


        static MySqlConnection conn = new MySqlConnection(DbConnection.connectionString);
        LocationManager manager = new LocationManager(new LocationDal());
        LoggerManager logger = new LoggerManager(new LoggerDal());
        
        

        private void btnEkle_Click(object sender, EventArgs e)
        {
            string createGUID = System.Guid.NewGuid().ToString();
            if (txtKonum.Text == "")
            {
                wehMessageBox.Show("Lütfen geçerli bir değer giriniz!","Hata!",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }

            Location locationToAdd = new Location(createGUID, txtKonum.Text, cmbKategori.SelectedValue.ToString());
            try
            {
                manager.Add(locationToAdd);
                Logger log = new Logger(System.Guid.NewGuid().ToString(), _admin.id, "[ KonumId: " + locationToAdd.Id + " | " + locationToAdd.Shelf + " | KategoriId: " + locationToAdd.CategoryId + "" + "]" + _admin.FirstName + " " + _admin.LastName + " tarafından eklendi! -Tarih: " + DateTime.Now);
                logger.Log(log);
                wehMessageBox.Show("Başarıyla eklendi!",
                "Başarılı",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Information);
                this.Hide();
            }
            catch (Exception)
            {
                wehMessageBox.Show("Eklerken bir hata oluştu. Lütfen tekrar deneyiniz...","Hata!",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                throw;
            }
        }

        private void LocationQuick_Load(object sender, EventArgs e)
        {
            conn.Open();
            MySqlDataAdapter daCategories = new MySqlDataAdapter(SqlCommandHelper.getCategoriesCommand(conn));
            DataSet dsCategories = new DataSet();
            daCategories.Fill(dsCategories);
            SqlCommandHelper.getCategoriesCommand(conn).ExecuteNonQuery();
            conn.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
