// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollaboratorManager.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Jebakani Ishwarya"/>
// ----------------------------------------------------------------------------------------------------------

namespace Manager.Manager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using global::Manager.Interface;
    using Models;
    using Repository.Interface;

    /// <summary>
    /// Manager class for the collaborator 
    /// </summary>
    public class CollaboratorManager : ICollaboratorManager
    {
        /// <summary>
        /// declare the object for Collaborator repository
        /// </summary>
        private readonly ICollaboratorRepository repository;

        /// <summary>
        /// constructor to assign value to repository object
        /// Initializes a new instance of the <see cref="CollaboratorManager" /> class
        /// </summary>
        /// <param name="repository">object of Collaborator repository</param>
        public CollaboratorManager(ICollaboratorRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// method to manage the  add collaborator
        /// </summary>
        /// <param name="collaborator">collaborator model that contains id,name notesId</param>
        /// <returns>result as pass or fail</returns>
        public string AddCollaborator(CollaboratorModel collaborator)
        {
           return this.repository.AddCollaborator(collaborator);
        }

        /// <summary>
        /// manager method to remove the collaborator from note
        /// </summary>
        /// <param name="collaboratorId">unique collaborator id as integer</param>
        /// <returns>returns success or fail</returns>
        public string RemoveCollaborator(int collaboratorId)
        {
            return this.repository.RemoveCollaborator(collaboratorId);
        }

        /// <summary>
        /// manager method to get the list of collaborator for the notes
        /// </summary>
        /// <param name="noteId">passing note id </param>
        /// <returns>return the list of collaborator</returns>
        public List<CollaboratorModel> GetCollaborator(int noteId)
        {
            return this.repository.GetCollaborator(noteId);
        }
    }
}
