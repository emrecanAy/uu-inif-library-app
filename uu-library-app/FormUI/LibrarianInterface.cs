using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using uu_library_app.FormUI.Other_Operations;
using uu_library_app.FormUI.Deposit;
using uu_library_app.Entity.Concrete;
using uu_library_app.FormUI.MailSettings;

namespace uu_library_app.FormUI
{
    public partial class LibrarianInterface : Form
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
        public LibrarianInterface(Admin admin)
        {
            InitializeComponent();
            customizeDesign();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
            pnlNewNav.Height = btnDashboard.Height;
            pnlNewNav.Top = btnDashboard.Top;
            pnlNewNav.Left = btnDashboard.Left;
            panel3.Left = btnDashboard.Left;
            _admin = admin;
        }
        #region Methods
        private void customizeDesign()
        {
            panelBookSubMenu.Visible = false;
            panelMembersSubMenu.Visible = false;
            pnlDepositSubMenu.Visible = false;
            pnlDigerSubMenu.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            
        }
        private void hideSubMenu()
        {
            if (panelBookSubMenu.Visible == true)
                panelBookSubMenu.Visible = false;
            if (panelMembersSubMenu.Visible == true)
                panelMembersSubMenu.Visible = false;
            if (pnlDepositSubMenu.Visible == true)
                pnlDepositSubMenu.Visible = false;
            if (pnlDigerSubMenu.Visible == true)
                pnlDigerSubMenu.Visible = false;
            if (panel3.Visible == true)
                panel3.Visible = false;
            if (panel4.Visible == true)
                panel4.Visible = false;


        }
        private void showSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                hideSubMenu();
                subMenu.Visible = true;
            }
            else
            {
                subMenu.Visible = false;
            }
        }
        private Form activeForm = null;
        public void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChildFormNew.Controls.Add(childForm);
            panelChildFormNew.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();

        }
