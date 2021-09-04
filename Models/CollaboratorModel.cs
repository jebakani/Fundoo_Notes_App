using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CollaboratorModel
    {
        [Key]
        public int ColId { get; set; }
        [ForeignKey("NotesModel")]
        public int NoteId { get; set; }
        [Required]
        public string EmailId { get; set; }
        public virtual NotesModel NotesModel { get; set; }
    }
}
