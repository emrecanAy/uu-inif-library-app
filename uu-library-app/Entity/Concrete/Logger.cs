using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.Entity.Abstract;

namespace uu_library_app.Entity.Concrete
{
    public class Logger : IBaseModelWithIdAndDate
    {
        private string adminId;
        private string logMessage;

        public Logger() { }

        public Logger(string id, string adminId, string logMessage)
        {
            this.Id = id;
            this.AdminId = adminId;
            this.LogMessage = logMessage;
        }

        public string Id { get => id; set => id = value; }
        public string AdminId { get => adminId; set => adminId = value; }
        public string LogMessage { get => logMessage; set => logMessage = value; }
        public DateTime CreatedAt { get => createdAt; set => createdAt = value; }
    }
}
