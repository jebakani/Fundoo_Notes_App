using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    //the class that stores the response details
    public class ResponseModel<T>
    {
        //status represent the true or false 
        public bool status { get; set; }
        public string message { get; set; }
        public T data { get; set; } //data is optional whenever needed data is returned
    }
}
