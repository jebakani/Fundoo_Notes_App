// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabelManager.cs" company="Bridgelabz">
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
    /// Label manager access the repository and get the data from the database
    /// </summary>
    public class LabelManager : ILabelManager
    {
        /// <summary>
        /// Declaring object for the IUserRepository
        /// </summary>
        private readonly ILabelRepository repository;

        /// <summary>
        /// constructor for Note manager
        /// Initializes a new instance of the <see cref="LabelManager"/> class
        /// </summary>
        /// <param name="repository">repository of type ILabelRepository</param>
        public LabelManager(ILabelRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// the method to create the Label
        /// </summary>
        /// <param name="label">Label model object with id,name,user id and note Id</param>
        /// <returns>success or fail message</returns>
        public string CreateLabel(LabelModel label)
        {
            return this.repository.CreateLabel(label);
        }

        /// <summary>
        /// the method to Add the Label
        /// </summary>
        /// <param name="label">Label model object with id,name,user id and note Id</param>
        /// <returns>success or fail message</returns>
        public string AddLabel(LabelModel label)
        {
            return this.repository.AddLabel(label);
        }

        /// <summary>
        /// the method to remove the Label
        /// </summary>
        /// <param name="lableId">Label id that is unique</param>
        /// <returns>success or fail message</returns>
        public string RemoveLabel(int lableId)
        {
            return this.repository.RemoveLabel(lableId);
        }

        /// <summary>
        /// the method to delete the Label
        /// </summary>
        /// <param name="userId">user id as integer</param>
        /// <param name="labelName">label name as string</param>
        /// <returns>success or fail message</returns>
        public string DeleteLabel(LabelModel label)
        {
            return this.repository.DeleteLabel(label);
        }

        /// <summary>
        /// the method to get the Label
        /// </summary>
        /// <param name="userId">user id as integer</param>
        /// <returns>List of label in label model</returns>
        public List<LabelModel> GetLabelByUserId(int userId)
        {
            return this.repository.GetLabelByUserId(userId);
        }

        /// <summary>
        /// the method to get the Label
        /// </summary>
        /// <param name="noteId">note id as integer</param>
        /// <returns>List of label in label model</returns>
        public List<LabelModel> GetLabelByNoteId(int noteId)
        {
            return this.repository.GetLabelByNoteId(noteId);
        }

        /// <summary>
        /// the method to edit the Label
        /// </summary>
        /// <param name="label">Label model object with id,name,user id and note Id</param>
        /// <returns>success or fail message</returns>
        public string EditLabel(LabelModel label)
        {
            return this.repository.EditLabel(label);
        }

        /// <summary>
        /// declaring method to get all the notes for particular label
        /// </summary>
        /// <param name="labelName">label name as string</param>
        /// <param name="userId">user id as integer</param>
        /// <returns>list of notes</returns>
        public List<NotesModel> GetNotesByLabel(LabelModel label)
        {
            return this.repository.GetNotesByLabel(label);
        }
    }
}
