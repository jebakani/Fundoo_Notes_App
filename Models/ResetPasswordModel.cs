// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResetPasswordModel.cs" company="Bridgelabz">
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
    /// Reset password model that contain email id and new password
    /// </summary>
    public class ResetPasswordModel
    {
        /// <summary>
        /// Gets or sets the  Email as string
        /// </summary>
        [Required]
        [RegularExpression(@"^[a-zA-Z]{3}([\- \+ _\.]*[a-zA-Z0-9]+)*@[a-zA-Z0-9]+\.[a-z]{2,3}(\.[a-zA-Z]{2,4}){0,1}$", ErrorMessage = "Invalid Email Id")]
        public string EmailId { get; set; }

        /// <summary>
        /// Gets or sets the  Password as string
        /// </summary>
        [Required]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?!.*[<>`])(?=[^.,:;'!@#$%^&*_+=|(){}[?\-\]\/\\]*[.,:;'!@#$%^&*_+=|(){}[?\-\]\/\\][^.,:;'!@#$%^&*_+=|(){}[?\-\]\/\\]*$).{8,}$", ErrorMessage = "Invalid Password")]
       public string NewPassword { get; set; }
    }
}
