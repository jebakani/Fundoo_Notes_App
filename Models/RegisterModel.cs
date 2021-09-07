// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegisterModel.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Jebakani Ishwarya"/>
// ----------------------------------------------------------------------------------------------------------

namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// register model contains the user details
    /// </summary>
    public class RegisterModel
    {
        /// <summary>
        /// Gets or sets the  id which is primary key
        /// </summary>
        [Key]
        public int id { get; set; }

        /// <summary>
        /// Gets or sets the  FirstName as string
        /// </summary>
        [Required]
        [RegularExpression(@"^[A-Z][a-z]{2}[a-z]*$", ErrorMessage = "First name is invalid")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the  LastName as string
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the  Email as string
        /// </summary>
        [Required]
        [RegularExpression(@"^[a-zA-Z]{3}([\- \+ _\.]*[a-zA-Z0-9]+)*@[a-zA-Z0-9]+\.[a-z]{2,3}(\.[a-zA-Z]{2,4}){0,1}$", ErrorMessage = "Invalid Email Id")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the  Password as string
        /// </summary>
        [Required]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?!.*[<>`])(?=[^.,:;'!@#$%^&*_+=|(){}[?\-\]\/\\]*[.,:;'!@#$%^&*_+=|(){}[?\-\]\/\\][^.,:;'!@#$%^&*_+=|(){}[?\-\]\/\\]*$).{8,}$", ErrorMessage = "Invalid Password")]
        public string Password { get; set; }
    }
}
