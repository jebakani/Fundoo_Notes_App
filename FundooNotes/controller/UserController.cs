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
        private readonly IUserManager manager;
        public UserController(IUserManager manager)
        {
            this.manager = manager;
        }
        [HttpPost]
        [Route("api/register")]
        public IActionResult Register([FromBody]RegisterModel userData)
        {
            try
            {
                bool result=this.manager.Register(userData);
                if(result==true)
                {
                    return this.Ok(new ResponseModel<string>(){ status = true,message="Registration Successful" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { status = false, message = "Registration UnSuccessful" });
                }
            }
            catch(Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { status = false, message = ex.Message });
            }
        }
        [HttpGet]
        [Route("api/login")]
        public IActionResult Login([FromBody]LoginModel loginData)
        {
            try
            {
                bool result = this.manager.Login(loginData.EmailId,loginData.passWord);
                if (result == true)
                {
                    return this.Ok(new ResponseModel<string>() { status = true, message = "Login Successful" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { status = false, message = "Login UnSuccessful" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { status = false, message = ex.Message });
            }
        }
    }
}
