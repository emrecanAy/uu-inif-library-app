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

namespace uu_library_app.FormUI
{
    public partial class KutuphaneGorevlisiArayuz : Form
    {
        [DllImport("Gd32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
            (int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
            );
        public KutuphaneGorevlisiArayuz()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
            pnlNav.Height = btnDashboard.Height;
            pnlNav.Top = btnDashboard.Top;
            pnlNav.Left = btnDashboard.Left;
            btnDashboard.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void KutuphaneGorevlisiArayuz_Load(object sender, EventArgs e)
        {

        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnDashboard.Height;
            pnlNav.Top = btnDashboard.Top;
            pnlNav.Left = btnDashboard.Left;
            btnDashboard.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void btnAnalytics_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnAnalytics.Height;
            pnlNav.Top = btnAnalytics.Top;
            btnAnalytics.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnUsers.Height;
            pnlNav.Top = btnUsers.Top;
            btnUsers.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void btnContactUs_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnContactUs.Height;
            pnlNav.Top = btnContactUs.Top;
            btnContactUs.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnSettings.Height;
            pnlNav.Top = btnSettings.Top;
            btnSettings.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void btnDashboard_Leave(object sender, EventArgs e)
        {
            btnDashboard.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnAnalytics_Leave(object sender, EventArgs e)
        {
            btnDashboard.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnUsers_Leave(object sender, EventArgs e)
        {
            btnDashboard.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnContactUs_Leave(object sender, EventArgs e)
        {
            btnDashboard.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnSettings_Leave(object sender, EventArgs e)
        {
            btnDashboard.BackColor = Color.FromArgb(24, 30, 54);
        }
    }
}
