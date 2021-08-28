using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Inteface
{
    public interface IUserRepository
    {
        bool Register(RegisterModel userData);
    }
}
