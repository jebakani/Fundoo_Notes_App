using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes1.Model
{
    public class RegisterModel
    {
        [Key]
        public string id { get; set; }

        public string firstName { get; set; }
        
        public string lastName { get; set; }
        
        public string email { get; set; }
        
        public string password { get; set; }


    }
}
