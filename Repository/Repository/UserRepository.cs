

namespace FundooNotes1.Repository
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;
    using System.Text;
    using Experimental.System.Messaging;
    using global::Repository.Context;
    using global::Repository.Inteface;
    using Model;
    using Models;

    //using Repository.Context;
    //using Repository.Inteface;

    public class UserRepository : IUserRepository
    {
        private readonly UserContext userContext;

        //getting user contect object through constructor
        public UserRepository(UserContext userContext)
        {
            this.userContext = userContext;
        }

        //method for register user
        public bool Register(RegisterModel userData)
        {
            try
            {
                if (userData != null)
                {
                    //encrypting the password
                    userData.Password = EncryptPassword(userData.Password);
                    //add the data to the data base using user context 
                    this.userContext.Add(userData);
                    //save the change in data base
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

        //method to encrypt the password
        public string EncryptPassword(string password)
        {
            //encodes Unicode characters into a sequence of one to four bytes per character
            var passwordInBytes = Encoding.UTF8.GetBytes(password);

            //Converts a subset of an array of 8-bit unsigned integers to its equivalent string representation that is encoded with base-64 digits
            string encodedPassword = Convert.ToBase64String(passwordInBytes);

            //returns the encoded pasword
            return encodedPassword;
        }

        //method to check login details
        public bool Login(string emailId, string password)
        {
            try
            {
                string encodePassword = EncryptPassword(password);
                //search the data base for particular email id and password . if any one is not match then return null
                var login = this.userContext.user.Where(x => x.Email == emailId && x.Password == encodePassword).FirstOrDefault();
               //if the value not equal to null then return true
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
        public bool ForgetPassword(string email)
        {
            try
            {
               var validEmail= this.userContext.user.Where(x => x.Email == email).FirstOrDefault();
               if(validEmail!=null)
               {
                    MSMQSend("Link for resetting the password");
                    return ReceiveQueue(email);
               }
               else
               {
                    return false;
               }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
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

        private void MSMQSend(string url)
        {
            MessageQueue messageQueue = QueueDetail();
            Message message = new Message();
            message.Formatter = new BinaryMessageFormatter();
            message.Body = url;
            messageQueue.Label = "url link";
            messageQueue.Send(message);
        }
            
        private bool ReceiveQueue(string email)
        {
            //for reading from MSMQ
            var receiveQueue = new MessageQueue(@".\Private$\ResetPasswordQueue");
            var receiveMsg = receiveQueue.Receive();
            receiveMsg.Formatter = new BinaryMessageFormatter();

            string linkToBeSend = receiveMsg.Body.ToString();
            if (SendMail(email, linkToBeSend))
            {
                return true;
            }
            return false;
        }

        private bool SendMail(string email,string message)
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

        public bool ResetPassword(ResetPasswordModel resetPassword)
        {
            try
            {
                if (resetPassword != null)
                {
                    var userData= this.userContext.user.Where(x => x.Email == resetPassword.EmailId).FirstOrDefault();

                    if (userData != null)
                    {
                        //encrypting the password
                        userData.Password = EncryptPassword(resetPassword.NewPassword);
                        ////add the data to the data base using user context 
                        //this.userContext.Add(userData);
                        //save the change in data base
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
    }
}
