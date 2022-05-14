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
    public class LanguageManager : ILanguageService
    {
        ILanguageDal _service;
        public LanguageManager(ILanguageDal service)
        {
            _service = service;
        }

        public void Add(Language language)
        {
            _service.Add(language);
        }

        public void Delete(Language language)
        {
            _service.Delete(language);
        }

        public List<Language> getAll()
        {
            return _service.getAll();
        }

        public void Update(Language language)
        {
            _service.Update(language);
        }
    }
}
