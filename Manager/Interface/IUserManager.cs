// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserManager.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Jebakani Ishwarya"/>
// ----------------------------------------------------------------------------------------------------------

namespace Manager.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Model;
    using Models;

    /// <summary>
    /// IUserManager Interface that contains the declaration for the UserManager method
    /// </summary>
    public interface IUserManager
    {
        /// <summary>
        /// Declaration of Register methods
        /// </summary>
        /// <param name="userData"> User details</param>
        /// <returns>returns true or false</returns>
        string Register(RegisterModel userData);

        /// <summary>
        /// Declaration of Login methods
        /// </summary>
        /// <param name="emailId">email id of type string</param>
        /// <param name="password">password of type string</param>
        /// <returns>returns true or false</returns>
        RegisterModel Login(string emailId, string password);

        string GenerateToken(string email);

        /// <summary>
        /// Declaration of Register methods
        /// </summary>
        /// <param name="emailId">email id of type string</param>
        /// <returns>returns true or false</returns>
        bool ForgetPassword(string emailId);

        /// <summary>
        /// Reset password method
        /// </summary>
        /// <param name="resetPassword">email id and password</param>
        /// <returns>the result in boolean</returns>
        bool ResetPassword(ResetPasswordModel resetPassword);
    }
}
