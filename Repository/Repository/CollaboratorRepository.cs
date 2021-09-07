// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollaboratorRepository.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Jebakani Ishwarya"/>
// ----------------------------------------------------------------------------------------------------------

namespace Repository.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;
    using System.Text;
    using System.Threading.Tasks;
    using Experimental.System.Messaging;
    using Models;
    using global::Repository.Context;
    using global::Repository.Interface;

    /// <summary>
    /// Repository class for the collaborator 
    /// </summary>
    public class CollaboratorRepository : ICollaboratorRepository
    {
        /// <summary>
        /// declare the object for UserContext
        /// </summary>
        private readonly UserContext userContext;

        /// <summary>
        /// constructor to assign value to UserContext
        /// Initializes a new instance of the <see cref="CollaboratorRepository" /> class
        /// </summary>
        /// <param name="userContext">object of UserContext</param>
        public CollaboratorRepository(UserContext userContext)
        {
            this.userContext = userContext;
        }

        /// <summary>
        /// method to manage the  add collaborator
        /// </summary>
        /// <param name="collaborator">collaborator model that contains id,name notesId</param>
        /// <returns>result as pass or fail as a statement</returns>
        public string AddCollaborator(CollaboratorModel collaborator)
        {
            try
            {
                var ownerId = this.userContext.User.Where(u => u.id == this.userContext.Notes
                       .Where(x => x.NotesId == collaborator.NoteId).Select(x => x.UserId).FirstOrDefault() && u.Email == collaborator.EmailId).SingleOrDefault();
                if (ownerId == null)
                {
                    var result = this.userContext.Collaborators.Where(x => x.NoteId == collaborator.NoteId && x.EmailId == collaborator.EmailId).SingleOrDefault();
                    if (result == null)
                    {
                        this.userContext.Collaborators.Add(collaborator);
                        this.userContext.SaveChanges();
                        return "Collaborator is added";
                    }
                }

                return "email id already exist";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// manager method to remove the collaborator from note
        /// </summary>
        /// <param name="collaboratorsId">unique collaborator id as integer</param>
        /// <returns>returns success or fail as string</returns>
        public string RemoveCollaborator(int collaboratorsId)
        {
            try
            {
                var collaborator = this.userContext.Collaborators.Where(x => x.ColId == collaboratorsId).SingleOrDefault();
                if (collaborator != null)
                {
                    this.userContext.Collaborators.Remove(collaborator);
                    this.userContext.SaveChanges();
                    return "Collaborator is deleted";
                }

                return "can't able to delete";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// manager method to get the list of collaborator for the notes
        /// </summary>
        /// <param name="noteId">passing note id </param>
        /// <returns>return the list of collaborator</returns>
        public List<CollaboratorModel> GetCollaborator(int noteId)
        {
            try
            {
                /*checking the result using linq query user id has the notes 
                  if user id has n number of notes then push*/ 
                var collaborators = this.userContext.Collaborators.Where(c => c.NoteId == noteId).ToList();
                return collaborators;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
