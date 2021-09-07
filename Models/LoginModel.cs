// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoginModel.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Jebakani Ishwarya"/>
// ----------------------------------------------------------------------------------------------------------

namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Login model class that has properties such ha email id and password for login
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// Gets or sets the Email id 
        /// </summary>
        [Required]
        [RegularExpression(@"^[a-zA-Z]{3}([\- \+ _\.]*[a-zA-Z0-9]+)*@[a-zA-Z0-9]+\.[a-z]{2,3}(\.[a-zA-Z]{2,4}){0,1}$", ErrorMessage = "Invalid Email Id")]
        public string EmailId { get; set; }
        
        /// <summary>
        /// Gets or sets the password
        /// </summary>
        [Required]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?!.*[<>`])(?=[^.,:;'!@#$%^&*_+=|(){}[?\-\]\/\\]*[.,:;'!@#$%^&*_+=|(){}[?\-\]\/\\][^.,:;'!@#$%^&*_+=|(){}[?\-\]\/\\]*$).{8,}$", ErrorMessage = "Invalid Password")]
        public string Password { get; set; }
    }
}
