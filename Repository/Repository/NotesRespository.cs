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
        public NotesRespository(UserContext userContext)
        {
            this.userContext = userContext;
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

        public List<NotesModel> GetNotes(int id)
        {
            try
            {
                //checking the result using linq query user id has the notes 
                //if user id has n number of notes then push 
                var notes = this.userContext.Notes.Where(note => note.UserId == id && note.Trash == false && note.Archieve==false).ToList();
                return notes;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool MoveToTrash(int noteId)
        {
            try
            {
                var notes = this.userContext.Notes.Where(note => note.NotesId == noteId).FirstOrDefault();
                if (notes != null)
                {
                    notes.Trash = true;
                    this.userContext.Update(notes);
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
        public bool RestoreFromTrash(int noteId)
        {
            try
            {
                var notes = this.userContext.Notes.Where(note => note.NotesId == noteId && note.Trash==true).FirstOrDefault();
                if (notes != null)
                {
                    notes.Trash = false;
                    this.userContext.Update(notes);
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
        public bool MoveToArchieve(int noteId)
        {
            try
            {
                var notes = this.userContext.Notes.Where(note =>  note.NotesId == noteId && note.Trash==false).FirstOrDefault();
                if (notes != null)
                {
                    notes.Archieve = true;
                    this.userContext.Update(notes);
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
        public bool UnArchive(int noteId)
        {
            try
            {
                var notes = this.userContext.Notes.Where(note =>  note.NotesId == noteId && note.Trash == false).FirstOrDefault();
                if (notes != null)
                {
                    notes.Archieve = false;
                    this.userContext.Update(notes);
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
                    this.userContext.Update(notes);
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
                    this.userContext.Update(notes);
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
                    this.userContext.Update(note);
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
                    this.userContext.Update(note);
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
    }
}