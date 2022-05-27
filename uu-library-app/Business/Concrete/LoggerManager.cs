using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.Business.Abstract;
using uu_library_app.DataAccess.Abstract;
using uu_library_app.Entity.Concrete;

namespace uu_library_app.Business.Concrete
{
    public class LoggerManager : ILoggerService
    {
        ILoggerDal _loggerDal;

        public LoggerManager(ILoggerDal loggerDal)
        {
            _loggerDal = loggerDal;
        }

        public List<Logger> GetAll()
        {
            return _loggerDal.GetAll();
        }

        public void Log(Logger logger)
        {
            _loggerDal.Log(logger);
        }
    }
}
