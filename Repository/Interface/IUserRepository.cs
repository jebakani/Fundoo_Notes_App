using Model;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Inteface
{
    //interface for the userRepository
    public interface IUserRepository
    {
        bool Register(RegisterModel userData);
        bool Login(string emailId, string password);
        bool ForgetPassword(string emailId);

        bool ResetPassword(ResetPasswordModel resetPassword);
    }
}
