using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Context
{
    //user context class extends the DbContext 
    /// <summary>
    ///  DbContext is a combination of the Unit Of Work and Repository patterns
    ///  DbContext is the primary class that is responsible for interacting with the database
    /// </summary>
    public class UserContext:DbContext
    {
        public UserContext(DbContextOptions<UserContext> options):base(options)
        {

        }

        //The DbSet class represents an entity set that can be used for create, read, update, and delete operations
        //user is the table where the user data is stored
        //register model represent the entity that is stored in the user table
        public DbSet<RegisterModel> user { get; set; }
    }
}
