// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotesModel.cs" company="Bridgelabz">
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
    /// Notes model class that has properties for creating notes
    /// </summary>
    public class NotesModel
    {
        /// <summary>
        /// Gets or sets the Notes id which is primary key
        /// </summary>
        [Key]
       public int NotesId { get; set; }

        /// <summary>
        /// Gets or sets the User id which is foreign key
        /// </summary>
        [ForeignKey("RegisterModel")]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the title in note as string
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the Description as string
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the Image url
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Gets or sets the color unicode as string
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Gets or sets the Remainder date and time in string format
        /// </summary>
        public string Remainder { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether status as true or false
        /// </summary>
        [DefaultValue(false)]
        public bool Pin { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether status as true or false
        /// </summary>
        [DefaultValue(false)]
        public bool Archieve { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether status as true or false        /// </summary>
        [DefaultValue(false)]
        public bool Trash { get; set; }

        /// <summary>
        /// Gets or sets the Register Model whose primary key is user id
        /// </summary>
        public virtual RegisterModel RegisterModel { get; set; }
    }
}
