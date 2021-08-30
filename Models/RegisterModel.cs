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
        public string firstName { get; set; }
        
        [Required]
        public string lastName { get; set; }

        [Required]
        public string email { get; set; }
        
        [Required]
        public string password { get; set; }


    }
}
