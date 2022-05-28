using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.Entity.Concrete;

namespace uu_library_app.Business.Abstract
{
    public interface IFacultyService
    {
        List<Faculty> GetAll();
        void Add(Faculty faculty);
        void Update(Faculty faculty);
        void Delete(Faculty faculty);
    }
}
