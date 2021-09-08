// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotesRespository.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Jebakani Ishwarya"/>
// ----------------------------------------------------------------------------------------------------------

namespace Repository.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Models;
    using global::Repository.Context;
    using global::Repository.Interface;

    /// <summary>
    /// note repository class that connect with the note table operation in database
    /// </summary>
    public class NotesRespository : INotesRepository
    {  
        /// <summary>
       /// create the object for user context
       /// </summary>
        private readonly UserContext userContext;

        /// <summary>
        /// getting user context object through constructor
        /// Initializes a new instance of the <see cref="NotesRespository"/> class
        /// </summary>
        /// <param name="userContext">user context object that has connection with database Context</param>
        /// <param name="configuration">configuration object to access the app setting file</param>
        public NotesRespository(UserContext userContext, IConfiguration configuration)
        {
            this.userContext = userContext;
            this.Configuration = configuration;
        }

        /// <summary>
        /// Gets method to get Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Method to add new notes
        /// </summary>
        /// <param name="notes">Notes model object that has all the properties of notes</param>
        /// <returns>Message whether added or not</returns>
        public string AddNotes(NotesModel notes)
        {
            try
            {
                if (notes != null && (notes.Description != null || notes.Title != null || notes.Image != null || notes.Remainder != null))
                {
                    //// add the data to the data base using user context 
                    this.userContext.Add(notes);
                    //// save the change in data base
                    this.userContext.SaveChanges();
                    return "Notes Added Succesfully";
                }

                return "Notes didn't get added";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// method to get the all notes
        /// </summary>
        /// <param name="userId">User id as integer</param>
        /// <returns>List of notes that are in type of note model object</returns>
        public List<NotesModel> GetNotes(int userId)
        {
            try
            {
                /*checking the result using linq query user id has the notes 
                  if user id has n number of notes then push */
                var notes = this.userContext.Notes.Where(note => note.UserId == userId && note.Trash == false && note.Archieve == false).ToList();

                var emailId = this.userContext.User.Where(x => x.id == userId).Select(x => x.Email).SingleOrDefault();
                var collaboratorNotes = (from note in this.userContext.Notes
                                         join collaborator in this.userContext.Collaborators
                                         on note.NotesId equals collaborator.NoteId
                                         where collaborator.EmailId.Equals(emailId)
                                         select note).ToList();
                notes.AddRange(collaboratorNotes);
                return notes;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// method to get the notes that are archive
        /// </summary>
        /// <param name="userId">User id as integer</param>
        /// <returns>List of notes that are in type of note model object</returns>
        public List<NotesModel> GetArchive(int userId)
        {
            try
            {
                /*checking the result using linq query user id has the notes 
                if user id has n number of notes then push */ 
                var notes = this.userContext.Notes.Where(note => note.UserId == userId && note.Trash == false && note.Archieve == true).ToList();
                return notes;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// method to get the notes that are having remainder
        /// </summary>
        /// <param name="userId">User id as integer</param>
        /// <returns>List of notes that are in type of note model object</returns>
        public List<NotesModel> GetRemainder(int userId)
        {
            try
            {
                /*checking the result using linq query user id has the notes 
                 if user id has n number of notes then push */
                var notes = this.userContext.Notes.Where(note => note.UserId == userId && note.Trash == false && note.Remainder != null).ToList();
                return notes;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// method to get the notes that are trash
        /// </summary>
        /// <param name="userId">User id as integer</param>
        /// <returns>List of notes that are in type of note model object</returns>
        public List<NotesModel> GetTrash(int userId)
        {
            try
            {
                /*checking the result using linq query user id has the notes 
                if user id has n number of notes then push */
                var notes = this.userContext.Notes.Where(note => note.UserId == userId && note.Trash == true).ToList();
                return notes;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Method to move the notes to trash
        /// </summary>
        /// <param name="noteId">notes id in Integer</param>
        /// <returns>the message as success or failed</returns>
        public string MoveToTrash(int noteId)
        {
            try
            {
                string message;
                var notes = this.userContext.Notes.Where(note => note.NotesId == noteId).FirstOrDefault();
                if (notes == null)
                {
                    return "Move to trash unsuccessful";
                }

                if (notes.Pin == true)
                {
                    message = "Unpinned and move to trash";
                    notes.Pin = false;
                }
                else
                {
                    message = "Notes Moved to trash";
                }

                notes.Remainder = null;
                notes.Trash = true;
                this.userContext.Notes.Update(notes);
                this.userContext.SaveChanges();
                return message;  
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Method to restore the notes from trash
        /// </summary>
        /// <param name="noteId">notes id in Integer</param>
        /// <returns>boolean value as true or false</returns>
        public bool RestoreFromTrash(int noteId)
        {
            try
            {
                var notes = this.userContext.Notes.Where(note => note.NotesId == noteId && note.Trash == true).FirstOrDefault();
                if (notes != null)
                {
                    notes.Trash = false;
                    this.userContext.Notes.Update(notes);
                    this.userContext.SaveChanges();
                    return true;
                }
                
                return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Method to move the notes to archive
        /// </summary>
        /// <param name="noteId">notes id in Integer</param>
        /// <returns>the message as success or failed</returns>
        public string MoveToArchieve(int noteId)
        {
            try
            {
                string message;
                var notes = this.userContext.Notes.Where(note => note.NotesId == noteId && note.Trash == false).FirstOrDefault();
                if (notes != null)
                {
                    if (notes.Pin == true)
                    {
                        message = "Unpinned and Note is archive";
                        notes.Pin = false;
                    }
                    else
                    {
                        message = "Notes archived";
                    }

                    notes.Archieve = true;
                    this.userContext.Notes.Update(notes);
                    this.userContext.SaveChanges();
                    return message;
                }

                return "Notes not available";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Method to un archive the notes
        /// </summary>
        /// <param name="noteId">notes id in Integer</param>
        /// <returns>boolean value as true or false</returns>
        public bool UnArchive(int noteId)
        {
            try
            {
                var notes = this.userContext.Notes.Where(note => note.NotesId == noteId && note.Trash == false).FirstOrDefault();
                if (notes != null)
                {
                    notes.Archieve = false;
                    this.userContext.Notes.Update(notes);
                    this.userContext.SaveChanges();
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Method to pin or unpin the note
        /// </summary>
        /// <param name="noteId">notes id in Integer</param>
        /// <returns>the message as success or failed</returns>
        public string PinAndUnpinNotes(int noteId)
        {
            try
            {
                string message;
                var notes = this.userContext.Notes.Where(note => note.NotesId == noteId && note.Trash == false).FirstOrDefault();
                if (notes != null)
                {
                    if (notes.Pin)
                    {
                        notes.Pin = false;
                        message = "notes is Unpinned";
                    }
                    else
                    {
                        notes.Pin = true;
                        message = "notes is pinned";
                    }

                    if (notes.Archieve)
                    {
                        notes.Archieve = false;
                        message = "Notes unarchived and pinned";
                    }

                    this.userContext.Notes.Update(notes);
                    this.userContext.SaveChanges();
                    return message;
                }

                return "Pinning Unsuccessfull";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Method to update the notes
        /// </summary>
        /// <param name="updateNote">Notes model object that has all the properties of notes</param>
        /// <returns>The note model object</returns>
        public NotesModel UpdateNote(NotesUpdateModel updateNote)
        {
            try
            {
                var notes = this.userContext.Notes.Where(note => note.NotesId == updateNote.Notes && note.Trash == false).FirstOrDefault();
                if (notes != null)
                {
                    notes.Description = updateNote.Description;
                    notes.Title = updateNote.Title;
                    this.userContext.Notes.Update(notes);
                    this.userContext.SaveChanges();
                    return notes;
                }

                return notes;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Method to delete the notes from trash
        /// </summary>
        /// <param name="noteId">notes id in Integer</param>
        /// <returns>boolean value as true or false</returns>
        public bool DeleteFromTrash(int noteId)
        {
            try
            {
                var notes = this.userContext.Notes.Where(note => note.NotesId == noteId && note.Trash == true).FirstOrDefault();
                if (notes != null)
                {
                    this.userContext.Notes.Remove(notes);
                    this.userContext.SaveChanges();
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Method to update the color in the note
        /// </summary>
        /// <param name="noteId">notes id in Integer</param>
        /// <param name="color">color as string value</param>
        /// <returns>boolean value as true or false</returns>
        public string UpdateColor(int noteId,  string color)
        {
            try
            {
                var note = this.userContext.Notes.Where(note => note.NotesId == noteId && note.Trash == false).FirstOrDefault();
                if (note != null)
                {
                    note.Color = color;
                    this.userContext.Notes.Update(note);
                    this.userContext.SaveChanges();
                    return "Color is updated";
                }

                return color;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Method to move the notes to trash
        /// </summary>
        /// <param name="noteId">notes id in Integer</param>
        /// <param name="remainder">remainder value in terms of date and time</param>
        /// <returns>boolean value as true or false</returns>
        public string UpdateRemainder(int noteId, string remainder)
        {
            try
            {
                var note = this.userContext.Notes.Where(note => note.NotesId == noteId && note.Trash == false).FirstOrDefault();
                if (note != null)
                {
                    note.Remainder = remainder;
                    this.userContext.Notes.Update(note);
                    this.userContext.SaveChanges();
                    return "remainder is added";
                }
                return remainder;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Method to delete the remainder from notes
        /// </summary>
        /// <param name="noteId">notes id in Integer</param>
        /// <returns>boolean value as true or false</returns>
        public bool DeleteRemainder(int noteId)
        {
            try
            {
                var note = this.userContext.Notes.Where(note => note.NotesId == noteId && note.Trash == false).FirstOrDefault();
                if (note != null)
                {
                    note.Remainder = null;
                    this.userContext.Notes.Update(note);
                    this.userContext.SaveChanges();
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Method to empty the trash
        /// </summary>
        /// <param name="userId">notes id in Integer</param>
        /// <returns>boolean value as true or false</returns>
        public bool EmptyTrash(int userId)
        {
            try
            {
                var notes = this.userContext.Notes.Where(note => note.UserId == userId && note.Trash == true).ToList();
                if (notes.Count > 0)
                {
                    this.userContext.Notes.RemoveRange(notes);
                    this.userContext.SaveChanges();
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Method add image to the note
        /// </summary>
        /// <param name="noteId">notes id in Integer</param>
        /// <param name="image">image as an i form </param>
        /// <returns>boolean value as true or false</returns>
        public string AddImage(int noteId, IFormFile image)
        {
            try
            {
                Account account = new Account(this.Configuration.GetValue<string>("CloudConfiguration:CloudName"), this.Configuration.GetValue<string>("CloudConfiguration:APIKey"), this.Configuration.GetValue<string>("CloudConfiguration:APISecret"));
                var cloudinary = new Cloudinary(account);
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(image.FileName, image.OpenReadStream()),
                };

                var uploadResult = cloudinary.Upload(uploadParams);
                string imagePath = uploadResult.Url.ToString();
                var notes = this.userContext.Notes.Where(x => x.NotesId == noteId).SingleOrDefault();
                if (notes != null)
                {
                    notes.Image = imagePath;
                    this.userContext.Notes.Update(notes);
                    this.userContext.SaveChanges();
                    return "Image is updated";
                }

                return imagePath;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Method to delete the image in the notes
        /// </summary>
        /// <param name="noteId">notes id in Integer</param>
        /// <returns>boolean value as true or false</returns>
        public bool DeleteImage(int noteId)
        { 
            try
            {
                var note = this.userContext.Notes.Where(x => x.NotesId == noteId).SingleOrDefault();
                if (note != null)
                {
                    note.Image = null;
                    this.userContext.Notes.Update(note);
                    this.userContext.SaveChanges();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}