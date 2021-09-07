﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserController.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Jebakani Ishwarya"/>
// ----------------------------------------------------------------------------------------------------------

namespace FundooNotes.Controller
{  
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Manager.Interface;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Model;
    using Models;
    using StackExchange.Redis;

    /// <summary>
    /// UserController that connect view with model
    /// </summary>
    public class UserController : ControllerBase
    {
        /// <summary>
        /// create the object for IUserManager
        /// </summary>
        private readonly IUserManager manager;
        private readonly ILogger<UserController> _logger;
        /// <summary>
        /// Constructor to assign object of IUserManager
        /// Initializes a new instance of the <see cref="UserController"/> class
        /// </summary>
        /// <param name="manager">IUserManager manager</param>
        public UserController(IUserManager manager, ILogger<UserController> logger)
        {
            this.manager = manager;
            this._logger = logger;
        }

        /// <summary>
        /// controller API for the forget password
        /// </summary>
        /// <param name="userData">User data contains user details</param>
        /// <returns>result of the action</returns>
        [HttpPost]
        [Route("api/register")]
        public IActionResult Register([FromBody]RegisterModel userData)
        {
            _logger.LogInformation("Registration of new user initialized");
            try
            {
                string result = this.manager.Register(userData);

                if (result.Equals("Registration Successful"))
                {
                    ////Creates a OkResult object that produces an empty Status200OK response.
                    _logger.LogInformation(""+userData.FirstName+" is registered");
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result});
                }
                else
                {
                    _logger.LogError("Registration unsuccessfull");
                    ////Creates an BadRequestResult that produces a Status400BadRequest response.
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurs:" + ex.Message);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// controller API for the forget password
        /// </summary>
        /// <param name="loginData">Login data contains emailId and password</param>
        /// <returns>result of the action</returns>
        [HttpPost]
        [Route("api/login")]
        public IActionResult Login([FromBody]LoginModel loginData)
        {
            try
            {
                var result =this.manager.Login(loginData.EmailId, loginData.Password);
                string resultMassage = this.manager.GenerateToken(loginData.EmailId);
                if (result.Equals("Login sucessful"))
                {
                    
                    ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
                    IDatabase database = connectionMultiplexer.GetDatabase();
                    string FirstName = database.StringGet("FirstName");
                    string LastName = database.StringGet("LastName");
                    int UserId = Convert.ToInt32(database.StringGet("UserID"));
                    _logger.LogInformation("" + FirstName + " is loggedIn");
                    RegisterModel userData = new RegisterModel
                    {
                        FirstName = FirstName,
                        LastName = LastName,
                        id = UserId,
                        Email = loginData.EmailId

                    };
                    ////Creates a OkResult object that produces an empty Status200OK response.
                    return this.Ok(new  { Status = true, Message = "Login Successful", data = userData, resultMassage});
                }
                else
                {
                    _logger.LogWarning(loginData.EmailId+" :Login failed ");
                   
                    ////Creates an BadRequestResult that produces a Status400BadRequest response.
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Login UnSuccessful" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurs:" + ex.Message);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// controller API for the forget password
        /// </summary>
        /// <param name="email">gives the user email id</param>
        /// <returns>returns the result whether the action is success or fail</returns>
        [HttpPost]
        [Route("api/forgetPassword")]
        public IActionResult ForgetPassword(string email)
        {
            _logger.LogInformation("Forget password is initialized");
            try
            {
                bool result = this.manager.ForgetPassword(email);
                if (result == true)
                {
                    ////Creates a OkResult object that produces an empty Status200OK response.
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Link to reset password is send to mail" });
                }
                else
                {
                    ////Creates an BadRequestResult that produces a Status400BadRequest response.
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Reset password Unsuccessfull" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Controller method to reset the password
        /// </summary>
        /// <param name="resetPassword">reset model data a</param>
        /// <returns>returns the status such as success or fail</returns>
        [HttpPut]
        [Route("api/resetPassword")]
        public IActionResult ResetPassword([FromBody]ResetPasswordModel resetPassword)
        {
            try
            {
                bool result = this.manager.ResetPassword(resetPassword);
                if (result == true)
                {
                    _logger.LogInformation("password resetted");
                    ////Creates a OkResult object that produces an empty Status200OK response.
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Reset password successfull" });
                }
                else
                {
                    _logger.LogWarning("password reset fail");
                    ////Creates an BadRequestResult that produces a Status400BadRequest response.
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Reset password Unsuccessfull" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error:"+ex.Message);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}
