// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotesManager.cs" company="Bridgelabz">
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
    using Microsoft.AspNetCore.Http;
    using Models;
    using Repository.Interface;

    /// <summary>
    /// Notes manager class to manage the data of the notes
    /// </summary>
    public class NotesManager : INotesManager
    {
        /// <summary>
        /// Declaring object for the IUserRepository
        /// </summary>
        private readonly INotesRepository repository;

        /// <summary>
        /// constructor for Note manager
        /// Initializes a new instance of the <see cref="NotesManager"/> class
        /// </summary>
        /// <param name="repository">repository of type INotesRepository</param>
        public NotesManager(INotesRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Method to add new notes
        /// </summary>
        /// <param name="notes">Notes model object that has all the properties of notes</param>
        /// <returns>Message whether added or not</returns>
        public string AddNotes(NotesModel notes)
        {
            return this.repository.AddNotes(notes);
        }

        /// <summary>
        /// method to get the all notes
        /// </summary>
        /// <param name="userId">User id as integer</param>
        /// <returns>List of notes that are in type of note model object</returns>
        public List<NotesModel> GetNotes(int userId)
        {
            return this.repository.GetNotes(userId);
        }

        /// <summary>
        /// method to get the notes that are archive
        /// </summary>
        /// <param name="userId">User id as integer</param>
        /// <returns>List of notes that are in type of note model object</returns>
        public List<NotesModel> GetArchive(int userId)
        {
            return this.repository.GetArchive(userId);
        }

        /// <summary>
        /// method to get the notes that are having remainder
        /// </summary>
        /// <param name="userId">User id as integer</param>
        /// <returns>List of notes that are in type of note model object</returns>
        public List<NotesModel> GetRemainder(int userId)
        {
            return this.repository.GetRemainder(userId);
        }

        /// <summary>
        /// method to get the notes that are trash
        /// </summary>
        /// <param name="userId">User id as integer</param>
        /// <returns>List of notes that are in type of note model object</returns>
        public List<NotesModel> GetTrash(int userId)
        {
            return this.repository.GetTrash(userId);
        }

        /// <summary>
        /// Method to move the notes to trash
        /// </summary>
        /// <param name="noteId">notes id in Integer</param>
        /// <returns>the message as success or failed</returns>
        public string MoveToTrash(int noteId)
        {
            return this.repository.MoveToTrash(noteId);
        }

        /// <summary>
        /// Method to restore the notes from trash
        /// </summary>
        /// <param name="noteId">notes id in Integer</param>
        /// <returns>boolean value as true or false</returns>
        public bool RestoreFromTrash(int noteId)
        {
            return this.repository.RestoreFromTrash(noteId);
        }

        /// <summary>
        /// Method to move the notes to archive
        /// </summary>
        /// <param name="noteId">notes id in Integer</param>
        /// <returns>the message as success or failed</returns>
        public string MoveToArchieve(int noteId)
        {
            return this.repository.MoveToArchieve(noteId);
        }

        /// <summary>
        /// Method to un archive the notes
        /// </summary>
        /// <param name="noteId">notes id in Integer</param>
        /// <returns>boolean value as true or false</returns>
        public bool UnArchive(int noteId)
        {
            return this.repository.UnArchive(noteId);
        }

        /// <summary>
        /// Method to pin or unpin the note
        /// </summary>
        /// <param name="noteId">notes id in Integer</param>
        /// <returns>the message as success or failed</returns>
        public string PinAndUnpinNotes(int noteId)
        {
            return this.repository.PinAndUnpinNotes(noteId);
        }

        /// <summary>
        /// Method to update the notes
        /// </summary>
        /// <param name="updateNote">Notes model object that has all the properties of notes</param>
        /// <returns>The note model object</returns>
        public NotesModel UpdateNote(NotesUpdateModel updateNote)
        {
            return this.repository.UpdateNote(updateNote);
        }

        /// <summary>
        /// Method to delete the notes from trash
        /// </summary>
        /// <param name="noteId">notes id in Integer</param>
        /// <returns>boolean value as true or false</returns>
        public bool DeleteFromTrash(int noteId)
        {
            return this.repository.DeleteFromTrash(noteId);
        }

        /// <summary>
        /// Method to update the color in the note
        /// </summary>
        /// <param name="noteId">notes id in Integer</param>
        /// <param name="color">color as string value</param>
        /// <returns>boolean value as true or false</returns>
        public bool UpdateColor(int noteId, string color)
        {
            return this.repository.UpdateColor(noteId, color);
        }

        /// <summary>
        /// Method to move the notes to trash
        /// </summary>
        /// <param name="noteId">notes id in Integer</param>
        /// <param name="remainder">remainder value in terms of date and time</param>
        /// <returns>boolean value as true or false</returns>
        public bool UpdateRemainder(int noteId, string remainder)
        {
            return this.repository.UpdateRemainder(noteId, remainder);
        }

        /// <summary>
        /// Method to delete the remainder from notes
        /// </summary>
        /// <param name="noteId">notes id in Integer</param>
        /// <returns>boolean value as true or false</returns>
        public bool DeleteRemainder(int noteId)
        {
            return this.repository.DeleteRemainder(noteId);
        }

        /// <summary>
        /// Method to empty the trash
        /// </summary>
        /// <param name="userId">notes id in Integer</param>
        /// <returns>boolean value as true or false</returns>
        public bool EmptyTrash(int userId)
        {
            return this.repository.EmptyTrash(userId);
        }

        /// <summary>
        /// Method add image to the note
        /// </summary>
        /// <param name="noteId">notes id in Integer</param>
        /// <param name="image">image as an i form </param>
        /// <returns>boolean value as true or false</returns>
        public bool AddImage(int noteId, IFormFile image)
        {
            return this.repository.AddImage(noteId, image);
        }

        /// <summary>
        /// Method to delete the image in the notes
        /// </summary>
        /// <param name="noteId">notes id in Integer</param>
        /// <returns>boolean value as true or false</returns>
        public bool DeleteImage(int noteId)
        {
            return this.repository.DeleteImage(noteId);
        }
    }
}
