// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICollaboratorManager.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Jebakani Ishwarya"/>
// ----------------------------------------------------------------------------------------------------------

namespace Manager.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Models;

    /// <summary>
    /// Interface to access the Collaborator manager
    /// </summary>
    public interface ICollaboratorManager
    {
        /// <summary>
        /// Definition for add collaborator
        /// </summary>
        /// <param name="collaborator">collaborator model that contains id,name notesId</param>
        /// <returns>result as pass or fail</returns>
        string AddCollaborator(CollaboratorModel collaborator);

        /// <summary>
        /// declaring the method to remove the collaborator
        /// </summary>
        /// <param name="collaboratorId">collaborator id as integer</param>
        /// <returns>result as pass or fail</returns>
        string RemoveCollaborator(int collaboratorId);

        /// <summary>
        /// declaring the method to get the collaborator list
        /// </summary>
        /// <param name="noteId">note id that is integer</param>
        /// <returns>list of collaborator</returns>
        List<CollaboratorModel> GetCollaborator(int noteId);
    }
}
