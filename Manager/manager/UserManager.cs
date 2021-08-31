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
    //user manager access the repository and get the data from the repository
    public class UserManager:IUserManager
    {
        private readonly IUserRepository repository;
        public UserManager(IUserRepository repository)
        {
            this.repository = repository;
        }

        public bool ForgetPassword(string emailId)
        {
            try
            {
                return this.repository.ForgetPassword(emailId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //it passess the login details and returns the result
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

        //register passess the user data to the repository
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
