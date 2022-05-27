using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.Entity.Concrete;

namespace uu_library_app.DataAccess.Abstract
{
    public interface ILoggerDal
    {
        void Log(Logger logger);
        List<Logger> GetAll(); 
    }
}
