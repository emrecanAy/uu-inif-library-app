using MessageBoxDenemesi;
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
using uu_library_app.Core.RoutinCustomJobs;
using uu_library_app.Core.Utils;
using uu_library_app.DataAccess.Concrete;
using uu_library_app.Entity.Concrete;

namespace uu_library_app.FormUI.MailSettings
{
    public partial class Mail_Operations : Form
    {
        Admin _admin;
        public Mail_Operations(Admin admin)
        {
            InitializeComponent();
            _admin = admin;
        }

        SettingsManager settingsManager = new SettingsManager(new SettingsDal());
        LoggerManager logger = new LoggerManager(new LoggerDal());

        private void btnReminding_Click(object sender, EventArgs e)
        {
            if(txtSenderPassword.Text != txtControl.Text)
            {
                wehMessageBox.Show("Parolalar uyuşmuyor!",
                "Uyarı!",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Warning);
                return;
            }

            if(txtSenderEmail.Text == "" || txtSenderPassword.Text == "" || txtControl.Text == "" || txtGecikmeBaslik.Text == "" || txtHatirlatmaBaslik.Text == "" || richGecikme.Text == "" || richHatirlatma.Text == "" || numericGecikme.Value == 0 || numericHatirlatma.Value == 0)
            {
                wehMessageBox.Show("Tüm alanları doldurun!",
                "Uyarı!",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Warning);
                return;
            }

            DialogResult dialogResult = wehMessageBox.Show("Güncellemek istediğinize emin misiniz? Email veya şifrenin yanlış girilmesi durumunda otomatik eposta sistemi aksayacaktır!",
                "Uyarı!",
                  MessageBoxButtons.YesNo,
                  MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                Settings settings = new Settings(settingsManager.getSettings().Id, txtSenderEmail.Text, StringEncoder.Encrypt(txtSenderPassword.Text), Convert.ToInt32(numericHatirlatma.Value), Convert.ToInt32(numericGecikme.Value), txtHatirlatmaBaslik.Text, richHatirlatma.Text, txtGecikmeBaslik.Text, richGecikme.Text, DateTime.Now);
                settingsManager.Update(settings);
                Logger log = new Logger(System.Guid.NewGuid().ToString(), _admin.id, "[ " + settings.Id + " | " + settings.SenderEmail + " ] " + _admin.FirstName + " " + _admin.LastName + " tarafından mail ayarları güncellendi! -Tarih: " + DateTime.Now);
                logger.Log(log);

                wehMessageBox.Show("Başarıyla güncellendi!",
                "Başarılı",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Information);
            }
            
        }

        private void Mail_Operations_Load(object sender, EventArgs e)
        {
            Settings settings = settingsManager.getSettings();
            txtSenderEmail.Text = settings.SenderEmail;
            txtSenderPassword.Text = StringEncoder.Decrypt(settings.SenderPassword);
            txtGecikmeBaslik.Text = settings.ExpiredMailHeader;
            txtHatirlatmaBaslik.Text = settings.RemindingMailHeader;
            richGecikme.Text = settings.ExpiredMailText;
            richHatirlatma.Text = settings.RemindingMailText;
            numericGecikme.Value = settings.DepositDay;
            numericHatirlatma.Value = settings.RemindingDay;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MailSenderJob job = new MailSenderJob();
            job.everyDayCheckAndSendMail();
        }
    }
}