#endregion
        private void LibrarianInterface_Load(object sender, EventArgs e)
        {
            openChildForm(new Gosterge_Paneli());
            lblAdmin.Text = _admin.FirstName + " " + _admin.LastName;
        }
        #region Buttons Click Events
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            openChildForm(new Gosterge_Paneli());
            pnlNewNav.Height = btnDashboard.Height;
            pnlNewNav.Top = btnDashboard.Top;
            pnlNewNav.Left = btnDashboard.Left;
            btnDashboard.BackColor = Color.FromArgb(46, 51, 73);
            hideSubMenu();
        }

        private void btnBooks_Click(object sender, EventArgs e)
        {
            pnlNewNav.Height = btnBooks.Height;
            pnlNewNav.Top = btnBooks.Top;
            pnlNewNav.Left = btnBooks.Left;
            btnBooks.BackColor = Color.FromArgb(46, 51, 73);
            showSubMenu(panelBookSubMenu);
        }

        private void btnMembers_Click(object sender, EventArgs e)
        {
            pnlNewNav.Height = btnMembers.Height;
            pnlNewNav.Top = btnMembers.Top;
            pnlNewNav.Left = btnMembers.Left;
            btnMembers.BackColor = Color.FromArgb(46, 51, 73);
            showSubMenu(panelMembersSubMenu);
        }

        private void btnDeposit_Click(object sender, EventArgs e)
        {
            pnlNewNav.Height = btnDeposit.Height;
            pnlNewNav.Top = btnDeposit.Top;
            pnlNewNav.Left = btnDeposit.Left;
            btnDeposit.BackColor = Color.FromArgb(46, 51, 73);
            showSubMenu(pnlDepositSubMenu);
        }
        private void btnDigerislemler_click(object sender, EventArgs e)
        {
            pnlNewNav.Height = btnDigerislemler.Height;
            pnlNewNav.Top = btnDigerislemler.Top;
            pnlNewNav.Left = btnDigerislemler.Left;
            btnDigerislemler.BackColor = Color.FromArgb(46, 51, 73);
            showSubMenu(pnlDigerSubMenu);
        }


        private void btnLogOut_Click(object sender, EventArgs e)
        {
            pnlNewNav.Height = btnLogOut.Height;
            pnlNewNav.Top = btnLogOut.Top;
            pnlNewNav.Left = btnLogOut.Left;
            btnLogOut.BackColor = Color.FromArgb(46, 51, 73);
            Application.Exit();
        }
        
        #endregion
        #region Buttons Leave
        private void btnDashboard_Leave(object sender, EventArgs e)
        {
            btnDashboard.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnBooks_Leave(object sender, EventArgs e)
        {
            btnBooks.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnMembers_Leave(object sender, EventArgs e)
        {
            btnMembers.BackColor = Color.FromArgb(24, 30, 54);
        }

        
        private void btnDeposit_Leave(object sender, EventArgs e)
        {
            btnDeposit.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnSettings_Leave(object sender, EventArgs e)
        {
            btnDigerislemler.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnLogOut_Leave(object sender, EventArgs e)
        {
            btnLogOut.BackColor = Color.FromArgb(24, 30, 54);
        }
        private void btnSettings_Leave_1(object sender, EventArgs e)
        {
            btnDigerislemler.BackColor = Color.FromArgb(24, 30, 54);
        }
        #endregion
        #region CRUD Books Buttons
        private void btnAddBook_Click(object sender, EventArgs e)
        {
            openChildForm(new Add_Books(_admin));
            hideSubMenu();
        }

        private void btnDeleteBook_Click(object sender, EventArgs e)
        {
            openChildForm(new delete_book(_admin));
            hideSubMenu();
        }

        private void btnEditBook_Click(object sender, EventArgs e)
        {
            openChildForm(new edit_book(_admin));
            hideSubMenu();
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            openChildForm(new book_listing());
            hideSubMenu();
        }


        #endregion
        #region CRUD Members Buttons
        private void btnAddMember_Click(object sender, EventArgs e)
        {
            openChildForm(new Add_Student(_admin));
            hideSubMenu();
        }

        private void btnUyeListele_Click(object sender, EventArgs e)
        {
            openChildForm(new student_listing());
            hideSubMenu();
        }

        private void btnDeleteMember_Click(object sender, EventArgs e)
        {
            openChildForm(new Edit_Student(_admin));
            hideSubMenu();
        }

        private void btnEditMember_Click(object sender, EventArgs e)
        {
            openChildForm(new Delete_Student(_admin));
            hideSubMenu();
        }

        private void btnLending_Click(object sender, EventArgs e)
        {
            openChildForm(new Borrowing_Book(_admin));
            hideSubMenu();
        }

        private void btnReceive_Click(object sender, EventArgs e)
        {
            openChildForm(new Get_Book_Back(_admin));
            hideSubMenu();
        }

        private void btnYazar_Click(object sender, EventArgs e)
        {
            openChildForm(new author_actions(_admin));
            hideSubMenu();
        }
        private void btnYayinevi_Click(object sender, EventArgs e)
        {
            openChildForm(new publisher_actions(_admin));
            hideSubMenu();
        }
        private void btnDil_Click(object sender, EventArgs e)
        {
            openChildForm(new Language_Operations(_admin));
            hideSubMenu();
        }

        private void btnKategori_Click(object sender, EventArgs e)
        {
            openChildForm(new Category_Operations(_admin));
            hideSubMenu();
        }

        private void btnBolum_Click(object sender, EventArgs e)
        {
            openChildForm(new Department_Operations(_admin));
            hideSubMenu();
        }

        private void btnKonum_Click(object sender, EventArgs e)
        {
            openChildForm(new Location_actions(_admin));   
            hideSubMenu();
        }
        private void btnTeslimSorgu_Click(object sender, EventArgs e)
        {
            openChildForm(new Book_query());
            hideSubMenu();
        }
        private void btnMailSettings_Click(object sender, EventArgs e)
        {
            openChildForm(new Veri_İslemleri(_admin));
            hideSubMenu();
        }
        private void btnFakulte_Click(object sender, EventArgs e)
        {
            openChildForm(new Faculty_Operations(_admin));
            hideSubMenu();
        }

        private void btnVeriCikti_Click(object sender, EventArgs e)
        {
           
            openChildForm(new Mail_Operations(_admin));
            hideSubMenu();
        }





        #endregion

        private void panelChildFormNew_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            pnlNewNav.Height = btnSettings.Height;
            pnlNewNav.Top = btnSettings.Top;
            pnlNewNav.Left = btnSettings.Left;
            btnSettings.BackColor = Color.FromArgb(46, 51, 73);
            showSubMenu(panel3);
        }

        private void btnSettings_Leave_2(object sender, EventArgs e)
        {
            btnSettings.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnDosya_Click(object sender, EventArgs e)
        {
            pnlNewNav.Height = btnDosya.Height;
            pnlNewNav.Top = btnDosya.Top;
            pnlNewNav.Left = btnDosya.Left;
            btnDosya.BackColor = Color.FromArgb(46, 51, 73);
            showSubMenu(panel4);
        }

        private void btnDosya_Leave(object sender, EventArgs e)
        {
            btnDosya.BackColor = Color.FromArgb(24, 30, 54);
        }
    }
}
