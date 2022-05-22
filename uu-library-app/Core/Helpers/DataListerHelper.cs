using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace uu_library_app.Core.Helpers
{
    public static class DataListerHelper
    {
        public static void listBookDataToTable(DataGridView dataGrid, MySqlConnection conn)
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM Book WHERE deleted=false", conn);
            da.Fill(dt);
            dataGrid.DataSource = dt;
            dataGrid.Columns[0].Visible = false;
            dataGrid.Columns[6].Visible = false;
            dataGrid.Columns[3].HeaderText = "Yazar";
            dataGrid.Columns[2].Visible = false;
            dataGrid.Columns[5].HeaderText = "Yayınevi";
            dataGrid.Columns[4].Visible = false;
            dataGrid.Columns[1].HeaderText = "İsim";
            dataGrid.Columns[7].Visible = false;
            dataGrid.Columns[8].Visible = false;
            dataGrid.Columns[9].Visible = false;
            dataGrid.Columns[10].Visible = false;
            dataGrid.Columns[11].Visible = false;
            dataGrid.Columns[12].Visible = false;
            dataGrid.Columns[13].Visible = false;
            dataGrid.Columns[14].Visible = false;
            dataGrid.Columns[15].Visible = false;
            dataGrid.RowHeadersVisible = false;
            dataGrid.DefaultCellStyle.Font = new Font("Nirmala UI", 13);
        }

        public static void listStudentDataToTable(DataGridView dataGrid, MySqlConnection conn)
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("Select * From Student WHERE deleted=false", conn);
            da.Fill(dt);
            dataGrid.DataSource = dt;
            dataGrid.Columns[1].HeaderText = "";
            dataGrid.Columns[0].Visible = false;
            dataGrid.Columns[2].HeaderText = "";
            dataGrid.Columns[3].Visible = false;
            dataGrid.Columns[4].Visible = false;
            dataGrid.Columns[5].HeaderText = "";
            dataGrid.Columns[6].Visible = false;
            dataGrid.Columns[7].Visible = false;
            dataGrid.Columns[8].Visible = false;
            dataGrid.ColumnHeadersVisible = false;
            dataGrid.RowHeadersVisible = false;
            dataGrid.DefaultCellStyle.Font = new Font("Nirmala UI", 13);

        }


    }
}
