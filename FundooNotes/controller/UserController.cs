using Manager.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using Model;

namespace FundooNotes.controller
{
    public class UserController : ControllerBase
    {
        //controller uses the User manager to access the repository
        private readonly IUserManager manager;
        public UserController(IUserManager manager)
        {
            this.manager = manager;
        }

        //for register httpPost header is user that post the data
        [HttpPost]
        //it routes to the particular method when the url is matched
        [Route("api/register")]
        public IActionResult Register([FromBody]RegisterModel userData)
        {
            try
            {
                bool result=this.manager.Register(userData);
                if(result==true)
                {
                    //Creates a OkResult object that produces an empty Status200OK response.
                    return this.Ok(new ResponseModel<string>(){ status = true,message="Registration Successful" });
                }
                else
                {
                    //Creates an BadRequestResult that produces a Status400BadRequest response.
                    return this.BadRequest(new ResponseModel<string>() { status = false, message = "Registration UnSuccessful" });
                }
            }
            catch(Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { status = false, message = ex.Message });
            }
        }

        //for register httpGet header is user that get the data from the source
        [HttpGet]
        //it routes to the particular method when the url is matched
        [Route("api/login")]
        public IActionResult Login([FromBody]LoginModel loginData)
        {
            try
            {
                bool result = this.manager.Login(loginData.EmailId,loginData.Password);
                if (result == true)
                {
                    //Creates a OkResult object that produces an empty Status200OK response.
                    return this.Ok(new ResponseModel<string>() { status = true, message = "Login Successful" });
                }
                else
                {
                    //Creates an BadRequestResult that produces a Status400BadRequest response.
                    return this.BadRequest(new ResponseModel<string>() { status = false, message = "Login UnSuccessful" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { status = false, message = ex.Message });
            }
        }
        [HttpGet]
        [Route("api/forgetPassword")]
        public IActionResult ForgetPassword(string email)
        {
            try
            {
                bool result = this.manager.ForgetPassword(email);
                if (result == true)
                {
                    //Creates a OkResult object that produces an empty Status200OK response.
                    return this.Ok(new ResponseModel<string>() { status = true, message = "Reset password successfull" });
                }
                else
                {
                    //Creates an BadRequestResult that produces a Status400BadRequest response.
                    return this.BadRequest(new ResponseModel<string>() { status = false, message = "Reset password Unsuccessfull" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { status = false, message = ex.Message });
            }
        }
    }
}
