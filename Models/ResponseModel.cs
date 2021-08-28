using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ResponseModel<T>
    {
        public bool status { get; set; }
        public string message { get; set; }
        public T data { get; set; } //data is optional whenever needed data is returned
    }
}
