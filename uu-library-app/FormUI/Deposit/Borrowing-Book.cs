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
     
        private void Borrowing_Book_Load(object sender, EventArgs e)
        {

            DataListerToTableHelper.listBorrowingBookStudentDataToTable(dataGridView1, conn);
            DataListerToTableHelper.listInnerJoinBorrowingBookDataToTable(dataGridView3, conn);
      
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        DataView dataView = new DataView();
        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter =
            string.Format("number LIKE '{0}%' OR number LIKE '% {0}%'", txtAra.Text);
        }

        private void txtAra_MouseEnter(object sender, EventArgs e)
        {
            

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
            txtAdSoyad.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtBolum.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtOkulNo.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtKitapId.Text = dataGridView3.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtKitapAd.Text = dataGridView3.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtIsbn.Text = dataGridView3.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtYayinevi.Text = dataGridView3.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtYazar.Text = dataGridView3.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {

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
