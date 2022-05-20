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
            if(language.LanguageName != null && language.LanguageName.Length > 0)
            {
                _service.Add(language);
            }
            
        }

        public void Delete(string id)
        {
            if(id != null)
            {
                _service.Delete(id);
            }
            
        }

        public List<Language> getAll()
        {
            return _service.getAll();
        }

        public void Update(Language language)
        {
            if (language.LanguageName != null && language.LanguageName.Length > 0)
            {
                _service.Update(language);
            }
        }
    }
}
