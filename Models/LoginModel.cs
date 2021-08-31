using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class LoginModel
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z]{3}([\\- \\+ _\\.]*[a-zA-Z0-9]+)*@[a-zA-Z0-9]+\\.[a-z]{2,3}(\\.[a-zA-Z]{2,4}){0,1}$", ErrorMessage = "Invalid Email Id")]
        public string EmailId { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?!.*[<>`])(?=[^.,:;'!@#$%^&*_+=|(){}[?\-\]\/\\]*[.,:;'!@#$%^&*_+=|(){}[?\-\]\/\\][^.,:;'!@#$%^&*_+=|(){}[?\-\]\/\\]*$).{8,}$", ErrorMessage = "Invalid Password")]
        public string Password { get; set; }
    }
}
