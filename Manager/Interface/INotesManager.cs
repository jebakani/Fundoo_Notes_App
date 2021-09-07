// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INotesManager.cs" company="Bridgelabz">
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
    using Microsoft.AspNetCore.Http;
    using Models;

    /// <summary>
    /// Interface for declaring the Notes manager methods
    /// </summary>
    public interface INotesManager
    {
        /// <summary>
        /// Method to add new notes
        /// </summary>
        /// <param name="notes">Notes model object that has all the properties of notes</param>
        /// <returns>Message whether added or not</returns>
        string AddNotes(NotesModel notes);

        /// <summary>
        /// method to get the all notes
        /// </summary>
        /// <param name="userId">User id as integer</param>
        /// <returns>List of notes that are in type of note model object</returns>
        List<NotesModel> GetNotes(int userId);

        /// <summary>
        /// method to get the notes that are archive
        /// </summary>
        /// <param name="userId">User id as integer</param>
        /// <returns>List of notes that are in type of note model object</returns>
        List<NotesModel> GetArchive(int userId);

        /// <summary>
        /// method to get the notes that are having remainder
        /// </summary>
        /// <param name="userId">User id as integer</param>
        /// <returns>List of notes that are in type of note model object</returns>
        List<NotesModel> GetRemainder(int userId);

        /// <summary>
        /// method to get the notes that are trash
        /// </summary>
        /// <param name="userId">User id as integer</param>
        /// <returns>List of notes that are in type of note model object</returns>
        List<NotesModel> GetTrash(int userId);

        /// <summary>
        /// Method to move the notes to trash
        /// </summary>
        /// <param name="noteId">notes id in Integer</param>
        /// <returns>the message as success or failed</returns>
        string MoveToTrash(int noteId);

        /// <summary>
        /// Method to move the notes to archive
        /// </summary>
        /// <param name="noteId">notes id in Integer</param>
        /// <returns>the message as success or failed</returns>
        string MoveToArchieve(int noteId);

        /// <summary>
        /// Method to un archive the notes
        /// </summary>
        /// <param name="noteId">notes id in Integer</param>
        /// <returns>boolean value as true or false</returns>
        bool UnArchive(int noteId);

        /// <summary>
        /// Method to restore the notes from trash
        /// </summary>
        /// <param name="noteId">notes id in Integer</param>
        /// <returns>boolean value as true or false</returns>
        bool RestoreFromTrash(int noteId);

        /// <summary>
        /// Method to pin or unpin the note
        /// </summary>
        /// <param name="noteId">notes id in Integer</param>
        /// <returns>the message as success or failed</returns>
        string PinAndUnpinNotes(int noteId);

        /// <summary>
        /// Method to update the notes
        /// </summary>
        /// <param name="updateNote">Notes model object that has all the properties of notes</param>
        /// <returns>The note model object</returns>
        NotesModel UpdateNote(NotesUpdateModel updateNote);

        /// <summary>
        /// Method to delete the notes from trash
        /// </summary>
        /// <param name="noteId">notes id in Integer</param>
        /// <returns>boolean value as true or false</returns>
        bool DeleteFromTrash(int noteId);

        /// <summary>
        /// Method to update the color in the note
        /// </summary>
        /// <param name="noteId">notes id in Integer</param>
        /// <param name="color">color as string value</param>
        /// <returns>boolean value as true or false</returns>
        bool UpdateColor(int noteId, string color);

        /// <summary>
        /// Method to move the notes to trash
        /// </summary>
        /// <param name="noteId">notes id in Integer</param>
        /// <param name="remainder">remainder value in terms of date and time</param>
        /// <returns>boolean value as true or false</returns>
        bool UpdateRemainder(int noteId, string remainder);

        /// <summary>
        /// Method to delete the remainder from notes
        /// </summary>
        /// <param name="noteId">notes id in Integer</param>
        /// <returns>boolean value as true or false</returns>
        bool DeleteRemainder(int noteId);

        /// <summary>
        /// Method to empty the trash
        /// </summary>
        /// <param name="userId">notes id in Integer</param>
        /// <returns>boolean value as true or false</returns>
        bool EmptyTrash(int userId);

        /// <summary>
        /// Method add image to the note
        /// </summary>
        /// <param name="noteId">notes id in Integer</param>
        /// <param name="image">image as an i form </param>
        /// <returns>boolean value as true or false</returns>
        bool AddImage(int noteId, IFormFile image);

        /// <summary>
        /// Method to delete the image in the notes
        /// </summary>
        /// <param name="noteId">notes id in Integer</param>
        /// <returns>boolean value as true or false</returns>
        bool DeleteImage(int noteId);
    }
}
