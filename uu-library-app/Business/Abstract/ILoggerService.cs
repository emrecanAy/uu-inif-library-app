using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.DataAccess.Abstract;
using uu_library_app.Entity.Concrete;

namespace uu_library_app.Business.Abstract
{
    public interface ILoggerService
    {
        List<Logger> GetAll();
        void Log(Logger logger);

    }
}
