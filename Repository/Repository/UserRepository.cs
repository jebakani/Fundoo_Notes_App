using Repository.Inteface;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repository.Context;
using System.Text;

namespace FundooNotes1.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext userContext;
        public UserRepository(UserContext userContext)
        {
            this.userContext = userContext;
        }
        public bool Register(RegisterModel userData)
        {
            try
            {
                if(userData!=null)
                {
                    //encrypting the password
                    userData.password = EncryptPassword(userData.password);
                    this.userContext.Add(userData);
                    this.userContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch(ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //method to encrypt the password
        public string EncryptPassword(string password)
        {
            var passwordInBytes = Encoding.UTF8.GetBytes(password);
            string encodedPassword = Convert.ToBase64String(passwordInBytes);
            return encodedPassword;
        }
    }
}
