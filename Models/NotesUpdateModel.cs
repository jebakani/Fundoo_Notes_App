// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotesUpdateModel.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Jebakani Ishwarya"/>
// ----------------------------------------------------------------------------------------------------------

namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Notes model class that has properties such as note id , description and title
    /// </summary>
    public class NotesUpdateModel
    {
        /// <summary>
        /// Gets or sets the Notes id which is primary key
        /// </summary>
        public int Notes { get; set; }

        /// <summary>
        /// Gets or sets the title in note as string
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the Description as string
        /// </summary>
        public string Description { get; set; }
    }
}
