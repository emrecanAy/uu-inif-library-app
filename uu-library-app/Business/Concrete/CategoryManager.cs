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
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _service;

        public CategoryManager(ICategoryDal service)
        {
            _service = service;
        }

        public void Add(Category category)
        {
            if (category.Name != null )
            {
                _service.Add(category);
            };
        }

        public void Delete(string id)
        {
            _service.Delete(id);
        }

        public List<Category> getAll()
        {
            return _service.getAll();
        }

        public void Update(Category category)
        {
            _service.Update(category);
        }
    }
}
