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
    using Microsoft.AspNetCore.Mvc;
    using Model;
    using Models;
    
    /// <summary>
    /// UserController that connect view with model
    /// </summary>
    public class UserController : ControllerBase
    {
        /// <summary>
        /// create the object for IUserManager
        /// </summary>
        private readonly IUserManager manager;

        /// <summary>
        /// Constructor to assign object of IUserManager
        /// Initializes a new instance of the <see cref="UserController"/> class
        /// </summary>
        /// <param name="manager">IUserManager manager</param>
        public UserController(IUserManager manager)
        {
            this.manager = manager;
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
            try
            {
                bool result = this.manager.Register(userData);

                if (result == true)
                {
                    ////Creates a OkResult object that produces an empty Status200OK response.
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Registration Successful" });
                }
                else
                {
                    ////Creates an BadRequestResult that produces a Status400BadRequest response.
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Registration UnSuccessful" });
                }
            }
            catch (Exception ex)
            {
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
                if (result != null)
                {
                    ////Creates a OkResult object that produces an empty Status200OK response.
                    return this.Ok(new  { Status = true, Message = "Login Successful", data = result.toString(),resultMassage});
                }
                else
                {
                    ////Creates an BadRequestResult that produces a Status400BadRequest response.
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Login UnSuccessful" });
                }
            }
            catch (Exception ex)
            {
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
                    ////Creates a OkResult object that produces an empty Status200OK response.
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Reset password successfull" });
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
    }
}
