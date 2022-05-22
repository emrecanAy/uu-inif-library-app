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
using uu_library_app.DataAccess.Concrete.EntityFramework;
using uu_library_app.Entity.Concrete;

namespace uu_library_app
{
    public partial class Borrowing_Book : Form
    {
        public Borrowing_Book()
        {
            InitializeComponent();
        }

        MySqlConnection conn = new MySqlConnection(DbConnection.connectionString);
        DepositBookManager depositBookManager = new DepositBookManager(new DepositBookDal());
        StudentManager studentManager = new StudentManager(new StudentDal());
        BookManager bookManager = new BookManager(new BookDal());
        private void listDepositBookDataToTable()
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("Select * From DepositBook WHERE deleted=false", conn);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[1].HeaderText = "";
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.DefaultCellStyle.Font = new Font("Nirmala UI", 13);

        }
        private void Borrowing_Book_Load(object sender, EventArgs e)
        {

            DataListerHelper.listStudentDataToTable(dataGridView1, conn);
            DataListerHelper.listBookDataToTable(dataGridView3, conn);
            txtAra.Text = "Öğrenci numarasını giriniz...";
            txtAra.ForeColor = Color.Silver;

            

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            

        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            //DataTable dt = new DataTable();
            //DataView dataView = new DataView();
            //dataView = dt.DefaultView;
            //dataView.RowFilter = "number like '"+txtAra.Text+"%'";
            //dataGridView1.DataSource = dataView;
            //dataGridView1.Visible = true;
        }

        private void txtAra_MouseEnter(object sender, EventArgs e)
        {
            if (txtAra.Text == "Öğrenci numarasını giriniz...")
            {
                txtAra.Text = "";
            }

        }

        private void txtAra_MouseLeave(object sender, EventArgs e)
        {
            if (txtAra.Text.Trim() == "")
            {
                txtAra.Text = "Öğrenci numarasını giriniz...";
                txtAra.ForeColor = Color.Silver;
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtOgrenciId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtAdSoyad.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() +" "+ dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtBolum.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            txtOkulNo.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtKitapId.Text = dataGridView3.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtKitapAd.Text = dataGridView3.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtIsbn.Text = dataGridView3.Rows[e.RowIndex].Cells[7].Value.ToString();
            txtYayinevi.Text = dataGridView3.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtYazar.Text = dataGridView3.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {

            /*
            datagridde seçilen kitabın count'una bakılacak.
            eğer count = 0 ise elimizde bulunmuyor diye uyarı çıkarsın.
            eğer count mesela 10 ise karşı kişiye verilsin. ve count -1 azaltılsın.

             */

            if(txtKitapId.Text == "" || txtKitapAd.Text == "")
            {
                MessageBox.Show("Ödünç verilecek kitabı seçin...");
                return;
            }
            if (txtOgrenciId.Text == "" || txtAdSoyad.Text == "")
            {
                MessageBox.Show("Ödünç verilecek öğrenciyi seçin...");
                return;
            }

            try
            {
                string createGUID = System.Guid.NewGuid().ToString();
                Book book = bookManager.getById(txtKitapId.Text);
                Console.WriteLine(book.BookName);
                DepositBook depositBookToAdd = new DepositBook(createGUID, txtOgrenciId.Text, txtKitapId.Text, DateTime.Now);
                if (book.StockCount == 0)
                {
                    MessageBox.Show("Kütüphanede bulunan tüm " + book.BookName + " kitapları ödünç verildi!");
                    return;
                }
                depositBookManager.Add(depositBookToAdd);
                MessageBox.Show("Kitap başarıyla ödünç verildi!");
                book.StockCount = book.StockCount - 1;
                bookManager.Update(book);
                Console.WriteLine("Stok adet: "+book.StockCount);

            }
            catch (Exception)
            {
                MessageBox.Show("Bir hata oluştu. Tekrar deneyin!");
                throw;
            }

        }

        private void txtKitapId_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
