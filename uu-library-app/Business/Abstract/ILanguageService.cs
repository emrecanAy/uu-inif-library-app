using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.Entity.Concrete;

namespace uu_library_app.Business.Abstract
{
    public interface ILanguageService
    {
        List<Language> getAll();
        void Add(Language student);
        void Update(Language student);
        void Delete(Language student);
    }
}
