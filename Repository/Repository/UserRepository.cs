// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserRepository.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Jebakani Ishwarya"/>
// ----------------------------------------------------------------------------------------------------------

namespace FundooNotes1.Repository
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;
    using System.Security.Claims;
    using System.Text;
    using Experimental.System.Messaging;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using Model;
    using Models;
    using global::Repository.Context;
    using global::Repository.Inteface;
    using StackExchange.Redis;

    /// <summary>
    /// User repository class that execute the query and connect with database
    /// </summary>
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// create the object for user context
        /// </summary>
        private readonly UserContext userContext;

        /// <summary>
        /// getting user context object through constructor
        /// Initializes a new instance of the <see cref="UserRepository"/> class
        /// </summary>
        /// <param name="userContext">user context object that has connection with database Context</param>
        /// <param name="configuration">configuration object to access the app setting file</param>
        public UserRepository(UserContext userContext, IConfiguration configuration)
        {
            this.userContext = userContext;
            this.Configuration = configuration;
        } 
        
        /// <summary>
        /// Gets method to get Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// method for register user
        /// </summary>
        /// <param name="userData">User data is passed as parameter</param>
        /// <returns>whether the data send or not</returns>
        public string Register(RegisterModel userData)
        {
            try
            {
                var validEmail = this.userContext.User.Where(x => x.Email == userData.Email).FirstOrDefault();
                if (validEmail == null)
                {
                    if (userData != null)
                    {
                        //// encrypting the password
                        userData.Password = this.EncryptPassword(userData.Password);
                        //// add the data to the data base using user context 
                        this.userContext.Add(userData);
                        //// save the change in data base
                        this.userContext.SaveChanges();
                        return "Registration Successful";
                    }

                    return "Registration UnSuccessful";
                }

                return "Email Id Already Exists";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method to encrypt the password
        /// </summary>
        /// <param name="password">pass password that has to be encrypted</param>
        /// <returns>encrypted password</returns>
        public string EncryptPassword(string password)
        {
            ////encodes Unicode characters into a sequence of one to four bytes per character
            var passwordInBytes = Encoding.UTF8.GetBytes(password);

            ////Converts a subset of an array of 8-bit unsigned integers to its equivalent string representation that is encoded with base-64 digits
            string encodedPassword = Convert.ToBase64String(passwordInBytes);

            ////returns the encoded pasword
            return encodedPassword;
        }

        /// <summary>
        /// Declaring of Generate token method
        /// </summary>
        /// <param name="email">email of user as string</param>
        /// <returns>return the JWT token</returns>
        public string GenerateToken(string email)
        {
            var key = Encoding.UTF8.GetBytes(this.Configuration["SecretKey"]);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                      new Claim(ClaimTypes.Name, email)
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }

        /// <summary>
        /// method to check login details
        /// </summary>
        /// <param name="emailId">email id of user in string</param>
        /// <param name="password">password of user in string</param>
        /// <returns>Login success or not</returns>
        public string Login(string emailId, string password)
        {
            try
            {
                string encodePassword = this.EncryptPassword(password);
                ////search the data base for particular email id and password . if any one is not match then return null
                var login = this.userContext.User.Where(x => x.Email == emailId && x.Password == encodePassword).FirstOrDefault();
                ////if the value not equal to null then return true
                if (login != null)
                {
                    ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
                    IDatabase database = connectionMultiplexer.GetDatabase();
                    database.StringSet(key: "FirstName", login.FirstName);
                    database.StringSet(key: "LastName", login.LastName);
                    database.StringSet(key: "UserId", login.id.ToString());
                    return "Login sucessful";
                }
                else
                {
                    return "Login fail";
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// method to implement forget password
        /// </summary>
        /// <param name="email">email as string</param>
        /// <returns>send mail to user</returns>
        public bool ForgetPassword(string email)
        {
            try
            {
               var validEmail = this.userContext.User.Where(x => x.Email == email).FirstOrDefault();
               if (validEmail != null)
               {
                    this.MSMQSend("Link for resetting the password");
                    return this.SendEmail(email);
               }
               else
               {
                    return false;
               }
            } 
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// method to reset the password
        /// </summary>
        /// <param name="resetPassword">password and mail id</param>
        /// <returns>return the result in boolean</returns>
        public bool ResetPassword(ResetPasswordModel resetPassword)
        {
            try
            {
                if (resetPassword != null)
                {
                    var userData = this.userContext.User.Where(x => x.Email == resetPassword.EmailId).FirstOrDefault();

                    if (userData != null)
                    {
                        ////encrypting the password
                        userData.Password = this.EncryptPassword(resetPassword.NewPassword);
                        ////save the change in data base
                        this.userContext.SaveChanges();
                        return true;
                    }

                    return false;
                }

                return false;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method to send the email by receiving the message from queue
        /// </summary>
        /// <param name="email">email id of receiver</param>
        /// <returns>boolean value as true or false</returns>
        private bool SendEmail(string email)
        {
            string linkToBeSend = this.ReceiveQueue(email);
            if (this.SendMailUsingSMTP(email, linkToBeSend))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// method to create new queue for message or get the queue if already exists
        /// </summary>
        /// <returns>the queue</returns>
        private MessageQueue QueueDetail()
        {
            MessageQueue messageQueue;
            if (MessageQueue.Exists(@".\Private$\ResetPasswordQueue"))
            {
                messageQueue = new MessageQueue(@".\Private$\ResetPasswordQueue");
            }
            else
            {
                messageQueue = MessageQueue.Create(@".\Private$\ResetPasswordQueue");
            }

            return messageQueue;
        }

        /// <summary>
        /// method send the message in the queue
        /// </summary>
        /// <param name="url">url link that has to be send</param>
        private void MSMQSend(string url)
        {
            try
            {
                MessageQueue messageQueue = this.QueueDetail();
                Message message = new Message();
                message.Formatter = new BinaryMessageFormatter();
                message.Body = url;
                messageQueue.Label = "url link";
                messageQueue.Send(message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
        /// <summary>
        /// method to get the message from the queue and send it to the mail
        /// </summary>
        /// <param name="email">email id of user to send mail</param>
        /// <returns>returns whether the mail is send or not</returns>
        private string ReceiveQueue(string email)
        {
            ////for reading from MSMQ
            var receiveQueue = new MessageQueue(@".\Private$\ResetPasswordQueue");
            var receiveMsg = receiveQueue.Receive();
            receiveMsg.Formatter = new BinaryMessageFormatter();

            string linkToBeSend = receiveMsg.Body.ToString();
            return linkToBeSend;
        }

        /// <summary>
        /// method to send the mail
        /// </summary>
        /// <param name="email">email as string</param>
        /// <param name="message">message can be string or url or combination of both</param>
        /// <returns>returns the result to receive queue method</returns>
        private bool SendMailUsingSMTP(string email, string message)
        {
            MailMessage mailMessage = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            mailMessage.From = new MailAddress("17cse12jebakaniishwaryav@gmail.com");
            mailMessage.To.Add(new MailAddress(email));
            mailMessage.Subject = "Link to reset you password for fundoo Application";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = message;
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("17cse12jebakaniishwaryav@gmail.com", "Jebakani2000");
            smtp.Send(mailMessage);
            return true;
        }    
    }
}
