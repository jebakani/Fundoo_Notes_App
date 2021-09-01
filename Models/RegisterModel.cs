using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Model
{
    //register model contains the user details
    //it acts as the data transfer medium
    public class RegisterModel
    {
        [Key]
        public int id { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z][a-z]{2}[a-z]*$",ErrorMessage ="First name is invalid")]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]{3}([\- \+ _\.]*[a-zA-Z0-9]+)*@[a-zA-Z0-9]+\.[a-z]{2,3}(\.[a-zA-Z]{2,4}){0,1}$", ErrorMessage = "Invalid Email Id")]
        public string Email { get; set; }
        
        [Required]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?!.*[<>`])(?=[^.,:;'!@#$%^&*_+=|(){}[?\-\]\/\\]*[.,:;'!@#$%^&*_+=|(){}[?\-\]\/\\][^.,:;'!@#$%^&*_+=|(){}[?\-\]\/\\]*$).{8,}$", ErrorMessage = "Invalid Password")]
        public string Password { get; set; }

        public string toString()
        {
            return "UserName:" + this.FirstName + " " + this.LastName + " EmailId:" + this.Email;
        }

    }
}
