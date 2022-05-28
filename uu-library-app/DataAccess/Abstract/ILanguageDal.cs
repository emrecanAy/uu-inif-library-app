using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.Entity.Concrete;

namespace uu_library_app.DataAccess.Abstract
{
    public interface ILanguageDal
    {
        List<Language> getAll();
        void Add(Language language);
        void Update(Language language);
        void Delete(Language language);
    }
}
