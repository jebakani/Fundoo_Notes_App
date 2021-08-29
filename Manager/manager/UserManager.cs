using Repository.Inteface;
using Manager.Interface;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.manager
{
    public class UserManager:IUserManager
    {
        private readonly IUserRepository repository;
        public UserManager(IUserRepository repository)
        {
            this.repository = repository;
        }

        public bool Login(string emailId, string password)
        {
            try
            {
                return this.repository.Login(emailId,password);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Register(RegisterModel userData)
        {
            try
            {
                return this.repository.Register(userData);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
