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
using uu_library_app.DataAccess.Concrete;
using uu_library_app.Entity.Concrete;

namespace uu_library_app.FormUI.Other_Operations
{
    public partial class settings_test : Form
    {
        public settings_test()
        {
            InitializeComponent();
        }

        SettingsManager settingsManager = new SettingsManager(new SettingsDal());

        private void button1_Click(object sender, EventArgs e)
        {
            
        //    Settings settings = new Settings(settingsManager.getSettings().Id, txtSenderPassword.Text, Convert.ToInt32(numericRemindingDay.Value), Convert.ToInt32(numericDepositDay.Value), txtRemindingMailHeader.Text, richRemindMailText.Text, txtExpiredMailHeader.Text, richExpiredMailText.Text);
        //    settingsManager.Update(settings);
        }
    }
}
