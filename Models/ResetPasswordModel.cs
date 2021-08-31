using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ResetPasswordModel
    {
        [Required]
        public string EmailId { get; set; }

        [Required]
        public string NewPassword { get; set; }
    }
}
