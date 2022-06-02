using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.Business.Abstract;
using uu_library_app.DataAccess.Abstract;
using uu_library_app.DataAccess.Concrete;
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

        public void Delete(Category category)
        {
            if(checkIfExistInBooks(category.Id))
            {
                throw new Exception("Bu kategori; kitaplarda bulunan kitaba veya kitaplara ait olduğu için öncelikle kitaplara giderek bu kategorinin ait olduğu kitabı veya kitapları silmeniz gerekmektedir!");
            }
            else
            {
                _service.Delete(category);
            }
        }

        public List<Category> getAll()
        {
            return _service.getAll();
        }

        public void Update(Category category)
        {
            _service.Update(category);
        }

        public bool checkIfExistInBooks(string categoryId)
        {
            BookManager bookManager = new BookManager(new BookDal());
            if(bookManager.getByCategoryId(categoryId) != null) {
                return true;
            }
            return false;
        }

    }
}
