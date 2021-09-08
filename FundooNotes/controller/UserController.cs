// --------------------------------------------------------------------------------------------------------------------
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
    using Microsoft.AspNetCore.Http;
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
        /// Declare the object for IUserManager
        /// </summary>
        private readonly IUserManager manager;

        const string SessionName = "UserName";
        const string SessionMail = "EmailId";
        /// <summary>
        /// Declare object for ILogger
        /// </summary>
        private readonly ILogger<UserController> logger;

        /// <summary>
        /// Constructor to assign object of IUserManager
        /// Initializes a new instance of the <see cref="UserController"/> class
        /// </summary>
        /// <param name="manager">IUserManager manager</param>
        /// <param name="logger">ILogger logger</param>
        public UserController(IUserManager manager, ILogger<UserController> logger)
        {
            this.manager = manager;
            this.logger = logger;
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
            this.logger.LogInformation("Registration of new user initialized");
            HttpContext.Session.SetString(SessionName, userData.FirstName+" "+userData.LastName);
            HttpContext.Session.SetString(SessionMail, userData.Email);
            try
            {
                string result = this.manager.Register(userData);
       
                if (result.Equals("Registration Successful"))
                {
                    var name = HttpContext.Session.GetString(SessionName);
                    var email = HttpContext.Session.GetString(SessionMail);
                    ////Creates a OkResult object that produces an empty Status200OK response.
                    this.logger.LogInformation(userData.FirstName + " is registered");
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result ,Data =" Session detailes :"+name+" "+email});
                }
                else
                {
                    this.logger.LogError("Registration unsuccessfull");
                    ////Creates an BadRequestResult that produces a Status400BadRequest response.
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError("Error occurs:" + ex.Message);
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
                var result = this.manager.Login(loginData.EmailId, loginData.Password);
                string resultMassage = this.manager.GenerateToken(loginData.EmailId);
                if (result.Equals("Login sucessful"))
                {                    
                    ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
                    IDatabase database = connectionMultiplexer.GetDatabase();
                    string firstName = database.StringGet("FirstName");
                    string lastName = database.StringGet("LastName");
                    int userId = Convert.ToInt32(database.StringGet("UserID"));
                    this.logger.LogInformation(firstName + " is loggedIn");
                    RegisterModel userData = new RegisterModel
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        id = userId,
                        Email = loginData.EmailId
                    };
                    ////Creates a OkResult object that produces an empty Status200OK response.
                    return this.Ok(new  
                    { 
                        Status = true, 
                        Message = "Login Successful", 
                        data = userData, resultMassage
                    });
                }
                else
                {
                    this.logger.LogWarning(loginData.EmailId + " :Login failed ");
                   
                    ////Creates an BadRequestResult that produces a Status400BadRequest response.
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Login UnSuccessful" });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError("Error occurs:" + ex.Message);
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
            this.logger.LogInformation("Forget password is initialized");
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
                    this.logger.LogInformation("password resetted");
                    ////Creates a OkResult object that produces an empty Status200OK response.
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Reset password successfull" });
                }
                else
                {
                    this.logger.LogWarning("password reset fail");
                    ////Creates an BadRequestResult that produces a Status400BadRequest response.
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Reset password Unsuccessfull" });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError("Error:" + ex.Message);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}
