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

namespace uu_library_app
{
    public partial class Get_Book_Back : Form
    {
        public Get_Book_Back()
        {
            InitializeComponent();
        }

        MySqlConnection conn = new MySqlConnection(DbConnection.connectionString);
        DepositBookManager depositBookManager = new DepositBookManager(new DepositBookDal());
        BookManager bookManager = new BookManager(new BookDal());

        private void listDataToTable()
        {
            DataTable dt = new DataTable();
            MySqlCommand getAllCommand = new MySqlCommand("SELECT DepositBook.depositDate, Student.number, Student.firstName, Student.lastName, Book.bookName'bookName', Book.isbnNumber, Author.firstName'authorFirstName', Author.lastName'authorLastName', Publisher.name'publisherName', Book.id, DepositBook.id, DepositBook.status FROM DepositBook INNER JOIN Student ON DepositBook.studentId = Student.id INNER JOIN Book ON DepositBook.bookId = Book.id INNER JOIN Author ON Book.authorId = Author.id INNER JOIN Publisher ON Book.publisherId = Publisher.id WHERE Student.id=@p1 AND DepositBook.status=0", conn);

            var studentId = dgvOgrenci.CurrentRow.Cells[0].Value.ToString();
            getAllCommand.Parameters.AddWithValue("@p1", studentId);
            MySqlDataAdapter da = new MySqlDataAdapter(getAllCommand);
            da.Fill(dt);
            dgvDahaOnceAlinanKitaplar.DataSource = dt;
            dgvDahaOnceAlinanKitaplar.Columns[0].Width = 100;
            dgvDahaOnceAlinanKitaplar.Columns[1].Visible = false;
            dgvDahaOnceAlinanKitaplar.Columns[0].HeaderText = "Ödünç Tarihi";
            dgvDahaOnceAlinanKitaplar.Columns[4].HeaderText = "Kitap Adı";
            dgvDahaOnceAlinanKitaplar.Columns[2].Visible = false;
            dgvDahaOnceAlinanKitaplar.Columns[3].Visible = false;
            dgvDahaOnceAlinanKitaplar.Columns[5].Visible = false;
            dgvDahaOnceAlinanKitaplar.Columns[6].Visible = false;
            dgvDahaOnceAlinanKitaplar.Columns[7].Visible = false;
            dgvDahaOnceAlinanKitaplar.Columns[8].Visible = false;
            dgvDahaOnceAlinanKitaplar.Columns[9].Visible = false;
            dgvDahaOnceAlinanKitaplar.Columns[10].Visible = false;
            dgvDahaOnceAlinanKitaplar.Columns[11].Visible = false;
        }

        private void Get_Book_Back_Load(object sender, EventArgs e)
        {
            DataListerHelper.listStudentDataToTable(dgvOgrenci, conn);
        }

        private void dgvOgrenci_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        private void dgvOgrenci_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtOgrenciId.Text = dgvOgrenci.Rows[e.RowIndex].Cells[0].Value.ToString();
            conn.Open();
            listDataToTable();
            conn.Close();

        }

        private void txtOgrenciId_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btnGetir_Click(object sender, EventArgs e)
        {
            
        }

        private void dgvOgrenci_SelectionChanged(object sender, EventArgs e)
        {
            
        }

        private void dgvOgrenci_DataSourceChanged(object sender, EventArgs e)
        {
            
        }

        private void dgvDahaOnceAlinanKitaplar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtKitapId.Text = dgvDahaOnceAlinanKitaplar.Rows[e.RowIndex].Cells[9].Value.ToString();
            txtAd.Text = dgvDahaOnceAlinanKitaplar.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtYayinevi.Text = dgvDahaOnceAlinanKitaplar.Rows[e.RowIndex].Cells[8].Value.ToString();
            txtIsbn.Text = dgvDahaOnceAlinanKitaplar.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtYazar.Text = dgvDahaOnceAlinanKitaplar.Rows[e.RowIndex].Cells[6].Value.ToString()+" "+dgvDahaOnceAlinanKitaplar.Rows[e.RowIndex].Cells[7].Value.ToString();
            txtDepositBookId.Text = dgvDahaOnceAlinanKitaplar.Rows[e.RowIndex].Cells[10].Value.ToString();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {

            Book book = bookManager.getById(txtKitapId.Text);
            book.StockCount = book.StockCount + 1;
            bookManager.Update(book);

            DepositBook depositBook = new DepositBook();
            depositBook.Status = true;
            depositBookManager.Update(depositBook);
            Console.WriteLine("Stok Adet :"+book.StockCount);

            /*
            datagridde seçilen kitabın count'una okunacak.
            kitap teslim alındığında o kitabın count'u +1 artırılacak.
            Yeni sorgu olanağı: count'u 0 olan kitapları getir. count'u 0 olmayan kitaplar    
             */

            /*
            
            -> DepositBook tablosunda teslimAlmaTarihi ve teslimEdilmesiGerekenTarih de tutulmali.

            Kitap ödünç verilirken ödünç ver butonuna basılıp kayıt yapıldığında teslimEdilmesiGerekenTarih
            default olarak DateTime.Now + 15 olarak kaydedilecek.(15 gün max teslim süresi)

            Kitap teslim alındığında   
            teslimAlmaTarihi DateTime.Now olarak güncellenecek.
            Sonra kıyas yapılacak.
            int kacGunGecikti = teslimAlmaTarihi - teslimEdilmesiGerekenTarih;
            int cezaPuani = kacGunGecikti * -5; gibi gibi vs vs.

             */



            if (txtDepositBookId.Text == "")
            {
                MessageBox.Show("Lütfen bir kitap seçiniz!");
                return;
            }

            try
            {
                depositBookManager.depositBook(txtDepositBookId.Text);
                MessageBox.Show("Kitap başarıyla teslim alındı...");
                listDataToTable();
            }
            catch (Exception)
            {
                MessageBox.Show("Bir hata oluştu! Tekrar deneyin...");
                throw;
            }
            
        }

        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            (dgvOgrenci.DataSource as DataTable).DefaultView.RowFilter =
            string.Format("number LIKE '{0}%' OR number LIKE '% {0}%'", txtAra.Text);
        }

        private void txtKitapAra_TextChanged(object sender, EventArgs e)
        {
            (dgvDahaOnceAlinanKitaplar.DataSource as DataTable).DefaultView.RowFilter =
            string.Format("bookName LIKE '{0}%' OR bookName LIKE '% {0}%'", txtKitapAra.Text);
        }
    }
}
