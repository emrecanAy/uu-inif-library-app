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
    public class PersonnelManager : IPersonnelService
    {
        IPersonnelDal _service;

        public PersonnelManager(IPersonnelDal service)
        {
            _service = service;
        }

        public void Add(Personnel personnel)
        {
            _service.Add(personnel);
        }

        public void Delete(Personnel personnel)
        {
            _service.Delete(personnel);
        }

        public Personnel findByEmail(string email)
        {
            return _service.findByEmail(email);
        }

        public Personnel findByName(string name)
        {
            return _service.findByName(name);

        }

        public List<Personnel> getAll()
        {
            return _service.getAll();
        }

        public Personnel getById(string id)
        {
            return _service.getById(id);
        }

        public void Update(Personnel personnel)
        {
            _service.Update(personnel);
        }
    }
}
