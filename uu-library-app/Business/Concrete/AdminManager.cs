using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.Business.Abstract;
using uu_library_app.Core.Utils;
using uu_library_app.DataAccess.Abstract;
using uu_library_app.Entity.Concrete;

namespace uu_library_app.Business.Concrete
{
    public class AdminManager : IAdminService
    {
        IAdminDal _service;
        public AdminManager(IAdminDal service)
        {
            _service = service;
        }

        public void Add(Admin admin, string verificationCode)
        {
            if (admin.FirstName != null || admin.LastName != null || admin.EMail != null || admin.Password != null || admin.Id != null)
            {
             _service.Add(admin);
             Console.WriteLine("Kayıt başarılı!");
            }
        }

        public void Delete(Admin admin)
        {
            if (admin.Id != null)
            {
                _service.Delete(admin.Id);
            }
        }

        public List<Admin> getAll()
        {
            return _service.getAll();
        }

        public Admin getbyEmail(string eMail)
        {
            
            return _service.getByEmail(eMail);
        }

        public void Update(Admin admin)
        {
            if (admin.FirstName != null || admin.LastName != null || admin.EMail != null || admin.Password != null || admin.Id != null)
            {
                _service.Update(admin);
            }
        }

        public Admin GetById(string id)
        {
            return _service.GetById(id);
        }

        public bool isValidSchoolMail(string email) //Utils'e koy.
        {
            string ending = email.Substring(email.IndexOf("@") + 1);
            if (ending != "ogr.uludag.edu.tr")
            {
                return false;
            }
            return true;

        }

        public bool checkIfEmailExist(string email) //email'lere regex eklenecek.
        {
            if (_service.getByEmail(email).Id != null)
            {
                return true;
            }
            return false;
        }

        public bool checkIfEmailEqualsToPassword(string eMail, string password)
        {
            Admin admin = _service.getByEmail(eMail);
            if (StringEncoder.Decrypt(admin.Password) != password)
            {
                return false;
            }
            return true;

        }
        public bool checkIfEmailVerificated(string code, string verificationCode)
        {
            if (verificationCode == code)
            {
                return true;
            }
            return false;
        }


        string code;
        public void sendEmailVerificationCode(string email, string code)
        {
            try
            {
                code = EmailVerificator.GenerateCode();
                MailSender.SendMail(email, code);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
