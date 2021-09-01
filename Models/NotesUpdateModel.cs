using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class NotesUpdateModel
    {
        public int UserId{get;set;}
        public int Notes { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

    }
}
