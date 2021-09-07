// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollaboratorModel.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Jebakani Ishwarya"/>
// ----------------------------------------------------------------------------------------------------------

namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Collaborator model class that has the properties such as id, email, and notes id
    /// </summary>
    public class CollaboratorModel
    {
        /// <summary>
        /// Gets or sets the collaborator id which is primary key
        /// </summary>
        [Key]
        public int ColId { get; set; }

        /// <summary>
        ///  Gets or sets the Note id which is foreign key
        /// </summary>
        [ForeignKey("NotesModel")]
        public int NoteId { get; set; }

        /// <summary>
        /// Gets or sets the Email id  which is required
        /// </summary>
        [Required]
        public string EmailId { get; set; }

        /// <summary>
        /// Gets or sets the note model in which the note id is primary key
        /// </summary>
        public virtual NotesModel NotesModel { get; set; }
    }
}
