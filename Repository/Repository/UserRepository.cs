// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserRepository.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Jebakani Ishwarya"/>
// ----------------------------------------------------------------------------------------------------------

namespace FundooNotes1.Repository
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;
    using System.Text;
    using Experimental.System.Messaging;
    using Model;
    using Models;
    using global::Repository.Context;
    using global::Repository.Inteface;

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
        public UserRepository(UserContext userContext)
        {
            this.userContext = userContext;
        }

        /// <summary>
        /// method for register user
        /// </summary>
        /// <param name="userData">User data is passed as parameter</param>
        /// <returns>whether the data send or not</returns>
        public bool Register(RegisterModel userData)
        {
            try
            {
                if (userData != null)
                {
                    //// encrypting the password
                    userData.Password = this.EncryptPassword(userData.Password);
                    //// add the data to the data base using user context 
                    this.userContext.Add(userData);
                    //// save the change in data base
                    this.userContext.SaveChanges();
                    return true;
                }

                return false;
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
        /// method to check login details
        /// </summary>
        /// <param name="emailId">email id of user in string</param>
        /// <param name="password">password of user in string</param>
        /// <returns>Login success or not</returns>
        public bool Login(string emailId, string password)
        {
            try
            {
                string encodePassword = this.EncryptPassword(password);
                ////search the data base for particular email id and password . if any one is not match then return null
                var login = this.userContext.user.Where(x => x.Email == emailId && x.Password == encodePassword).FirstOrDefault();
                ////if the value not equal to null then return true
                if (login != null)
                {
                    return true;
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
        /// method to implement forget password
        /// </summary>
        /// <param name="email">email as string</param>
        /// <returns>send mail to user</returns>
        public bool ForgetPassword(string email)
        {
            try
            {
               var validEmail = this.userContext.user.Where(x => x.Email == email).FirstOrDefault();
               if (validEmail != null)
               {
                    this.MSMQSend("Link for resetting the password");
                    return this.ReceiveQueue(email);
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
                    var userData = this.userContext.user.Where(x => x.Email == resetPassword.EmailId).FirstOrDefault();

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
            MessageQueue messageQueue = this.QueueDetail();
            Message message = new Message();
            message.Formatter = new BinaryMessageFormatter();
            message.Body = url;
            messageQueue.Label = "url link";
            messageQueue.Send(message);
        }
        
        /// <summary>
        /// method to get the message from the queue and send it to the mail
        /// </summary>
        /// <param name="email">email id of user to send mail</param>
        /// <returns>returns whether the mail is send or not</returns>
        private bool ReceiveQueue(string email)
        {
            ////for reading from MSMQ
            var receiveQueue = new MessageQueue(@".\Private$\ResetPasswordQueue");
            var receiveMsg = receiveQueue.Receive();
            receiveMsg.Formatter = new BinaryMessageFormatter();

            string linkToBeSend = receiveMsg.Body.ToString();
            if (this.SendMail(email, linkToBeSend))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// method to send the mail
        /// </summary>
        /// <param name="email">email as string</param>
        /// <param name="message">message can be string or url or combination of both</param>
        /// <returns>returns the result to receive queue method</returns>
        private bool SendMail(string email, string message)
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
