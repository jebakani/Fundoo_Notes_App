// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabelModel.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Jebakani Ishwarya"/>
// ----------------------------------------------------------------------------------------------------------

namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Model;

    /// <summary>
    /// Label model class that has properties for label
    /// </summary>
    public class LabelModel
    {
        /// <summary>
        /// Gets or sets the Label id which is primary key
        /// </summary>
        [Key]
        public int LabelId { get; set; }

        /// <summary>
        /// Gets or sets the Label Name which is required
        /// </summary>
        [Required]
        public string LabelName { get; set; }

        /// <summary>
        /// Gets or sets the User id which is foreign key from register model
        /// </summary>
        [ForeignKey("RegisterModel")]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the Note Id which is foreign key
        /// </summary>
        [ForeignKey("NotesModel")]
        public int? NoteId { get; set; }

        /// <summary>
        /// Gets or sets the Register Model which has user id as primary key
        /// </summary>
        public virtual RegisterModel RegisterModel { get; set; }

        /// <summary>
        /// Gets or sets the Notes Model which has note id as primary key
        /// </summary>
        public virtual NotesModel NotesModel { get; set; }
    }
}
