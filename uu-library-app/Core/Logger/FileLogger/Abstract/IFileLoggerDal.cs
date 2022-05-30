using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uu_library_app.Core.Logger.FileLogger.Abstract
{
    public interface IFileLoggerDal
    {
        void Log(FileLogger fileLogger);
        List<FileLogger> GetAll();
    }
}
