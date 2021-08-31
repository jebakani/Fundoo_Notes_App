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

    public class UserContext:DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }
        public DbSet<RegisterModel> user { get; set; }
    }
}
