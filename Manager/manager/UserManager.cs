// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserManager.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Jebakani Ishwarya"/>
// ----------------------------------------------------------------------------------------------------------

namespace Manager.Manager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using global::Manager.Interface;
    using Model;
    using Models;
    using Repository.Inteface;

    /// <summary>
    /// user manager access the repository and get the data from the repository
    /// </summary>
    public class UserManager : IUserManager
    {
        /// <summary>
        /// Declaring object for the IUserRepository
        /// </summary>
        private readonly IUserRepository repository;

        /// <summary>
        /// constructor for user manager
        /// Initializes a new instance of the <see cref="UserManager"/> class
        /// </summary>
        /// <param name="repository">repository of type IUserRepository</param>
        public UserManager(IUserRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// method to manage Forget password
        /// </summary>
        /// <param name="emailId">emailId of type string</param>
        /// <returns>returns whether success or failure</returns>
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

        /// <summary>
        /// it pass the login details and returns the result
        /// </summary>
        /// <param name="emailId"> emailId of type string</param>
        /// <param name="password">password of type string</param>
        /// <returns>returns whether success or failed</returns>
        public string Login(string emailId, string password)
        {
            try
            {
                return this.repository.Login(emailId, password);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Declaring of Generate token method
        /// </summary>
        /// <param name="email">email of user as string</param>
        /// <returns>return the JWT token</returns>
        public string GenerateToken(string email)
        {
            try
            {
                return this.repository.GenerateToken(email);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// register pass the user data to the repository
        /// </summary>
        /// <param name="userData">user data contains the user details</param>
        /// <returns>return true or false </returns>
        public string Register(RegisterModel userData)
        {
            try
            {
                return this.repository.Register(userData);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Method to reset password 
        /// </summary>
        /// <param name="resetPassword">reset model data</param>
        /// <returns>returns the result in boolean</returns>
        public bool ResetPassword(ResetPasswordModel resetPassword)
        {
            try
            {
                return this.repository.ResetPassword(resetPassword);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
