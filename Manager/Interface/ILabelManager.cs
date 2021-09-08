// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILabelManager.cs" company="Bridgelabz">
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
    /// Interface for declaring the Label manager methods
    /// </summary>
    public interface ILabelManager
    {
        /// <summary>
        /// declaring the method to create the Label
        /// </summary>
        /// <param name="label">Label model object with id,name,user id and note Id</param>
        /// <returns>success or fail message</returns>
        string CreateLabel(LabelModel label);

        /// <summary>
        /// declaring the method to Add the Label
        /// </summary>
        /// <param name="label">Label model object with id,name,user id and note Id</param>
        /// <returns>success or fail message</returns>
        string AddLabel(LabelModel label);

        /// <summary>
        /// declaring the method to remove the Label
        /// </summary>
        /// <param name="lableId">Label id that is unique</param>
        /// <returns>success or fail message</returns>
        string RemoveLabel(int lableId);

        /// <summary>
        /// declaring the method to delete the Label
        /// </summary>
        /// <param name="userId">user id as integer</param>
        /// <param name="labelName">label name as string</param>
        /// <returns>success or fail message</returns>
        string DeleteLabel(int userId, string labelName);

        /// <summary>
        /// declaring the method to get the Label
        /// </summary>
        /// <param name="userId">user id as integer</param>
        /// <returns>List of label in label model</returns>
        List<LabelModel> GetLabelByUserId(int userId);

        /// <summary>
        /// declaring the method to get the Label
        /// </summary>
        /// <param name="noteId">note id as integer</param>
        /// <returns>List of label in label model</returns>
        List<LabelModel> GetLabelByNoteId(int noteId);

        /// <summary>
        /// declaring the method to edit the Label
        /// </summary>
        /// <param name="label">Label model object with id,name,user id and note Id</param>
        /// <returns>success or fail message</returns>
        string EditLabel(LabelModel label);

        /// <summary>
        /// declaring method to get all the notes for particular label
        /// </summary>
        /// <param name="labelName">label name as string</param>
        /// <param name="userId">user id as integer</param>
        /// <returns>list of notes</returns>
        List<NotesModel> GetNotesByLabel(string labelName, int userId);
    }
}
