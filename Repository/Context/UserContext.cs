// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserContext.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Jebakani Ishwaraya V"/>
// ----------------------------------------------------------------------------------------------------------
namespace Repository.Context
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Model;
    using Models;

    /// <summary>
    /// User context class that extends the Database Context
    /// </summary>
    public class UserContext : DbContext
    {
        /// <summary>
        /// Constructor that overrides the base option
        /// Initializes a new instance of the <see cref="UserContext"/> class
        /// </summary>
        /// <param name="options">configurations</param>
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the user table 
        /// </summary>
        public DbSet<RegisterModel> user { get; set; }
        public DbSet<NotesModel> Notes { get; set; }
    }
}
