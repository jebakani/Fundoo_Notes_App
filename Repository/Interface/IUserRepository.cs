// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserRepository.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Jebakani Ishwarya"/>
// ----------------------------------------------------------------------------------------------------------

namespace Repository.Inteface
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Model;
    using Models;

    /// <summary>
    /// interface for the userRepository
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// method to Register the user
        /// </summary>
        /// <param name="userData">user details</param>
        /// <returns>return true or false</returns>
        string Register(RegisterModel userData);

        /// <summary>
        /// method to Register the user
        /// </summary>
        /// <param name="emailId">email id as string</param>
        /// <param name="password">password as string</param>
        /// <returns>return true or false</returns>
        RegisterModel Login(string emailId, string password);

        string GenerateToken(string email);

        /// <summary>
        /// method to Register the user
        /// </summary>
        /// <param name="emailId">email id as string</param>
        /// <returns>return true or false</returns>
        bool ForgetPassword(string emailId);

        /// <summary>
        /// method to Register the user
        /// </summary>
        /// <param name="resetPassword">email id and password</param>
        /// <returns>return true or false</returns>
        bool ResetPassword(ResetPasswordModel resetPassword);
    }
}
