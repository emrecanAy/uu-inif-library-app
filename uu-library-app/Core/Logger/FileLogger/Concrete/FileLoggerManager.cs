using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.Core.Logger.FileLogger.Abstract;

namespace uu_library_app.Core.Logger.FileLogger
{
    public class FileLoggerManager : IFileLoggerService
    {
        IFileLoggerDal _service;

        public FileLoggerManager(IFileLoggerDal service)
        {
            _service = service;
        }

        public List<FileLogger> GetAll()
        {
            return _service.GetAll();
        }

        public void Log(FileLogger fileLogger)
        {
            _service.Log(fileLogger);
        }
    }
}
