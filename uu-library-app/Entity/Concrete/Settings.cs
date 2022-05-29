using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.Entity.Abstract;

namespace uu_library_app.Entity.Concrete
{
    public class Settings : IBaseModelWithIdAndDate
    {
        private string senderEmail;
        private string senderPassword;
        private int remindingDay;
        private int depositDay;
        private string remindingMailHeader;
        private string remindingMailText;
        private string expiredMailHeader;
        private string expiredMailText;
        private DateTime updatedAt;

        public Settings() { }
        public Settings(string id, string senderEmail, string senderPassword, int remindingDay, int depositDay, string remindingMailHeader, string remindingMailText, string expiredMailHeader, string expiredMailText, DateTime updatedAt)
        {
            this.id = id;
            this.SenderEmail = senderEmail;
            this.SenderPassword = senderPassword;
            this.RemindingDay = remindingDay;
            this.DepositDay = depositDay;
            this.RemindingMailHeader = remindingMailHeader;
            this.RemindingMailText = remindingMailText;
            this.ExpiredMailHeader = expiredMailHeader;
            this.ExpiredMailText = expiredMailText;
            this.UpdatedAt = updatedAt;
        }

        public string Id { get => id; set => id = value; }
        public string SenderEmail { get => senderEmail; set => senderEmail = value; }
        public string SenderPassword { get => senderPassword; set => senderPassword = value; }
        public int RemindingDay { get => remindingDay; set => remindingDay = value; }
        public int DepositDay { get => depositDay; set => depositDay = value; }
        public string RemindingMailHeader { get => remindingMailHeader; set => remindingMailHeader = value; }
        public string RemindingMailText { get => remindingMailText; set => remindingMailText = value; }
        public string ExpiredMailHeader { get => expiredMailHeader; set => expiredMailHeader = value; }
        public string ExpiredMailText { get => expiredMailText; set => expiredMailText = value; }
        public DateTime UpdatedAt { get => updatedAt; set => updatedAt = value; }
    }
}
