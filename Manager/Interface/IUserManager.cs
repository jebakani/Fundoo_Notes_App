using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Interface
{
    public interface IUserManager
    {
        bool Register(RegisterModel userData);
        bool Login(string emailId, string password);
    }

}
