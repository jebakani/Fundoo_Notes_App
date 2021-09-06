using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Models;
using Repository.Context;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class NotesRespository : INotesRepository
    {
        private readonly UserContext userContext;
        public IConfiguration Configuration { get; }
        public NotesRespository(UserContext userContext, IConfiguration Configuration)
        {
            this.userContext = userContext;
            this.Configuration = Configuration;
        }
        public string AddNotes(NotesModel notes)
        {
            try
            {
                //changes
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

        public List<NotesModel> GetNotes(int userId)
        {
            try
            {
                //checking the result using linq query user id has the notes 
                //if user id has n number of notes then push 
                var notes = this.userContext.Notes.Where(note => note.UserId == userId && note.Trash == false && note.Archieve == false).ToList();

                var emailId = userContext.User.Where(x => x.id == userId).Select(x => x.Email).SingleOrDefault();
                var collaboratorNotes = (from note in this.userContext.Notes
                                         join collaborator in this.userContext.Collaborators
                                         on note.NotesId equals collaborator.NoteId
                                         where collaborator.EmailId.Equals(emailId)
                                         select note ).ToList();
                notes.AddRange(collaboratorNotes);
                return notes;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<NotesModel> GetArchive(int userId)
        {
            try
            {
                //checking the result using linq query user id has the notes 
                //if user id has n number of notes then push 
                var notes = this.userContext.Notes.Where(note => note.UserId == userId && note.Trash == false && note.Archieve == true).ToList();
                return notes;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<NotesModel> GetRemainder(int userId)
        {
            try
            {
                //checking the result using linq query user id has the notes 
                //if user id has n number of notes then push 
                var notes = this.userContext.Notes.Where(note => note.UserId == userId && note.Trash == false && note.Remainder!=null).ToList();
                return notes;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<NotesModel> GetTrash(int userId)
        {
            try
            {
                //checking the result using linq query user id has the notes 
                //if user id has n number of notes then push 
                var notes = this.userContext.Notes.Where(note => note.UserId == userId && note.Trash == true).ToList();
                return notes;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
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
        public bool RestoreFromTrash(int noteId)
        {
            try
            {
                var notes = this.userContext.Notes.Where(note => note.NotesId == noteId && note.Trash==true).FirstOrDefault();
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
        public string MoveToArchieve(int noteId)
        {
            try
            {
                string message;
                var notes = this.userContext.Notes.Where(note =>  note.NotesId == noteId && note.Trash==false).FirstOrDefault();
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
        public bool UnArchive(int noteId)
        {
            try
            {
                var notes = this.userContext.Notes.Where(note =>  note.NotesId == noteId && note.Trash == false).FirstOrDefault();
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
                    if(notes.Archieve)
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
        public bool DeleteFromTrash(int noteId)
        {
            try
            {
                var notes = this.userContext.Notes.Where(note =>  note.NotesId == noteId && note.Trash == true).FirstOrDefault();
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
        public bool UpdateColor(int noteId,  string color)
        {
            try
            {
                var note= this.userContext.Notes.Where(note =>note.NotesId == noteId && note.Trash == false).FirstOrDefault();
                if(note!=null)
                {
                    note.Color = color;
                    this.userContext.Notes.Update(note);
                    this.userContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool UpdateRemainder(int noteId, string remainder)
        {
            try
            {
                var note = this.userContext.Notes.Where(note =>  note.NotesId == noteId && note.Trash == false).FirstOrDefault();
                if (note != null)
                {
                    note.Remainder = remainder;
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
        public bool AddImage(int noteId,IFormFile image)
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
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteImage(int noteId)
        { 
            try
            {
                var note = this.userContext.Notes.Where(x => x.NotesId == noteId).SingleOrDefault();
                if(note!=null)
                {
                    note.Image = null;
                    this.userContext.Notes.Update(note);
                    this.userContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}