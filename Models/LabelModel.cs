using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class LabelModel
    {
        [Key]
        public int LabelId { get; set; }
        [Required]
        public string LabelName { get; set; }

        [ForeignKey("RegisterModel")]
        public int UserId { get; set; }

        [ForeignKey("NotesModel")]
        public int? NoteId { get; set; }
        public virtual RegisterModel RegisterModel { get; set; }
        public virtual NotesModel NotesModel { get; set; }
    }
}
