using Model;
using Repository.Context;
using Repository.Inteface;
using System;
using System.Linq;
using System.Text;

namespace FundooNotes1.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext userContext;

        //getting user contect object through constructor
        public UserRepository(UserContext userContext)
        {
            this.userContext = userContext;
        }
        //method for register user
        public bool Register(RegisterModel userData)
        {
            try
            {
                if (userData != null)
                {
                    //encrypting the password
                    userData.password = EncryptPassword(userData.password);
                    //add the data to the data base using user context 
                    this.userContext.Add(userData);
                    //save the change in data base
                    this.userContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //method to encrypt the password
        public string EncryptPassword(string password)
        {
            //encodes Unicode characters into a sequence of one to four bytes per character
            var passwordInBytes = Encoding.UTF8.GetBytes(password);

            //Converts a subset of an array of 8-bit unsigned integers to its equivalent string representation that is encoded with base-64 digits
            string encodedPassword = Convert.ToBase64String(passwordInBytes);

            //returns the encoded pasword
            return encodedPassword;
        }

        //method to check login details
        public bool Login(string emailId, string password)
        {
            try
            {
                string encodePassword = EncryptPassword(password);
                //search the data base for particular email id and password . if any one is not match then return null
                var login = this.userContext.user.Where(x => x.email == emailId && x.password == encodePassword).FirstOrDefault();
               //if the value not equal to null then return true
                if (login != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
